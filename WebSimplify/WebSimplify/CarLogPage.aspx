<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="CarLogPage.aspx.cs" Inherits="WebSimplify.CarLogPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">יומן רכבים</div>
    <div class="menubuttoncontainer" id="dvWorkHours" runat="server">
        <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="" Visible="true" />
    </div>

    <div class="row" id="trdic" runat="server">
        <div class=" col-3">
        </div>
        <div class=" col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvAdd" runat="server"
                    OnRowDataBound="gvAdd_RowDataBound" CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="שם מסמך" AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txName" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תיאור">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txIdesc" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" רכב" ControlStyle-CssClass="griddatecell">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="cmbCars" CssClass="griddropinput"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="  ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnSearch" OnCommand="btnSearch_Command" CssClass="gridbutton" ImageUrl="Img/search.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוספה ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAdd" OnCommand="btnAdd_Command" CssClass="gridbutton" ImageUrl="Img/add.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>


        </div>


        <div class=" col-3">
        </div>

    </div>

    <div class="spanel">
        <div class="sgridcontainer">
            <asp:GridView ID="gvDocs" runat="server"
                OnRowDataBound="gvDocs_RowDataBound" AllowPaging="true" OnPageIndexChanging="gvDocs_PageIndexChanging"
                CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText=" שם מסמך">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblName"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="תיאור מסמך">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDescription"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" רכב" ControlStyle-CssClass="">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCar"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="עריכה">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="btnDownload" OnCommand="btnDownload_Command" CssClass="gridbutton" ImageUrl="Img/download.png"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>


</asp:Content>
