﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <h2 class="mb-4">Carrito de Compras</h2>

        <!-- Tabla de productos en el carrito -->
        <div class="table-responsive">
            <table class="table">
                <thead class="table-light">
                    <tr>
                        <th scope="col">Producto</th>
                        <th scope="col">Precio</th>
                        <th scope="col">Cantidad</th>
                        <th scope="col">Subtotal</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody id="carrito">

                    <tr>
                        <td>Producto 1</td>
                        <td>$50.00</td>
                        <td>
                            <asp:TextBox runat="server" CssClass="form-control w-50" ID="txtCantidad" />
                        </td>
                        <td>$50.00</td>
                        <td>
                            <asp:Button Text="Eliminar" CssClass="btn btn-danger btn-sm" ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!-- Total de la compra -->
        <div class="row">
            <div class="col-md-6">
                <h3 id="total">Total: $200.00</h3>
            </div>
            <div class="col-md-6 text-end">
               
                <asp:Button Text="Seguir Comprando" ID="btnSeguirComprando" CssClass="btn btn-outline-primary" runat="server" OnClick="btnSeguirComprando_Click" />
                <asp:Button Text="Pagar" CssClass="btn btn-success" ID="btnPagar" runat="server" OnClick="btnPagar_Click" />
            </div>
        </div>
    </div>

</asp:Content>
