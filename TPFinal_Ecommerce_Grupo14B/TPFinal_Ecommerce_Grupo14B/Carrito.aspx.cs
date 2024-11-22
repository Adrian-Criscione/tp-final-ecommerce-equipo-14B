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
    public partial class Carrito : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos = new List<Articulo>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Recupera el ID desde la sesión
                string id = Session["ProductoId"] as string;

                if (!string.IsNullOrEmpty(id))
                {
                    // Agregar el nuevo artículo al carrito
                    CargaDetallesProducto(id);
                    // Limpiar el ProductoId de la sesión para evitar duplicados
                    Session["ProductoId"] = null;
                }

                // Cargar el carrito para mostrar en el GridView
                CargarDetalleCarito();
                // Calcular y mostrar el total
                CalcularTotal();
            }

        }
        public void CargaDetallesProducto(string id)
        {
            // Recuperar o inicializar la lista de artículos en la sesión
            if (Session["ListaArticulos"] == null)
            {
                Session["ListaArticulos"] = new List<Articulo>();
            }

            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;

            // Recuperar o inicializar el diccionario de cantidades en la sesión
            if (Session["DiccionarioCantidades"] == null)
            {
                Session["DiccionarioCantidades"] = new Dictionary<int, int>();
            }

            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;

            // Buscar el artículo por ID
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = negocio.listarConSP().Find(x => x.Id == Convert.ToInt32(id));

            if (articulo != null)
            {
                // Obtener la cantidad desde la sesión (establecida en DetallesProducto.aspx)
                int cantidad = Convert.ToInt32(Session["CantidadSeleccionada"]);

                // Verificar si el artículo ya está en la lista
                if (!listaArticulos.Any(a => a.Id == articulo.Id))
                {
                    // Agregar el artículo si no existe en la lista
                    listaArticulos.Add(articulo);
                    diccionarioCantidades[articulo.Id] = cantidad;
                }
                else
                {
                    // Incrementar la cantidad en el diccionario si ya existe
                    diccionarioCantidades[articulo.Id] += cantidad;
                }

                // Actualizar la lista y el diccionario en la sesión
                Session["ListaArticulos"] = listaArticulos;
                Session["DiccionarioCantidades"] = diccionarioCantidades;
            }

        }
        public void CargarDetalleCarito()
        {
            // Recuperar la lista y el diccionario desde la sesión
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;

            if (listaArticulos != null && diccionarioCantidades != null)
            {
                // Combinar los datos de la lista de artículos con las cantidades
                var carrito = listaArticulos.Select(a => new
                {
                    a.Id,
                    a.Nombre,
                    a.Precio,
                    Cantidad = diccionarioCantidades[a.Id],
                    StockMaximo = a.Stock,
                    Subtotal = a.Precio * diccionarioCantidades[a.Id]
                }).ToList();


                decimal subtotalGeneral = carrito.Sum(item => item.Subtotal);
                Session["Subtotal"] = subtotalGeneral;

                // Enlazar al GridView
                gvCarrito.DataSource = carrito;
                gvCarrito.DataBind();
            }
            else
            {
                // Limpiar el GridView si no hay artículos
                gvCarrito.DataSource = null;
                gvCarrito.DataBind();
            }
        }


        private void CalcularTotal()
        {
            // Recuperar la lista y el diccionario de cantidades desde la sesión
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;

            decimal total = 0;

            if (listaArticulos != null && diccionarioCantidades != null)
            {
                // Sumar los subtotales de cada artículo
                total = listaArticulos.Sum(a => a.Precio * diccionarioCantidades[a.Id]);
            }

            // Actualizar el lblTotal
            lblTotal.Text = $"Total: ${total.ToString("0.00")}";
        }

        protected void btnSeguirComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            // Intentamos obtener el usuario desde la sesión
            Usuario usuario = (Usuario)Session["usuario"];

            // Verificamos si el usuario es null
            if (usuario == null)
            {
                // Si es null, redirigimos a la página de inicio de sesión
                Response.Redirect("IniciarSesion.aspx");
            }
            else
            {
                // Si el usuario no es null, lo guardamos en la sesión y redirigimos a la página de pagos
                Session["usuarioActual"] = usuario;
                Response.Redirect("Pagos.aspx");
            }
        }
        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtener el ID del artículo desde CommandArgument
            int idArticulo = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Aumentar":
                    AumentarCantidad(idArticulo);
                    break;

                case "Disminuir":
                    DisminuirCantidad(idArticulo);
                    break;

                case "Eliminar":
                    EliminarArticulo(idArticulo);
                    break;
            }

            // Recargar el carrito y calcular el total
            CargarDetalleCarito();
            CalcularTotal();
        }
        private void AumentarCantidad(int idArticulo)
        {
            // Obtener el diccionario de cantidades desde la sesión
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;

            if (diccionarioCantidades != null && listaArticulos != null)
            {
                // Verificar si el artículo está en el diccionario
                if (diccionarioCantidades.ContainsKey(idArticulo))
                {
                    // Obtener el artículo para obtener el stock máximo
                    Articulo articulo = listaArticulos.FirstOrDefault(a => a.Id == idArticulo);

                    if (articulo != null)
                    {
                        // Verificar si la cantidad actual es menor que el stock máximo
                        int cantidadActual = diccionarioCantidades[idArticulo];
                        if (cantidadActual < articulo.Stock)
                        {
                            // Incrementar la cantidad solo si no supera el stock máximo
                            diccionarioCantidades[idArticulo]++;
                            Session["DiccionarioCantidades"] = diccionarioCantidades;
                        }
                        else
                        {
                            // Opcional: Mostrar un mensaje si se intenta exceder el stock
                            Response.Write("No se puede incrementar la cantidad más allá del stock disponible.");
                        }
                    }
                }
            }
        }
        private void DisminuirCantidad(int idArticulo)
        {
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;

            if (diccionarioCantidades != null && diccionarioCantidades.ContainsKey(idArticulo))
            {
                if (diccionarioCantidades[idArticulo] > 1)
                {
                    diccionarioCantidades[idArticulo]--;
                }
                else
                {
                    // Si la cantidad es 1, elimina el artículo del carrito
                    EliminarArticulo(idArticulo);
                }

                Session["DiccionarioCantidades"] = diccionarioCantidades;
            }

        }
        private void EliminarArticulo(int idArticulo)
        {
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;

            if (listaArticulos != null && diccionarioCantidades != null)
            {
                // Eliminar el artículo de ambas estructuras
                listaArticulos.RemoveAll(a => a.Id == idArticulo);
                diccionarioCantidades.Remove(idArticulo);

                // Actualizar la sesión
                Session["ListaArticulos"] = listaArticulos;
                Session["DiccionarioCantidades"] = diccionarioCantidades;
            }

        }
    }
}