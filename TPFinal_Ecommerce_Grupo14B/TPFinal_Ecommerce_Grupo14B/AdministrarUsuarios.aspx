﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.AdministrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Administrar Usuarios</h1>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped table-bordered table-hover" OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged" HeaderStyle-CssClass="table-dark">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                    <asp:BoundField DataField="Clave" HeaderText="Clave" SortExpression="Clave" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" SortExpression="Direccion" />
                    <asp:BoundField DataField="Localidad" HeaderText="Localidad" SortExpression="Localidad" />
                    <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento" SortExpression="FechaNacimiento" />
                    <asp:CheckBoxField HeaderText="Estado" DataField="Estado" SortExpression="Estado" />
                    <asp:BoundField DataField="IDRol" HeaderText="Rol" SortExpression="IDRol" />
                    <asp:CommandField ShowSelectButton="True" SelectText="✍️" ButtonType="Button" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <asp:Button Text="Agregar" ID="btnAgregar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" runat="server" />
        <asp:Button Text="Volver" ID="btnVolver" CssClass="btn btn-primary" OnClick="btnVolver_Click" runat="server" />
    </div>
</asp:Content>
