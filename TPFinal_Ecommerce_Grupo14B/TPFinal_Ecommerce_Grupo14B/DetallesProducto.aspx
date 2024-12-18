﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetallesProducto.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.DetallesProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6">
                <!-- Imagen del Producto -->
                <asp:Image ID="imgProducto" runat="server" CssClass="img-fluid product-image" Width="414" Height="320" AlternateText='<%# Eval("Nombre") %>' ImageUrl='<%# Eval("UrlImagen") %>' />
            </div>
            <div class="col-md-6 product-details">
                <!-- Información del Producto -->
                <div>
                    <asp:Label ID="lblNombre" runat="server" CssClass="product-name"><%# Eval("Nombre") %></asp:Label>
                </div>
                <div class="mt-2">
                    <asp:Label ID="lblID" CssClass="product-description" runat="server"><%# Eval("Id") %></asp:Label>
                </div>
                <div class="mt-2">
                    <asp:Label ID="lblDescripcion" CssClass="product-description" runat="server"><%# Eval("Descripcion") %></asp:Label>
                </div>
                <div class="mt-3">
                    
                    <asp:Label ID="lblPrecio" CssClass="product-price" runat="server" Text='<%# "Precio: $" + Eval("Precio") %>'></asp:Label>
                </div>
                <div class="mt-4">
                    <asp:Button Text="Agregar al Carrito" runat="server" ID="btnAgregarCarrito" CssClass="btn btn-primary mt-3 w-100" OnClick="btnAgregarCarrito_Click" CommandArgument='<%# Eval("Id") %>' CommandName="ArticuloId" />
                </div>
                <div class="mt-4">
                    <a href="Productos.aspx" class="back-link">← Volver a la tienda</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
