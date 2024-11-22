<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5">
        <h2 class="mb-4">Proceso de Pago</h2>

        <div class="form-section">
            <h4>Método de Pago</h4>
            <div class="form-check">
                <input class="form-check-input" type="radio" name="metodoPago" id="pagoTarjetaCredito" value="tarjetaCredito" onclick="mostrarPagoTarjeta()" checked>
                <label class="form-check-label" for="pagoTarjetaCredito">
                    Tarjeta de Crédito
                   
                </label>
            </div>
            <div class="form-check">
                <input class="form-check-input" type="radio" name="metodoPago" id="pagoTarjetaDebito" value="tarjetaDebito" onclick="mostrarPagoTarjeta()">
                <label class="form-check-label" for="pagoTarjetaDebito">
                    Tarjeta de Débito
                   
                </label>

                <!-- Campos para ingresar los datos de la tarjeta -->
                <div id="datosTarjeta" class="mt-3">
                    <div class="mb-3">
                        <label for="numeroTarjeta" class="form-label">Número de Tarjeta</label>
                        <asp:TextBox ID="numeroTarjeta" runat="server" CssClass="form-control" placeholder="Ingrese el número de tarjeta"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="nombreTarjeta" class="form-label">Nombre en la Tarjeta</label>
                        <asp:TextBox ID="nombreTarjeta" runat="server" CssClass="form-control" placeholder="Ingrese el nombre como aparece en la tarjeta"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="cvv" class="form-label">CVV</label>
                        <asp:TextBox ID="cvv" runat="server" CssClass="form-control" placeholder="Código de seguridad (CVV)"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="fechaExpiracion" class="form-label">Fecha de Expiración</label>
                        <input type="text" class="form-control" id="fechaExpiracion" placeholder="MM/AA">
                    </div>
                </div>
            </div>

            <!-- Sección de método de entrega -->
            <div class="form-section">
                <h4>Método de Entrega</h4>
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="metodoEntrega" id="envioDomicilio" value="envioDomicilio" onclick="mostrarDireccionEnvio()">
                    <label class="form-check-label" for="envioDomicilio">
                        Envío a Domicilio
                   
                    </label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="metodoEntrega" id="retiroLocal" value="retiroLocal" onclick="ocultarDireccionEnvio()" checked>
                    <label class="form-check-label" for="retiroLocal">
                        Retiro en el Local
                   
                    </label>
                </div>

                <!-- Sección para agregar dirección si elige envío a domicilio -->
                <div id="direccionEnvio" class="mt-3" style="display: none;">
                    <div class="mb-3">
                        <label for="direccion" class="form-label">Dirección de Envío</label>
                        <input type="text" class="form-control" id="direccion" placeholder="Ingrese su dirección">
                    </div>
                    <div class="mb-3">
                        <label for="ciudad" class="form-label">Ciudad</label>
                        <input type="text" class="form-control" id="ciudad" placeholder="Ingrese su ciudad">
                    </div>
                </div>

                <!-- Campo para el código postal (se muestra en ambos métodos de entrega) -->
                <div class="mb-3">
                    <label for="codigoPostal" class="form-label">Código Postal</label>
                    <input type="text" class="form-control" id="codigoPostal" placeholder="Ingrese su código postal">
                </div>
            </div>
            <!-- Botones-->
            <div class="form-section">
                <asp:Button ID="btnConfirmarPago" runat="server" Text="Confirmar Pago" CssClass="btn btn-primary" OnClick="ConfirmPago_Click" />
                <a href="Carrito.aspx" class="btn btn-secondary">Volver al Carrito</a>
            </div>
        </div>
    </div>
</asp:Content>
