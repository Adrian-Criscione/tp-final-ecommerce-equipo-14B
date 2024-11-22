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

    public partial class AdministrarUsuarios : System.Web.UI.Page
    {
        UsuarioNegocio UsuarioNegocio = new UsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();

            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }
        
        public void chequearUsuarios()
        {
            if (Session["idRol"] == null)
            {
                Response.Redirect("IniciarSesion.aspx");
            }
            else
            {
                int idRol = Convert.ToInt32(Session["idRol"]);

                // Verificar si el usuario no es el ID 1
                if (idRol != 1)
                {
                    // Script para mostrar el mensaje de error y redirigir
                    string script = @"
            Swal.fire({
                title: 'Acceso denegado',
                text: 'No tiene permisos para ingresar a esta página.',
                icon: 'error',
                confirmButtonText: 'Aceptar'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = 'Default.aspx';
                }
            });";

                    // Registrar el script para que se ejecute en el cliente
                    ClientScript.RegisterStartupScript(this.GetType(), "PermisosDenegados", script, true);

                    // Salir del flujo normal de la página
                    return;
                }
            }
        }
        
        public void CargarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            lista = UsuarioNegocio.listar();
            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdministracionGeneral.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/FormularioUsuario.aspx");
        }

        protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvUsuarios.SelectedDataKey.Value.ToString();

            Response.Redirect("/FormularioUsuario.aspx?id=" + id);

        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text;
            List<Usuario> lista = UsuarioNegocio.listar();
            lista = lista.Where(u => u.Nombre.Contains(filtro) || u.Correo.Contains(filtro)).ToList(); 
            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();

        }
    }
}