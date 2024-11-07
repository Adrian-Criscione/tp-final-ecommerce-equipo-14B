﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdministrarArticulos.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.AdministrarArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Administrar Artículos</h1>
    <asp:GridView ID="gvArticulos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" HeaderStyle-CssClass="table-dark">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
            <asp:BoundField DataField="CategoriaId" HeaderText="Categoria" SortExpression="Categoria" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
        </Columns>
    </asp:GridView>
     <div class="col">
     <asp:Button Text="Agregar" ID="btnAgregar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" runat="server" />
     <asp:Button Text ="Editar" ID="btnEditar" CssClass="btn btn-secondary" runat="server" />
     <asp:Button Text="Eliminar" ID="btnEliminar" CssClass="btn btn-danger" runat="server" />
 </div>
</asp:Content>
