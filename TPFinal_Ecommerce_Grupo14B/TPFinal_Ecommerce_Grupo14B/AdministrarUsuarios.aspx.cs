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
            if (Session["idRol"] == null)
            {
                Response.Redirect("IniciarSesion.aspx");
            }
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }
        //cargar 5 datos de prueba
        
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