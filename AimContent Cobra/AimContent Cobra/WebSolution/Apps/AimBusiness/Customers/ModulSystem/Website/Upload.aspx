﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Upload.aspx.cs" Inherits="AimBusiness.Customers.ModulSystem.Website.Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <asp:FileUpload ID="fuExcel" runat="server" />
        <br />
        <asp:Button ID="btnImport" runat="server" Text="Importera!" onclick="btnImport_Click" />
        <p><asp:Label ID="lblMsg" runat="server" ForeColor="Red" /></p>
    </div>
</asp:Content>
