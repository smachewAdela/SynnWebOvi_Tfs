﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Shifts.aspx.cs" Inherits="WebSimplify.Shifts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">יומן משמרות</div>
    <div class="spanel">
        <div class="spanelHeader"> הוספת משמרת   <i class="fa fa-address-card"></i></div>

        <div >
            <input type="date" name="name" id="txadddiarydate" placeholder="תאריך" runat="server" />
        </div>
        <div>
            <asp:DropDownList id="cmbShifts" placeholder=" משמרת" runat="server" />
        </div>
        <button class="sbutton sbutton-sm" type="button" id="btnAddShift" runat="server" onserverclick="btnAddShift_ServerClick">הוסף</button>
    </div>
    <div class="spanel">
        <asp:Calendar ID="cdr" runat="server" FirstDayOfWeek="Sunday" Width="100%" OnDayRender="Calendar1_DayRender"></asp:Calendar>
    </div>
</asp:Content>