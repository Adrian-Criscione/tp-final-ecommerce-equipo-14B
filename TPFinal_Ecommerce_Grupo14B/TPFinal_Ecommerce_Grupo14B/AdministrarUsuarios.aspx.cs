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
        public void cargarDatosPrueba()
        {
            //necesito cargar 5 usuarios de prueba por constructor
            List<Usuario> lista = new List<Usuario>();

            Usuario u1 = new Usuario();
            u1.Id = 1;
            u1.Nombre = "Juan";
            u1.Correo = "Nada@nada";
            u1.Clave = "123";
            u1.Direccion = "Calle falsa 123";
            u1.Telefono = "123456";
            u1.Localidad = "Springfield";
            u1.FechaNacimiento = DateTime.Parse("01/01/1990");
            u1.Estado = true;
            u1.IdRol = 1;

            //agregar ese usuario a la 
            lista.Add(u1);

            //cargar la dgv con la lista
            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();



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