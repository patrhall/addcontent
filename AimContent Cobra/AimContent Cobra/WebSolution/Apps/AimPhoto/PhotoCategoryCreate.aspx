<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="PhotoCategoryCreate.aspx.cs" Inherits="AimPhoto.PhotoCategoryCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">     
<div>	                        
    <table border="0">
        <tr><td><asp:Label ID="lblDate" runat="server" /></td><td><asp:TextBox ID="tbCategoryDate" Width="300px" runat="server" /></td></tr>
        <tr><td><asp:Label ID="lblCategory" runat="server" /></td><td><asp:TextBox ID="tbCategoryName" Width="300px" runat="server" /></td></tr>
        <tr><td class="align_topleft"><asp:Label ID="lblComment" runat="server" /></td><td><asp:TextBox ID="tbCComment" Width="300px" TextMode="multiLine" Rows="5" runat="server" /></td></tr>
    </table> 
    <asp:Button ID="btCategoryName" runat="server" OnClick="btCategoryName_Click" />
    <asp:Label ID="lblMsg" runat="server" CssClass="error" /><br /><br />        
    <asp:HyperLink ID="hpBack" runat="server" />
</div>
</asp:Content>
