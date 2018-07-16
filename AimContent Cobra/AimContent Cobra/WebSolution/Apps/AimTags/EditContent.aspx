<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true"
    CodeBehind="EditContent.aspx.cs" Inherits="AimTags.EditContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/Tags.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <table class="editcontenttable">
        <asp:Panel ID="pnlContentType" runat="server">
        <tr>
            <td>                
                Innehållstyp
            </td>
            <td>
                <asp:DropDownList ID="ddlContentType" runat="server" OnSelectedIndexChanged="ddlContentType_SelectedIndexChanged" AutoPostBack="true" />
            </td>
        </tr>
        </asp:Panel>
        <asp:Panel ID="pnlForm" runat="server" Visible="false">
        <tr>
            <td>
                Namn
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server" /><asp:RequiredFieldValidator ID="reqName" runat="server" ErrorMessage="Du måste fylla i namn" ControlToValidate="tbName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Beskrivning/URL
            </td>
            <td>
                <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Rows="15" Columns="60" />
            </td>
        </tr>
         <tr>
            <td>
                Sökord
            </td>
            <td>
                <asp:TextBox ID="tbSearchWords" runat="server" TextMode="MultiLine" Rows="5" Columns="60" />
            </td>
        </tr>
        <asp:Panel ID="pnlHtml" runat="server" Visible="false">
        <tr>
            <td>
                Html
            </td>
            <td>
                <rad:RadEditor ID="editor1" runat="server" />
            </td>
        </tr>
        </asp:Panel>
        <asp:Panel ID="pnlFile" runat="server" Visible="false">       
        <tr>
            <td>
                Ladda upp fil
            </td>
            <td>
                <asp:FileUpload ID="fuFile" runat="server" />
            </td>
        </tr>         
        <asp:Panel ID="pnlCurrentFile" runat="server">
        <tr>
            <td>
                Nuvarande fil
            </td>
            <td>
               <asp:Label ID="lblCurrentFile" runat="server" />
            </td>
        </tr>       
        </asp:Panel>
        </asp:Panel>
        <tr>
            <td>
                Taggar
            </td>
            <td>
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
                            <rad:RadListBox ID="rlbAvailableTags" runat="server" Width="400px" Height="300px"
                                SelectionMode="Multiple" AllowTransfer="true" TransferToID="rlbSelectedTags"
                                AutoPostBackOnTransfer="false" AllowReorder="false" AutoPostBackOnReorder="false"
                                EnableDragAndDrop="true" />
                        </td>
                        <td>
                            <rad:RadListBox ID="rlbSelectedTags" runat="server" Width="400px" Height="300px"
                                SelectionMode="Multiple" AllowReorder="false" AutoPostBackOnReorder="false" EnableDragAndDrop="true" />
                        </td>
                    </tr>                    
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" />
            </td>
        </tr>
        </asp:Panel>
    </table>
</asp:Content>
