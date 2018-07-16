<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="PhotoUpload.aspx.cs" Inherits="AimPhoto.PhotoUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server"> 
    <rad:RadUpload ID="ruUpload" runat="server" />
    <rad:RadProgressManager ID="progressManager" runat="server" DisplayCancelButton="True" DisplayTimeData="True" />
    <rad:RadProgressArea ID="RadProgressArea1" runat="server" />   
    
    <p><asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" /></p>
    <p><asp:HyperLink ID="hpBack" runat="server" NavigateUrl="javascript:history.back(-1);" /></p>
</asp:Content>