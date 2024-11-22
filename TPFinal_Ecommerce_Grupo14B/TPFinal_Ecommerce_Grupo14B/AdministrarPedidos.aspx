<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdministrarPedidos.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.AdministrarPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Administrar Pedidos</h1>
        <asp:GridView ID="gvAdminPedidos" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="gvAdminPedidos_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID Pedido" />
                <asp:BoundField DataField="UsuarioId" HeaderText="UsuarioId" />
                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                <asp:BoundField DataField="FechaPedido" HeaderText="Fecha Pedido" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:BoundField DataField="NameStatus" HeaderText="Estado Detalle" />
                <asp:BoundField DataField="DireccionEnvio" HeaderText="Dirección Envío" />
                <asp:ButtonField ButtonType="Button" CommandName="CambiarEstado" Text="Cambiar Estado"  />
            </Columns>
        </asp:GridView>
         <asp:Button Text="Volver" runat="server" CssClass="btn btn-primary " ID="btnVolver" OnClick="btnVolver_Click" />
    </div>
</asp:Content>
