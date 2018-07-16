<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InteriorUpload.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="AimBusiness.Customers.ModulSystem.Website.InteriorUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <asp:DropDownList ID="ddSelectedLanguage" runat="server" /><br />
        <asp:FileUpload ID="fuExcel" runat="server" />
        <br />
        <asp:Button ID="btnImport" runat="server" Text="Importera" OnClick="btnImport_Click" />
        <p>
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" /></p>
    </div>
</asp:Content>
