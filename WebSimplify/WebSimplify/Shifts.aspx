<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Shifts.aspx.cs" Inherits="WebSimplify.Shifts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">יומן משמרות</div>


    <div class="row">
        <div class="col-3"></div>
        <div class="spanel spanelmin col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvAdd" runat="server"
                    OnRowDataBound="gvAdd_RowDataBound" CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText=" תאריך " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txDate" TextMode="Date" CssClass="griddateinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" משמרת" ControlStyle-CssClass="griddatecell">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="cmbShifts" CssClass="griddropinput" ></asp:DropDownList>
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
        <div class="col-3"></div>
    </div>
     <div class="spanel">
        <div class="sgridcontainer">
            <asp:GridView ID="gvShifts" runat="server"
                OnRowDataBound="gvShifts_RowDataBound" 
                CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                AutoGenerateColumns="false" >
                <Columns>
                    <asp:TemplateField HeaderText="תאריך" ControlStyle-CssClass="">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDate"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="יום" ControlStyle-CssClass="">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDay"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText=" משמרת">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblShift"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="הסר משמרת">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnDelete" OnCommand="btnDelete_Command" CssClass="gridbutton" ImageUrl="Img/dlt.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="row">
     
    </div>
</asp:Content>
