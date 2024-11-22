using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Web.Services.Description;

namespace TPFinal_Ecommerce_Grupo14B
{
    public partial class MiCuenta1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {

                Response.Redirect("IniciarSesion.aspx");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                    string.IsNullOrWhiteSpace(txtLocalidad.Text) ||
                    string.IsNullOrWhiteSpace(txtFechaNacimiento.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CamposVacios",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Todos los campos son obligatorios.'});", true);
                    return;
                }

                if (System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text.Trim(), @"\d"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NombreInvalido",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'El nombre no puede contener números.'});", true);
                    return;
                }


                if (!txtEmail.Text.Contains("@"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CorreoInvalido",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'El correo electrónico debe contener el carácter @.'});", true);
                    return;
                }


                if (!long.TryParse(txtTelefono.Text.Trim(), out _))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "TelefonoInvalido",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'El teléfono debe contener solo números.'});", true);
                    return;
                }


                if (txtClave.Text.Trim() != txtConfirmacionPassword.Text.Trim())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorPassword",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Las contraseñas no coinciden.'});", true);
                    return;
                }


                Usuario usuarioSesion = (Usuario)Session["usuario"];


                Usuario usuarioActualizado = new Usuario
                {
                    Id = usuarioSesion.Id,
                    Nombre = txtNombre.Text.Trim(),
                    Correo = txtEmail.Text.Trim(),
                    Clave = string.IsNullOrWhiteSpace(txtClave.Text) ? usuarioSesion.Clave : txtClave.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Localidad = txtLocalidad.Text.Trim(),
                    FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                    IdRol = usuarioSesion.IdRol
                };


                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.modificar(usuarioActualizado);


                Session["usuario"] = usuarioActualizado;


                ScriptManager.RegisterStartupScript(this, this.GetType(), "UsuarioModificado",
                    "Swal.fire({icon: 'success', title: 'Perfil actualizado', text: 'Tus datos han sido actualizados correctamente.'});", true);


                EnviarCorreoActualizacion(usuarioActualizado.Correo, usuarioActualizado);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorActualizacion",
                    $"Swal.fire({{icon: 'error', title: 'Error', text: 'Hubo un problema al actualizar tus datos: {ex.Message}'}});", true);
            }
        }

        private void EnviarCorreoActualizacion(string emailCliente, Usuario usuarioActualizado)
        {
            try
            {

                if (string.IsNullOrEmpty(emailCliente) || !emailCliente.Contains("@"))
                {
                    return;
                }


                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("ecommerce14bretro@gmail.com");
                mensaje.To.Add(emailCliente);
                mensaje.Subject = "Actualización de perfil";


                string cuerpoMensaje = $@"
                   Hola { usuarioActualizado.Nombre},
        
                      Tus datos de perfil han sido actualizados correctamente. A continuación, se detallan los datos actualizados:
        
                        Nombre: { usuarioActualizado.Nombre}
                        Correo: { usuarioActualizado.Correo}
                        Dirección: { usuarioActualizado.Direccion}
                        Teléfono: { usuarioActualizado.Telefono}
                        Localidad: { usuarioActualizado.Localidad}
                        Fecha de Nacimiento: { usuarioActualizado.FechaNacimiento.ToShortDateString()}
        
                          Si detectas algún error o necesitas realizar más modificaciones, por favor contáctanos.
                         ¡Saludos!
                                   ";

                       mensaje.Body = cuerpoMensaje;
                       mensaje.IsBodyHtml = false;


                SmtpClient clienteSmtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("ecommerce14bretro@gmail.com", "lmfw xlpn tkaw iqkx"),
                    EnableSsl = true
                };


                clienteSmtp.Send(mensaje);


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Datos actualizados",
                    "Swal.fire({icon: 'success', title: 'Datos actualizados', text: 'Se ha enviado un correo con la actualización de datos.'});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorEnvioCorreo",
                    "Swal.fire({icon: 'error', title: 'Error', text: 'Hubo un problema al enviar el correo de confirmación.'});", true);
            }
        }




        private void CargarDatosUsuario(Usuario usuario)
        {

            txtNombre.Text = usuario.Nombre;
            txtEmail.Text = usuario.Correo;
            txtDireccion.Text = usuario.Direccion;
            txtTelefono.Text = usuario.Telefono;
            txtLocalidad.Text = usuario.Localidad;
            txtFechaNacimiento.Text = usuario.FechaNacimiento.ToString("yyyy-MM-dd");
        }
    }

}
