<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoinDetails.aspx.cs" Inherits="CoinMarketProject.CoinDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Coin Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="log" runat="server" Text=""></asp:Label>
    <div class="container">
        <asp:GridView ID="CoinLogsView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="CoinName" HeaderText="Coin Adı" />
                <asp:BoundField DataField="PurchasePrice" HeaderText="Alış Fiyatı" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="Quantity" HeaderText="Miktar" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="CurrentPrice" HeaderText="Anlık Fiyat" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="ProfitLoss" HeaderText="Kâr/Zarar" DataFormatString="{0:F4}" />
                <asp:BoundField DataField="ProfitLossPercentage" HeaderText="Kâr/Zarar (%)" DataFormatString="{0:F4}" />
            </Columns>
        </asp:GridView>
        <label for="YearsInput">Yıl:</label>
        <input type="number" id="YearsInput" max="4" />
        <button class="btn button-primary" type="button" onclick="updateChart()">Güncelle</button>
        <asp:HiddenField ID="CoinCodeHiddenField" runat="server" />
        <asp:HiddenField ID="HistoricalPricesHiddenField" runat="server" />
        <canvas id="priceChart" width="800" height="400"></canvas>
    </div>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chartjs-adapter-moment/0.1.0/chartjs-adapter-moment.min.js"></script>
    <script type="text/javascript">
        function updateChart() {
            var years = document.getElementById('YearsInput').value;
            var url = new URL(window.location.href);
            url.searchParams.set('years', years);
            window.location.href = url;
        }

        $(document).ready(function () {
            var years = new URLSearchParams(window.location.search).get('years');
            document.getElementById('YearsInput').value = years;

            var historicalPrices = JSON.parse('<%= HistoricalPricesHiddenField.Value %>');
            var coinCode = '<%= CoinCodeHiddenField.Value %>';
            console.log(historicalPrices);

            var labels = [];
            var data = [];

            historicalPrices.forEach(function (item) {
                labels.push(moment(item.time_period_start).format('YYYY-MM-DD'));
                data.push(item.rate_close);
            });

            var ctx = document.getElementById("priceChart").getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Price (USD)',
                        data: data,
                        borderColor: 'rgba(75, 192, 192, 1)',
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderWidth: 2,
                        pointRadius: 3,
                        pointHoverRadius: 5,
                        fill: true
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    title: {
                        display: true,
                        text: coinCode,
                        fontSize: 18
                    },
                    scales: {
                        xAxes: [{
                            type: 'time',
                            time: {
                                unit: 'month',
                                tooltipFormat: 'll',
                                displayFormats: {
                                    month: 'MMM YYYY'
                                }
                            },
                            ticks: {
                                fontSize: 12,
                                maxRotation: 0,
                                minRotation: 0
                            },
                            gridLines: {
                                display: true,
                                color: "rgba(0, 0, 0, 0.1)"
                            }
                        }],
                        yAxes: [{
                            ticks: {
                                beginAtZero: false,
                                fontSize: 12
                            },
                            gridLines: {
                                display: true,
                                color: "rgba(0, 0, 0, 0.1)"
                            }
                        }]
                    },
                    legend: {
                        display: true,
                        labels: {
                            fontSize: 14
                        }
                    },
                    tooltips: {
                        enabled: true,
                        mode: 'index',
                        intersect: false,
                        bodyFontSize: 12,
                        titleFontSize: 14
                    },
                    hover: {
                        mode: 'nearest',
                        intersect: true
                    }
                }
            });
        });
    </script>
</asp:Content>
