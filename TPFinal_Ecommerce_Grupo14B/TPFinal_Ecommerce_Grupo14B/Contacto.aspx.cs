using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace TPFinal_Ecommerce_Grupo14B
{
    public partial class Contacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnEnviarMensaje_Click(object sender, EventArgs e)
        {
            try
            {
               
                string nombre = txtNombre.Text.Trim();
                string emailCliente = txtEmail.Text.Trim();
                string asunto = txtAsunto.Text.Trim();
                string mensajeCliente = txtMensaje.Text.Trim();

               
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(emailCliente) ||
                    string.IsNullOrEmpty(asunto) || string.IsNullOrEmpty(mensajeCliente))
                {
                    throw new Exception("Todos los campos son obligatorios.");
                }

               
                if (!Regex.IsMatch(emailCliente, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new Exception("El correo electrónico no tiene un formato válido.");
                }

               
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("ecommerce14bretro@gmail.com"); 
                mensaje.To.Add("ecommerce14bretro@gmail.com"); 
                mensaje.Subject = asunto;
                mensaje.Body = $"Nombre: {nombre}\nCorreo: {emailCliente}\nMensaje: {mensajeCliente}";
                mensaje.IsBodyHtml = false;

                // Configura el cliente SMTP
                SmtpClient clienteSmtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("ecommerce14bretro@gmail.com", "lmfw xlpn tkaw iqkx"), 
                    EnableSsl = true
                };

                
                clienteSmtp.Send(mensaje);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Swal.fire({icon: 'success', title: 'Mensaje enviado', text: 'El mensaje se ha enviado exitosamente.'});", true);
                txtNombre.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtAsunto.Text = string.Empty;
                txtMensaje.Text = string.Empty;

                return;
            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"Swal.fire({{icon: 'error', title: 'Error', text: '{ex.Message}'}});", true);
            }

        }


    }
}