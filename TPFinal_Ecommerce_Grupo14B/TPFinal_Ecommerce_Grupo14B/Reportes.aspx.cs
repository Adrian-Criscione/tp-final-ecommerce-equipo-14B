﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Ecommerce_Grupo14B
{
    public partial class Reportes : System.Web.UI.Page
    {
        CategoriaNegocio negocio = new CategoriaNegocio();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarCategorias();
            }
        }

        private void ListarCategorias(string filtro = "")
        {
            // Obtenemos la lista de categorías del negocio
            List<Categoria> listaCategorias = negocio.listar();

            // Si el filtro no está vacío, filtramos las categorías por el nombre
            if (!string.IsNullOrEmpty(filtro))
            {
                // Filtramos las categorías por nombre (en minúsculas) para que no importe el caso
                listaCategorias = listaCategorias.FindAll(c => c.Nombre.ToLower().Contains(filtro));
            }

            // Asignamos la lista de categorías al GridView
            gvCategoria.DataSource = listaCategorias;
            gvCategoria.DataBind();
        }


        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.ToLower();
            ListarCategorias(filtro);
        }

        protected void gvCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = Convert.ToInt32(gvCategoria.SelectedDataKey.Value);

            Response.Redirect("FormularioCategoria.aspx?id=" + idCategoria);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/FormularioCategoria.aspx");
        }
    }
}

