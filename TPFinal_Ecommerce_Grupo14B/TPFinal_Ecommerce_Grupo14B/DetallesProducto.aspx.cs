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

                ChequearStock();
            }
                //}
                
            }

        public void ChequearStock()
        {
            try
            {
            ArticuloNegocio negocio = new ArticuloNegocio();
            int stock = negocio.ObtenerStockDisponible(int.Parse(lblID.Text));
                if (stock == 0)
                {
                    btnAgregarCarrito.Enabled = false;
                    btnAgregarCarrito.Text = "Artículo sin stock.";
                    btnAgregarCarrito.CssClass = "btn btn-warning";
                }
                else
                {
                    btnAgregarCarrito.Enabled = true;
                    btnAgregarCarrito.Text = "Agregar al carrito";
                    btnAgregarCarrito.CssClass = "btn btn-primary";
                }

            }
            catch (Exception ex)
            {

                MostrarError("Ocurrió un error al verificar el stock: " + ex.Message);
            }
        }
        private void MostrarError(string mensaje)
        {
            string script = $"Swal.fire({{title: 'Error', text: '{mensaje}', icon: 'error'}});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", script, true);
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