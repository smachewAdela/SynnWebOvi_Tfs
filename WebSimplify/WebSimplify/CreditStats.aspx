﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="CreditStats.aspx.cs" Inherits="WebSimplify.CreditStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader" id="acc">נתוני אשראי</div>

    <div class="spanel">
        <div class="row">
            <div class=" col-4">
                <div class="sinputwlabel">
                    <label for="txp1">ממוצע לחיוב חודשי</label>
                    <asp:TextBox ID="txp1" runat="server" />
                </div>
            </div>
            <div class="col-4">
                <div class="sinputwlabel">
                    <label for="txp2">ממוצע לחיוב יומי</label>
                    <asp:TextBox ID="txp2" runat="server" />
                </div>
            </div>
            <div class=" col-4">
                <div class="sinputwlabel">
                    <label for="txp3">צפי לחודש נוכחי</label>
                    <asp:TextBox ID="txp3" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class=" col-2"></div>
        <div class="col-8">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gv" runat="server"
                    OnRowDataBound="gv_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="10" OnPageIndexChanging="gv_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText=" חודש">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonthName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סכום נוכחי" AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txCurrentTotal" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוצאה יומית">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDaylyValue"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="צפי לסוף חודש">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonthlyPredictions"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" עדכן סכום">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAction" OnCommand="btnAction_Command" CssClass="gridbutton" ImageUrl="Img/exchange.png" AlternateText="עדכן"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סגירת חודש ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCloseMonth" OnCommand="btnCloseMonth_Command" CssClass="gridbutton" ImageUrl="Img/locked.png" AlternateText="סגור חודש"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class=" col-2"></div>
    </div>
    <div class="row" runat="server" id="ichartcontainer">
        <div class=" col-2"></div>
        <div id="chart" class="col-8">
        </div>
        <div class=" col-2"></div>
    </div>

    <script type="text/javascript">
        google.charts.load('current', { packages: ['corechart', 'line'] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'הוצאות אשראי חודשיות כוללות',
                legend: { position: "none" }
            };
            $.ajax({
                type: "POST",
                url: "CreditStats.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.LineChart($("#chart")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }

        //$(window).resize(function () {
        //    drawChart();
        //});
    </script>



</asp:Content>
