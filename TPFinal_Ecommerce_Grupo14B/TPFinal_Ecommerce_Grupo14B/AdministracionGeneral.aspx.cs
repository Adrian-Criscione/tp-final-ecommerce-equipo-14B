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
    public partial class AdministracionGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idRol"] == null)
            {
                Response.Redirect("IniciarSesion.aspx");
            }
            int idRol = (int)Session["idRol"];

            if (idRol == 1)
            {
                btnAdminUsuarios.Visible = true;
                btnAdminReportes.Visible = true;
            }
        }

        protected void btnAdminUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdministrarUsuarios.aspx");
        }

        protected void btnAdminProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdministrarArticulos.aspx");
        }

        protected void btnAdminCategorias_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdministrarCategorias.aspx");
        }

        protected void btnAdminReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ReporteProductos.aspx");
        }

        protected void btnConfiguracion_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdminPedidos_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdministrarPedidos.aspx");
        }
    }
}