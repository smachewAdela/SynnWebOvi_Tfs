﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="DepositsPage.aspx.cs" Inherits="WebSimplify.DepositsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="spageheader" id="acc">נתוני הפקדות</div>

    <div class="spanel">
        <div class="row">
            <div class=" col-3">
                <div class="sinputwlabel">
                    <label for="txpSumm">סך כל ההפקדות שלך</label>
                    <asp:TextBox ID="txpSumm" runat="server" />
                </div>
            </div>
            <div class="col-3">
                <div class="sinputwlabel">
                    <label for="txpBalance">יתרתך הנוכחית</label>
                    <asp:TextBox ID="txpBalance" runat="server" />
                </div>
            </div>
              <div class="col-3">
                <div class="sinputwlabel">
                    <label for="txpBalance"> סה"כ נדרש</label>
                    <asp:TextBox ID="txRequired" runat="server" />
                </div>
            </div>
            <div class=" col-3">
                <div class="sinputwlabel">
                    <label for="txpUpToDate"> נכון לתאריך  </label>
                    <asp:TextBox ID="txpUpToDate" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div class="row" id="dvDisplay" runat="server">
		<div class="spanel">
			<div class="sgridcontainer">
				<asp:GridView ID="gvMyDeposit" runat="server" AllowPaging="true"
					OnRowDataBound="gvMyDeposit_RowDataBound"  OnPageIndexChanging="gvMyDeposit_PageIndexChanging"
					CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
					AutoGenerateColumns="false" >
					<Columns>
                        <asp:TemplateField HeaderText=" מפקיד" ControlStyle-CssClass="">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblexec"  ></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText=" אסמכתא" ControlStyle-CssClass="">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblMessage"  ></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						  <asp:TemplateField HeaderText=" סכום">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblId"></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="תאריך ">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblDate"></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>
		</div>
	</div>

     <div class="row" id="dvUserDisplay" runat="server">
		<div class="spanel">
			<div class="sgridcontainer">
				<asp:GridView ID="gvUsersDeposits" runat="server" AllowPaging="true"
					OnRowDataBound="gvUsersDeposits_RowDataBound"  
					CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
					AutoGenerateColumns="false" >
					<Columns>
                        <asp:TemplateField HeaderText=" מפקיד" ControlStyle-CssClass="">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblexecv"></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText=" סך הפקדות" ControlStyle-CssClass="">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblTotal"></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						  <asp:TemplateField HeaderText="יתרה">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblBal"></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="נכון לתאריך ">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblDatev"></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>
		</div>
	</div>
</asp:Content>
