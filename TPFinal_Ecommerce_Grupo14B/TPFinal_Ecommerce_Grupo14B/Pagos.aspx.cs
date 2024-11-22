using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinal_Ecommerce_Grupo14B
{
    public partial class Pagos : System.Web.UI.Page
    {
        PedidoNegocio pedidoNegocio = new PedidoNegocio();
        UsuarioNegocio usuarionegocio = new UsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            TraerUsuario();
        }

        public void TraerUsuario()
        {
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            int idArticulo = 1;

            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["usuarioActual"];
        }

        //OnClick="ConfirmPago_Click"
        protected void ConfirmPago_Click(object sender, EventArgs e)
        {
            string numeroTarjetaInput = numeroTarjeta.Text.Trim();
            string nombreTarjetaInput = nombreTarjeta.Text.Trim();
            string cvvInput = cvv.Text.Trim();
            string fechaExpiracionInput = txtfechaExpiracion.Text.Trim();


            if (string.IsNullOrEmpty(numeroTarjetaInput) || numeroTarjetaInput.Length != 16 || !numeroTarjetaInput.All(char.IsDigit))
            {
                MostrarSweetAlert("Error", "El número de tarjeta debe contener 16 dígitos numéricos.");
                return;
            }

            if (string.IsNullOrEmpty(nombreTarjetaInput) || !System.Text.RegularExpressions.Regex.IsMatch(nombreTarjetaInput, @"^[a-zA-Z\s]+$"))
            {
                MostrarSweetAlert("Error", "El nombre en la tarjeta debe contener solo letras.");
                return;
            }

            if (string.IsNullOrEmpty(cvvInput) || cvvInput.Length != 3 || !cvvInput.All(char.IsDigit))
            {
                MostrarSweetAlert("Error", "El CVV debe contener 3 dígitos numéricos.");
                return;
            }

            if (string.IsNullOrEmpty(fechaExpiracionInput) || !System.Text.RegularExpressions.Regex.IsMatch(fechaExpiracionInput, @"^\d{4}$"))
            {
                MostrarSweetAlert("Error", "La fecha de expiración debe contener exactamente 4 dígitos: 2 para el mes y 2 para el año (MMYY).");
                return;
            }
            int mes = int.Parse(fechaExpiracionInput.Substring(0, 2));
            int anio = int.Parse(fechaExpiracionInput.Substring(2, 2));
            if (mes < 1 || mes > 12)
            {
                MostrarSweetAlert("Error", "El mes en la fecha de expiración debe estar entre 01 y 12.");
                return;
            }

            if (envioDomicilio.Checked && string.IsNullOrEmpty(codigoPostal.Text.Trim()))
            {
                MostrarSweetAlert("Error", "Por favor, ingrese el Código Postal para el envío a domicilio.");
                return;
            }

            GuardarPedido(); // Lógica para procesar el pedido
            MostrarSweetAlert("Éxito", "El pago se ha procesado correctamente.");

        }

        private void MostrarSweetAlert(string titulo, string mensaje)
        {
            string script = $"Swal.fire({{title: '{titulo}', text: '{mensaje}', icon: '{(titulo == "Éxito" ? "success" : "error")}'}});";
            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }

        public void GuardarPedido()
        {
            Pedido pedido = new Pedido();
            // Recuperar datos de la sesión
            List<Articulo> listaArticulos = Session["ListaArticulos"] as List<Articulo>;
            Dictionary<int, int> diccionarioCantidades = Session["DiccionarioCantidades"] as Dictionary<int, int>;
            Usuario usuario = Session["usuarioActual"] as Usuario;

            if (listaArticulos == null || diccionarioCantidades == null || usuario == null)
            {
                // Manejar caso donde falten datos
                throw new InvalidOperationException("Faltan datos en la sesión para procesar el pedido.");
            }
            

          // Calcular el subtotal usando una expresión lambda
          decimal subtotal = listaArticulos
              .Where(a => diccionarioCantidades.ContainsKey(a.Id)) // Asegurarse de que el artículo esté en el diccionario
              .Sum(a => a.Precio * diccionarioCantidades[a.Id]);
            // Datos del pedido
            int idCarrito = usuario.Id; // Generar o asignar el ID del carrito según corresponda
            int idUsuario = usuario.Id;
            DateTime fecha = DateTime.Now;

            // Aquí puedes agregar lógica para guardar el pedido en la base de datos
            // por ejemplo, creando una instancia de Pedido o utilizando un servicio.

            // Ejemplo: Mostrar datos del pedido

            usuario.Direccion = usuarionegocio.BuscarUsuarioPorId(idUsuario).Direccion;
            pedido.UsuarioId = idUsuario;
            pedido.CarritoId = idCarrito;
            pedido.Total = subtotal;
            pedido.FechaPedido = fecha;
            pedido.Estado = 1;
            pedido.DireccionEnvio = usuario.Direccion;

            pedidoNegocio.InstertarPedido(pedido);

            // Descontar stock de cada artículo
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            foreach (var articulo in listaArticulos)
            {
                if (diccionarioCantidades.TryGetValue(articulo.Id, out int cantidadVendida))
                {
                    int stockDisponible = articuloNegocio.ObtenerStockDisponible(articulo.Id);
                    if (stockDisponible >= cantidadVendida)
                    {
                        articuloNegocio.ActualizarStock(articulo.Id, cantidadVendida);
                    }
                    else
                    {
                        throw new InvalidOperationException($"No hay suficiente stock para el artículo con ID {articulo.Id}. Stock disponible: {stockDisponible}, cantidad requerida: {cantidadVendida}.");
                    }
                }
            }




            //poner todos los valores en cero del carrito que ya se cargo la venta
            Session["ListaArticulos"] = null;
            Session["DiccionarioCantidades"] = null;
            listaArticulos = null;
            diccionarioCantidades = null;
            usuario = null;

            MostrarSweetAlert("Éxito", "El pedido se procesó correctamente y el stock fue actualizado.");


        }

        protected void envioDomicilio_CheckedChanged(object sender, EventArgs e)
        {
            
            codigoPostal.Visible = true;
            lblCodigoPostal.Visible = true;
        }

        protected void retiroLocal_CheckedChanged(object sender, EventArgs e)
        {
            codigoPostal.Visible = false;
            lblCodigoPostal.Visible = false;
        }
    }
}