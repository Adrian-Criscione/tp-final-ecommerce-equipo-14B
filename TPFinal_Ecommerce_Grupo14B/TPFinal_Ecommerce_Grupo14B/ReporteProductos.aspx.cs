using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Ecommerce_Grupo14B
{
    public partial class ReporteProductos : System.Web.UI.Page
    {
        ReporteNegocio reporteNegocio = new ReporteNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            chequearUsuarios();

            if (!IsPostBack)
            {
                CargarTotaldeProductos();
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
        protected void CargarTotaldeProductos()
        {
            int totalProductos = reporteNegocio.CantidadArticulos();
            lblTotalProductos.Text = totalProductos.ToString();
            lblCantidaddeClientes.Text = reporteNegocio.CantidadClientes().ToString();
            lblValorActivosEmpresa.Text = "$" + " " + reporteNegocio.ValorActivosEmpresa().ToString();
            gvPocoStock.DataSource = reporteNegocio.listar();
            gvPocoStock.DataBind();

     
            
        }
        protected void gvPocoStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPocoStock.PageIndex = e.NewPageIndex;
            CargarTotaldeProductos(); 
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdministracionGeneral.aspx");
        }
    }
}