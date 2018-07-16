<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="EditTagTreeObject.aspx.cs" Inherits="AimTags.EditTagTreeObject" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<br />
<br />
<table>
    <tr>
        <td>
            Tillgängliga taggar
        </td>
        <td>
            Valda taggar
        </td>
    </tr>
    <tr>
        <td>
        <rad:RadListBox ID="rlbAvailableTags" runat="server" Width="300px" Height="500px"
            SelectionMode="Multiple" AllowTransfer="true" TransferToID="rlbSelectedTags" AutoPostBackOnTransfer="true"
            AllowReorder="false" AutoPostBackOnReorder="false" EnableDragAndDrop="true" />
        </td>
        <td>
            <rad:RadListBox ID="rlbSelectedTags" runat="server" Width="300px" Height="500px"
            SelectionMode="Multiple" AllowReorder="false" AutoPostBackOnReorder="false" EnableDragAndDrop="true" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSaveTags" runat="server" Text="Spara taggar" OnClick="btnSaveTags_Click" />
        </td>
        <td>
            
        </td>
    </tr>
</table>
</asp:Content>