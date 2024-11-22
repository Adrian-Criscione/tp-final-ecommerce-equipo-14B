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
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }
        public List<Articulo> listaArticulosDos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //int categoriaID = 1;
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listarHome();
            listaArticulosDos = negocio.listarHome();

            if (!IsPostBack)
            {
                repRepetidor.DataSource = listaArticulos;
                repRepetidor.DataBind();
                repRepetidorDos.DataSource = listaArticulosDos;
                repRepetidorDos.DataBind();
            }

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
    }
}