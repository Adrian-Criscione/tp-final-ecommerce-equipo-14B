<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReporteProductos.aspx.cs" Inherits="TPFinal_Ecommerce_Grupo14B.ReporteProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <h1 class="mb-4">Dashboard de Reportes</h1>

        <!-- Fila para las tarjetas de reportes -->
        <div class="row mb-4">
            <div class="col-md-4">
                <div class="card text-white bg-primary mb-3">
                    <div class="card-header">Productos Totales</div>
                    <div class="card-body">
                        <h5>

                            <asp:Label ID="lblTotalProductos" runat="server" Text=""></asp:Label>
                        </h5>
                        <p class="card-text">Cantidad distinta de productos en stock.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white bg-success mb-3">
                    <div class="card-header">Clientes Registrados</div>
                    <div class="card-body">
                        <h5>
                            <asp:Label ID="lblCantidaddeClientes" runat="server" Text=""></asp:Label>
                        </h5>
                        <p class="card-text">Clientes registrados.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white bg-secondary mb-3">
                    <div class="card-header">Total Activo</div>
                    <div class="card-body">
                        <h5>
                            <asp:Label ID="lblValorActivosEmpresa" runat="server" Text=""></asp:Label>
                        </h5>
                        <p class="card-text">Valor total en pesos.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white bg-warning mb-3">
                    <div class="card-header">Cantidad productos vendidos</div>
                    <div class="card-body">
                        <h5>
                            <asp:Label ID="lblCantidadProductosVendidos" runat="server" Text="10"></asp:Label>
                        </h5>
                        <p class="card-text">Total productos vendidos.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white bg-info mb-3">
                    <div class="card-header">Total ventas</div>
                    <div class="card-body">
                        <h5>
                            <asp:Label ID="lblValorTotalVentas" runat="server" Text="$ 10"></asp:Label>
                        </h5>
                        <p class="card-text">Valor total en pesos de ventas.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-white mb-3" style="background-color: #ff6a00">
                    <div class="card-header">Productos bajo stock</div>
                    <div class="card-body">
                        <h5>
                            <asp:Label ID="lblProductosBajoStock" runat="server" Text="10"></asp:Label>
                        </h5>
                        <p class="card-text">Productos con menos de 3 unidades.</p>
                    </div>
                </div>
            </div>

            <!-- Fila que contiene la GridView y el gráfico -->
            <div class="row mb-4 " style="background-color: #eef1e5; color: #000;">
                <!-- Columna para la GridView -->
                <div class="col-md-6">
                    <h2>Productos con stock menor a 10</h2>
                    <div style="max-height: 400px; overflow-y: auto;">
                        <asp:GridView
                            ID="gvPocoStock"
                            runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <!-- Columna para el gráfico con un offset -->
                <div class="col-md-4 offset-md-2">
                    <!-- Espacio adicional con offset -->
                    <h2>Productos por categoria</h2>
                    <!-- Aquí se colocará el gráfico -->
                    <canvas id="ventasChart" width="400" height="400"></canvas>
                </div>



            </div>
                <!--Grafico Lineal-->
            <div class="row mb-4 " style="background-color: #d7ebf2; color: #000;>
                <div class="mt-4>
                    <h2>Gráfico de Ventas</h2>
                    <!-- Canvas para el gráfico lineal -->
                    <canvas id="ventasAnualChart"></canvas>
                </div>

            </div>



            <!-- Scrip para el grafico -->
            <script>
                // Obtener el contexto del elemento canvas
                var ctx = document.getElementById('ventasChart').getContext('2d');

                // Crear el gráfico
                var ventasChart = new Chart(ctx, {
                    type: 'pie', // Tipo de gráfico (puedes cambiarlo a 'line', 'pie', etc.)
                    data: {
                        labels: ['Remeras', 'Skate', 'Colectivos', 'Indumentaria', 'Juguetes'], // Etiquetas del eje X
                        datasets: [{
                            label: 'Ventas en USD',
                            data: [12000, 15000, 8000, 19000, 23000], // Datos del eje Y
                            backgroundColor: [
                                'rgba(54, 162, 235, 0.5)',
                                'rgba(75, 192, 192, 0.5)',
                                'rgba(255, 206, 86, 0.5)',
                                'rgba(153, 102, 255, 0.5)',
                                'rgba(255, 99, 132, 0.5)'
                            ],
                            borderColor: [
                                'rgba(54, 162, 235, 1)',
                                'rgba(75, 192, 192, 1)',
                                'rgba(255, 206, 86, 1)',
                                'rgba(153, 102, 255, 1)',
                                'rgba(255, 99, 132, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            </script>


            <script>
                // Obtén el contexto del canvas
                var ctx = document.getElementById('ventasAnualChart').getContext('2d');

                // Datos de ejemplo para los últimos 12 meses
                var labels = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
                var ventasData = [120, 150, 180, 130, 170, 210, 250, 300, 280, 240, 200, 220]; // Reemplaza con tus datos reales

                // Configura el gráfico lineal
                var ventasChart = new Chart(ctx, {
                    type: 'line', // Tipo de gráfico
                    data: {
                        labels: labels, // Etiquetas para los meses
                        datasets: [{
                            label: 'Cantidad de Ventas',
                            data: ventasData, // Datos de ventas
                            backgroundColor: 'rgba(54, 162, 235, 0.2)', // Color de fondo de las líneas
                            borderColor: 'rgba(54, 162, 235, 1)', // Color del borde
                            borderWidth: 2 // Ancho de la línea
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true // Iniciar el eje Y desde cero
                            }
                        }
                    }
                });
            </script>

             <asp:Button Text="Volver" ID="btnVolver" CssClass="btn btn-primary" OnClick="btnVolver_Click" runat="server" />

</asp:Content>