<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="AimBusiness.Customers.Tjuge22.Website.Overview" ValidateRequest="false" %>
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
                <asp:LinkButton ID="lbNewCase" ForeColor="blue" Font-Underline="true" runat="server" CommandName="new" Text="Lägg till ny bild" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridBoundColumn DataField="Name" HeaderText="Namn" />
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Enabled='<%# ButtonVisible(Eval("Position").ToString(), true) %>' CommandName="up" Text="Upp" />
                    </ItemTemplate>
                </rad:GridTemplateColumn>
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Enabled='<%# ButtonVisible(Eval("Position").ToString(), false) %>' CommandName="down" Text="Ner" />
                    </ItemTemplate>
                </rad:GridTemplateColumn>
                <rad:GridButtonColumn CommandName="editCase" ButtonType="ImageButton" ImageUrl="/Images/edit.gif" />
                <rad:GridButtonColumn CommandName="deleteCase" ButtonType="ImageButton" ImageUrl="/Images/delete.gif" />
            </Columns>
        </MasterTableView>
    </rad:RadGrid>
    <p><asp:Label ID="lblDescriptionLayout" runat="server" Text="Här bestäms hur bilderna ska visas på sidan. Välj antal rader och kolumner som ska visas per sida." /></p>
    <asp:Label ID="lblRows" runat="server" Text="Antal rader:" />
    <asp:TextBox ID="tbRows" runat="server" Width="30px" />
    <asp:Label ID="lblColumns" runat="server" Text="Antal kolumner:" />
    <asp:TextBox ID="tbColumns" runat="server" Width="30px" />
    <br />
    <p><asp:Label ID="lblDescriptionFont" runat="server" Text="Välj vilken font som ska användas för texterna på sidan. Obeservera att användaren måste ha fonten installerad på sin dator för att kunna se den. Välj därför helst vanligare standard-fonter. Om en användare inte har fonten installerad visas texterna i användarens default-font. Välj också de olika font-storlekarna." /></p>
    <table>
        <tr>
            <td><asp:Label ID="lblFontName" runat="server" Text="Fontnamn:" /></td>
            <td><asp:TextBox ID="tbFontName" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTitleFontsize" runat="server" Text="Storlek på titlarna:" /></td>
            <td><asp:TextBox ID="tbTitleSize" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDescriptionFontsize" runat="server" Text="Storlek på beskrivningarna:" /></td>
            <td><asp:TextBox ID="tbDescSize" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblFontColor" runat="server" Text="Färg på texterna:" /></td>
            <td>
                <asp:TextBox id="tbFontColor" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="tbFontColor" runat="server" Text="Färgen måste vara i formatet 0xXXXXXX där X är en hexadecimal siffra mellan 0-F." ValidationExpression="^0x[0-9A-F]{6}$" ValidationGroup="settings" />
            </td>
        </tr>
    </table>
    <br />
    <p><asp:Label ID="lblDescriptionSettings" runat="server" Text="Välj övriga inställningar. Inzoomad distans och utzoomad distans är känslan av avståndet till bilden i de olika lägena. Om detta värdet minskas så känns det som om bilden kommer närmare och tvärtom." /></p>
    <table>
        <tr>
            <td><asp:Label ID="lblDescriptionZoomedInDistance" runat="server" Text="Inzoomad distans:" /></td>
            <td>
                <asp:TextBox ID="tbZoomedInDistance" runat="server" />
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDescriptionZoomedOutDistance" runat="server" Text="Utzoomad distans:" /></td>
            <td>
                <asp:TextBox ID="tbZoomedOutDistance" runat="server" />
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDescriptionSound" runat="server" Text="Ljud aktiverat:" /></td>
            <td>
                <asp:RadioButtonList runat="server" ID="rblSound" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Ja" Value="true" Selected="True" />
                    <asp:ListItem Text="Nej" Value="false" />
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:Button ID="btnSaveSettiongs" runat="server" Text="Spara inställningar" OnClick="btnSaveSettings_OnClick" ValidationGroup="settings" />
    </asp:Panel>
    <asp:Panel ID="pnlEditCase" runat="server" Visible="false">
        <table>
            <tr>
                <td><asp:Label ID="lblTitleUpload" runat="server" Text="Ladda upp ny bild" /></td>
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

