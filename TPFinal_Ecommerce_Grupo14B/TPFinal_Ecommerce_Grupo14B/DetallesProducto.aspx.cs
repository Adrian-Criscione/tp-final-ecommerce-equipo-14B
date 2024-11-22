using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Ecommerce_Grupo14B
{
    public partial class DetallesProducto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["usuario"] == null)
            //{

            //    Response.Redirect("IniciarSesion.aspx");
            //}
            //else
            //{

                if (!IsPostBack)
                {

                    string id = Session["ProductoId"] as string;


                    if (!string.IsNullOrEmpty(id))
                    {

                        CargaDetallesProducto(id);
                    }
                    else
                    {
                        Response.Write("No se encontró el ID del producto en la sesión.");
                    }

                }
                //}
                
            }
        public void CargaDetallesProducto(string id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = negocio.listarConSP().Find(x => x.Id == Convert.ToInt32(id));
            lblNombre.Text = articulo.Nombre;
            lblID.Text = articulo.Id.ToString();
            lblDescripcion.Text = articulo.Descripcion;
            lblPrecio.Text = articulo.Precio.ToString();
            imgProducto.ImageUrl = articulo.UrlImagen;
        }
        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {


            int cantidadSeleccionada = 1;

            Session["CantidadSeleccionada"] = cantidadSeleccionada;


            Session["ProductoId"] = lblID.Text;


            Response.Redirect("Carrito.aspx");

        }

    }
}