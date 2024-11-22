using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace TPFinal_Ecommerce_Grupo14B
{
    public partial class AdministrarPedidos : System.Web.UI.Page
    {
        PedidoNegocio pedidoNegocio = new PedidoNegocio();
        List<Pedido> listaPedidos = new List<Pedido>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Evitar recargar datos en cada postback
            {
                CargarPedido();
            }
        }

        public void CargarPedido()
        {
            listaPedidos = pedidoNegocio.Listar(); // Cargar pedidos desde la capa de negocio
            gvAdminPedidos.DataSource = listaPedidos;
            gvAdminPedidos.DataBind();
        }

        protected void gvAdminPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado") // Comprobar que se presionó el botón correcto
            {
                // Recuperar el índice de la fila seleccionada
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Obtener la fila del GridView
                GridViewRow row = gvAdminPedidos.Rows[rowIndex];

                // Obtener el ID del pedido desde la primera columna
                int pedidoId = Convert.ToInt32(row.Cells[0].Text); // Asegúrate de que la columna corresponde al ID

                // Cambiar el estado del pedido llamando al método de la capa de negocio
                bool exito = pedidoNegocio.CambiarEstadoPedido(pedidoId);

                CargarPedido(); // Recargar datos para reflejar cambios

            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdministracionGeneral.aspx");
        }
    }
}
