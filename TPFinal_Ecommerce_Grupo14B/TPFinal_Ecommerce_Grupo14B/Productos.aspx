<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <!-- Breadcrumb -->
        <nav aria-label="breadcrumb" class="mb-4">
            <ol class="breadcrumb">
                <li class="breadcrumb-item">
                    <a href="Default.aspx" class="text-decoration-none">Home</a>
                </li>
                <li class="breadcrumb-item active" aria-current="page">Productos</li>
            </ol>
        </nav>

        <!-- Título de la página -->
        <h1 class="text-center mb-5">Productos Retro</h1>

        <!-- Contenedor principal -->
        <div class="row">

            <aside class="col-md-3">
                <div class="card shadow-sm">
                    <div class="card-header text-center fw-bold">
                        Categorías
                    </div>
                    <div class="card-body p-0">
                        <ul class="list-group list-group-flush">
                            <asp:Repeater ID="repCategorias" runat="server">
                                <ItemTemplate>
                                    <li class="list-group-item">
                                        <asp:LinkButton
                                            ID="lnkCategoria"
                                            runat="server"
                                            CssClass="text-decoration-none text-dark fw-semibold"
                                            CommandArgument='<%# Eval("Id") %>'
                                            OnClick="filtrarPorCategoria_Click">
                                <%# Eval("Nombre") %>
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </aside>


            <!-- Columna principal: Productos -->
            <main class="col-md-9">
                <div class="row g-4">
                    <asp:Repeater ID="repRepetidor" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6">
                                <div class="card h-100 shadow-sm">
                                    <!-- Imagen responsiva con límites definidos -->
                                    <asp:Image
                                        ID="imgArticulo"
                                        runat="server"
                                        CssClass="card-img-top img-fluid"
                                        ImageUrl='<%# string.IsNullOrWhiteSpace(Eval("UrlImagen") as string) ? "https://img.freepik.com/vector-premium/retro-efecto-texto-vintage-anos-70-80-editables-estilo-texto-retro-clasico_546273-329.jpg?w=740" : Eval("UrlImagen") %>'
                                        AlternateText='<%# Eval("Nombre") %>' />
                                    <!-- Contenido del card -->
                                    <div class="card-body text-center">
                                        <h5 class="card-title text-truncate"><%# Eval("Nombre") %></h5>
                                        <p class="card-text">$ <%# Eval("Precio") %></p>
                                        <asp:LinkButton
                                            ID="btnVerDetalle"
                                            runat="server"
                                            CssClass="btn btn-retro w-100"
                                            CommandArgument='<%# Eval("Id") %>'
                                            OnClick="btnVerDetalle_Click">
                                Ver Detalle
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </main>

        </div>
    </div>
</asp:Content>
