<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="AimBusiness.Customers.AimFor2022.Website.Overview" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        td{vertical-align:top;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <asp:Panel ID="pnlShowCases" runat="server">
    <rad:RadGrid ID="rgCases" runat="server" AutoGenerateColumns="false" OnItemCommand="rgCases_OnItemCommand">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id">
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNewCase" ForeColor="blue" Font-Underline="true" runat="server" CommandName="new" Text="Skapa nytt case" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridBoundColumn DataField="CaseName" HeaderText="Namn" />
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Enabled='<%# ButtonVisible(Eval("Position").ToString(), true) %>' CommandName="up" Text="Upp" />
                    </ItemTemplate>
                </rad:GridTemplateColumn>
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Enabled='<%# ButtonVisible(Eval("Position").ToString(), false) %>' CommandArgument="down" Text="Ner" />
                    </ItemTemplate>
                </rad:GridTemplateColumn>
                <rad:GridButtonColumn CommandName="editCase" ButtonType="ImageButton" ImageUrl="/Images/edit.gif" />
                <rad:GridButtonColumn CommandName="deleteCase" ButtonType="ImageButton" ImageUrl="/Images/delete.gif" />
            </Columns>
        </MasterTableView>
    </rad:RadGrid>
    <p><asp:Label ID="lblDescriptionLayout" runat="server" Text="Här bestäms hur casen ska visas på sidan. Välj antal rader och kolumner som ska visas per sida." /></p>
    <asp:Label ID="lblRows" runat="server" Text="Antal rader:" />
    <asp:TextBox ID="tbRows" runat="server" Width="30px" />
    <asp:Label ID="lblColumns" runat="server" Text="Antal kolumner:" />
    <asp:TextBox ID="tbColumns" runat="server" Width="30px" />
    <asp:Button ID="btnSaveSettiongs" runat="server" Text="Spara inställningar" OnClick="btnSaveSettings_OnClick" />
    </asp:Panel>
    <asp:Panel ID="pnlEditCase" runat="server" Visible="false">
        <table>
            <tr>
                <td><asp:Label ID="lblTitleUpload" runat="server" Text="Ladda upp nytt case" /></td>
                <td><asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="/images/arrow_left_grey.gif" OnClick="imgBtnCancel_OnClick" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblDescription" runat="server" Text="Fyll i formuläret och ladda upp en bild." /></td>
            </tr>
        </table>
        <table>
            <tr>
                <td><asp:Label ID="lblTitleCaseName" runat="server" Text="Namn:" /></td>
                <td><asp:TextBox ID="tbCaseName" runat="server" Width="400px" /></td>
                <td>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbCaseName" Display="Dynamic" ErrorMessage="Du måste fylla i ett namn" ValidationGroup="formCreateCase" />
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblTitelDescription" runat="server" Text="Beskriving:" /></td>
                <td>
                    <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Rows="6" Width="400" />
                </td>
                <td>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="formCreateCase" ControlToValidate="tbDescription" ErrorMessage="Du måste fylla i en beskrivning" />
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblTitleOriginalImage" runat="server" Text="Bild:" /></td>
                <td><asp:FileUpload ID="fuOriginalImage" runat="server" Width="405px" /></td>
                <td>
                    <asp:Label ID="lblErrorNoFile" runat="server" Visible="false" ForeColor="Red" Text="Du måste ladda upp en bild." EnableViewState="false" />
                    <asp:Label ID="lblErrorWrongExtension" runat="server" Visible="false" ForeColor="Red" Text="Bilden måste vara i rätt format: .jpg, .jpeg, .gif, eller .png" EnableViewState="false" />
                    <asp:Label ID="lblErrorWrongSize" runat="server" Visible="false" ForeColor="Red" Text="Bildens bredd och höjd måste vara minst 320 pixlar" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;"><asp:Button style="margin-right:5px;" ValidationGroup="formCreateCase" ID="btnUpload" runat="server" Text="Spara case" OnClick="btnUpload_OnClick" /></td>
                <td />
            </tr>
            </table>
    </asp:Panel>
    <asp:HiddenField ID="hfEdit" Value="-1" runat="server" />
    <asp:HiddenField ID="hfId" runat="server" />
</asp:Content>

