<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="CalendarEditItem.aspx.cs" Inherits="AimCalendar.CalendarEditItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Text="Titel" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbTitle" Width="500px" CssClass="tbcal" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;<asp:HyperLink ID="hlReturn" Text="&lt;img src='/Images/arrow_left_grey.gif' alt='Gå tillbaka till listan' /&gt;" runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCalendar" Text="Datum" CssClass="title" runat="server"></asp:Label>
                </td>
                <td> 
                    <rad:RadDateTimePicker ID="rdpDate" runat="server"></rad:RadDateTimePicker>
                    <asp:Label Visible="false" ID="lblError" CssClass="title" ForeColor="Red" runat="server"></asp:Label>
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="lblDescription" Text="Beskrivning" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbDescription" Width="500px" Height="100px" TextMode="MultiLine" CssClass="tbcal" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShow" Text="Visa" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="cbShow" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblisMarkt" Text="Markerad" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="cbisMarkt" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_OnClick" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
