<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="LiveStats.aspx.cs" Inherits="AimStats.LiveStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <asp:HyperLink ID="statsLink" runat="server" Visible="false" Text="Klicka här..." Target="_blank">
            <img src="stats_icon.gif" style="border-width:0px;" alt="" />
            <asp:Literal ID="litLinktext" runat="server" />
        </asp:HyperLink>
    </div>
</asp:Content>
