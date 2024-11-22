<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.IniciarSesion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container d-flex justify-content-center align-items-center vh-100">
        <div class="card p-4 shadow-sm" style="max-width: 400px; width: 100%;">
            <h3 class="text-center mb-4">Iniciar Sesión</h3>


            <div class="mb-3">
                <label for="email" class="form-label">Correo Electrónico</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" type="email" />
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" type="password" />
            </div>
            <div class="mb-3 text-end">
                <a href="/RecuperarContrasenia.aspx" class="link-primary">¿Olvidaste tu contraseña?</a>
            </div>
            <asp:Button Text="Iniciar Sesión" CssClass="btn btn-primary w-100" ID="btnIniciarSesion" OnClick="btnIniciarSesion_Click" runat="server" />
            <div class="mt-3 text-center">
                <p>¿No tienes una cuenta? <a href="/registro.aspx">Regístrate aquí</a></p>
            </div>
        </div>
    </div>

</asp:Content>
