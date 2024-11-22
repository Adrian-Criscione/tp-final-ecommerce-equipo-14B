<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5">
        <h2 class="mb-4">Proceso de Pago</h2>

        <div class="row">
            <div class="col-md-6">
                <div class="card shadow-sm mb-4">
                    <div class="card-body">
                        <h4 class="card-title">Método de Pago</h4>

                        <div class="form-check mb-3">
                            <input class="form-check-input" type="radio" name="metodoPago" id="pagoTarjetaCredito" value="tarjetaCredito" onclick="mostrarPagoTarjeta()" checked>
                            <label class="form-check-label" for="pagoTarjetaCredito">
                                Tarjeta de Crédito
                            </label>
                        </div>
                        <div class="form-check mb-3">
                            <input class="form-check-input" type="radio" name="metodoPago" id="pagoTarjetaDebito" value="tarjetaDebito" onclick="mostrarPagoTarjeta()">
                            <label class="form-check-label" for="pagoTarjetaDebito">
                                Tarjeta de Débito
                   
                            </label>
                        </div>

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
                                <asp:TextBox ID="txtfechaExpiracion" runat="server" CssClass="form-control" placeholder="MM/AA"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Sección de método de entrega -->
            <div class="col-md-6">
                <div class="card shadow-sm mb-4">
                    <div class="card-body">
                        <h4 class="card-title">Método de Entrega</h4>
                        <div class="form-check mb-3">
                            <asp:RadioButton ID="envioDomicilio" runat="server" GroupName="metodoEntrega" Text="Envío a Domicilio" AutoPostBack="true"
                                OnCheckedChanged="envioDomicilio_CheckedChanged" />
                        </div>
                        <div class="form-check mb-3">
                            <asp:RadioButton ID="retiroLocal" runat="server" GroupName="metodoEntrega" Text="Retiro en el Local" AutoPostBack="true"
                             Checked="true" OnCheckedChanged="retiroLocal_CheckedChanged" />
                        </div>

                        <!-- Campo para el código postal (se muestra en ambos métodos de entrega) -->
                        <div class="mb-3">
                            <asp:Label ID="lblCodigoPostal" CssClass="form-label" runat="server"
                                Text="Código Postal" Visible="false"></asp:Label>
                            <asp:TextBox ID="codigoPostal" runat="server" CssClass="form-control"
                                Placeholder="Ingrese su código postal" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Botones-->
        <div class="text-center mt-4">
            <asp:Button ID="btnConfirmarPago" runat="server" Text="Confirmar Pago" CssClass="btn btn-primary" OnClick="ConfirmPago_Click" />
            <a href="Carrito.aspx" class="btn btn-secondary">Volver al Carrito</a>
        </div>
    </div>
</asp:Content>
