using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Ecommerce_Grupo14B
{
    public partial class IniciarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] == "true")
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("~/IniciarSesion.aspx");
            }
        }

       

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuario.Correo = txtEmail.Text;
                usuario.Clave = txtPassword.Text;
                usuario.Estado = negocio.ExisteEmail(txtEmail.Text);
                if (negocio.loguear(usuario))
                {
                    if (!usuario.Estado)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "UsuarioInactivo",
                            "Swal.fire({icon: 'warning', title: 'Usuario Inactivo', text: 'Tu usuario está inactivo.'});", true);
                    }
                    else
                    {
                        Session["usuario"] = usuario;
                        Session["idRol"] = usuario.IdRol;

                        string mensajeExito = usuario.IdRol == 3
                            ? "¡Bienvenido! Redirigiendo a la página principal..." : "¡Acceso exitoso! Redirigiendo al panel...";

                        string redireccion = usuario.IdRol == 3
                            ? "Default.aspx" : "AdministracionGeneral.aspx";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SesionExitosa",
                            $"Swal.fire({{icon: 'success', title: 'Inicio de Sesión', text: '{mensajeExito}'}}).then((result) => {{ window.location.href = '{redireccion}'; }});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorCredenciales",
                        "Swal.fire({icon: 'error', title: 'Error', text: 'Usuario o contraseña incorrectos.'});", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorInesperado",
                    $"Swal.fire({{icon: 'error', title: 'Error Inesperado', text: '{ex.Message}'}});", true);
            }

        }
    }
}