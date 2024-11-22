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
    public partial class Productos : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }
        
        public List<Categoria> listaCategorias { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listarConSP();


            if (!IsPostBack)
            {
                CargarCategorias();
                CargarProductos();
            }


        }
        private void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            listaCategorias = categoriaNegocio.listar(); // Método para obtener categorías
            repCategorias.DataSource = listaCategorias;
            repCategorias.DataBind();
        }
        private void CargarProductos()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            listaArticulos = articuloNegocio.listarConSP(); // Método para obtener productos
            repRepetidor.DataSource = listaArticulos;
            repRepetidor.DataBind();
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            
            LinkButton btnVerDetalle = (LinkButton)sender;
            string id = btnVerDetalle.CommandArgument;

            // Guarda el id en la sesión
            
            Session.Add("ProductoId", id);
            

            // Redirige a la página de detalles
            Response.Redirect("DetallesProducto.aspx");
        }

        protected void filtrarPorCategoria_Click(object sender, EventArgs e)
        {
            LinkButton btnCategoria = (LinkButton)sender;
            int idCategoria = int.Parse(btnCategoria.CommandArgument);

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            listaArticulos = articuloNegocio.listarPorCategoria(idCategoria); // Método para filtrar productos por categoría
            repRepetidor.DataSource = listaArticulos;
            repRepetidor.DataBind();
        }
    }
}