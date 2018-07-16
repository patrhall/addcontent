<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Overview.aspx.cs" Inherits="AimBusiness.Customers.ProceduralRacing.Website.Overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        td{vertical-align:top;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<table><tr><td>
<table runat="server" id="table1">
<tr><td>Datum</td><td>
    <rad:RadDatePicker ID="RadDatePicker1" runat="server">
    </rad:RadDatePicker>
</td></tr>
<tr><td>Tid</td><td>
    <rad:RadTimePicker ID="RadTimePicker1" runat="server">
    </rad:RadTimePicker>
</td></tr>
<tr><td>Rubrik</td><td><asp:TextBox ID="txtRubrik" runat="server"></asp:TextBox></td></tr>
<tr><td>Paket</td><td><asp:TextBox ID="txtPaket" runat="server"></asp:TextBox></td></tr>
<tr><td>Grupp</td><td><asp:TextBox ID="txtGrupp" runat="server"></asp:TextBox></td></tr>
<tr><td>Bild</td><td>
    <asp:Image ID="Image1" Width="50px" Height="50px" runat="server" />
<asp:FileUpload ID="fuOriginalImage" runat="server" Width="405px" /></td></tr>
<tr><td>Pris</td><td><asp:TextBox ID="txtPris" runat="server"></asp:TextBox></td></tr>
<tr><td>Lagersaldo</td><td><asp:TextBox ID="txtLagersaldo" runat="server"></asp:TextBox></td></tr>
<tr><td>Sortering</td><td><asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox></td></tr>
<tr><td></td><td><asp:Button ID="btnSave" OnClick="btnSave_OnClick" runat="server" Text="Spara" /></td></tr>
</table>
</td><td valign="top"><rad:RadGrid ID="rgCases" runat="server" AutoGenerateColumns="false" OnItemCommand="rgCases_OnItemCommand">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ArticleID">
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNewCase" ForeColor="blue" Font-Underline="true" runat="server" CommandName="new" Text="Lägg till ny" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridBoundColumn DataField="ArticleID" HeaderText="Artikel" />
                <rad:GridBoundColumn DataField="Datum" HeaderText="Datum" />
                <rad:GridBoundColumn DataField="Rubrik" HeaderText="Rubrik" />
                <rad:GridButtonColumn CommandName="editCase" ButtonType="ImageButton" ImageUrl="/Images/edit.gif" />
                <rad:GridButtonColumn CommandName="deleteCase" ButtonType="ImageButton" ImageUrl="/Images/delete.gif" />
            </Columns>
        </MasterTableView>
    </rad:RadGrid></td></tr></table>
<asp:TextBox ID="txtId" Visible="false" runat="server"></asp:TextBox>
</asp:Content>
