﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- Sección de productos más vendidos -->
    <div id="productos-mas-vendidos" class="container mt-5">
        <h1 class="text-center mb-4">Los más vendidos !!</h1>
        <div id="productosCarousel" class="carousel slide" data-bs-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <div class="row">
                        <asp:Repeater ID="repRepetidor" runat="server">
                            <ItemTemplate>
                                <div class="col-md-4">
                                    <div class="card">
                                        <img src="<%#Eval("UrlImagen") %>" width="414" height="320" class="card-img-top" alt="<%#Eval("Nombre") %>">
                                        <div class="card-body">
                                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                            <p class="card-text"><%#Eval("Descripcion") %></p>
                                            <asp:LinkButton ID="btnVerDetalle" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' OnClick="btnVerDetalle_Click">Ver Más</asp:LinkButton>
                                            <%--<asp:Button Text="Comprar" runat="server" ID="btnComprarDestacados" CssClass="btn btn-primary" OnClick="btnComprarDestacados_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="carousel-item">
                    <div class="row">
                        <asp:Repeater ID="repRepetidorDos" runat="server">
                            <ItemTemplate>
                                <div class="col-md-4">
                                    <div class="card">
                                        <img src="<%#Eval("UrlImagen") %>" width="414" height="320" class="card-img-top" alt="<%#Eval("Nombre") %>">
                                        <div class="card-body">
                                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                            <p class="card-text"><%#Eval("Descripcion") %></p>
                                            <asp:LinkButton ID="btnVerDetalle" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' OnClick="btnVerDetalle_Click">Ver Más</asp:LinkButton>
                                            <%--<asp:Button Text="Comprar" runat="server" ID="btnComprarDestacados" CssClass="btn btn-primary" OnClick="btnComprarDestacados_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#productosCarousel" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Previous</span>
            </button>

            <button class="carousel-control-next" type="button" data-bs-target="#productosCarousel" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Next</span>
            </button>
        </div>
    </div>
</asp:Content>
