<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Default.aspx">Home</a></li>
                <li class="breadcrumb-item"><a href="Productos.aspx">Productos</a></li>
            </ol>
        </nav>
        <h1 class="text-center mb-4">Productos</h1>
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater ID="repRepetidor" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100">
                            <asp:Image
                                ID="imgArticulo" runat="server" CssClass="card-img-top" Width="414px" Height="320px" ImageUrl='<%# string.IsNullOrWhiteSpace(Eval("UrlImagen") as string) ? "https://img.freepik.com/vector-premium/retro-efecto-texto-vintage-anos-70-80-editables-estilo-texto-retro-clasico_546273-329.jpg?w=740" : Eval("UrlImagen") %>' AlternateText='<%# Eval("Nombre") %>' />
                            <div class="card-body">
                                <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                <p class="card-text">$ <%#Eval("Precio") %></p>
                                <asp:LinkButton ID="btnVerDetalle" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' OnClick="btnVerDetalle_Click">Ver Más</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
