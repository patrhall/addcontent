<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="AimForm.Form" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">    
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 680px;">
        <tr class="bread">
            <td>
                Formulärnamn:
                <asp:TextBox ID="tbFormName" Width="200" runat="server" CssClass="inputs"></asp:TextBox><br />
                <br /><asp:HyperLink ID="hlReturnToList" Text="Återvänd till listan >>" runat="server" NavigateUrl="/Apps/AimForm/FormList.aspx"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="headerstyle" valign="top" colspan="5" height="20">
                Lägg till/redigera beskrivningen till kontaktformuläret
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <rad:radeditor haspermission="true" language="en-US" id="editor1"
                    width="100%" height="300px" runat="server" onsubmitclicked="editor1_Submit">
                    </rad:radeditor>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" id="pnlExternalConnection" Visible="false">                
                    <br />
                    <br />
                    <hr />
                    <b><asp:Label ID="lblExternalConnectionTitle" runat="server" /></b><br />
                    <asp:Label ID="lblExternalConnectionDescription" runat="server" />                    
                    <br />
                    <asp:CheckBox ID="cbExternalConnection" runat="server" Text="Koppling aktiv" />
                    <br />
                    <asp:DropDownList ID="ddlExternalConnections" OnSelectedIndexChanged="UpdateProperties" AutoPostBack="true" runat="server"></asp:DropDownList>
                    <br />
                    <br />
                    <asp:PlaceHolder ID="phExternalConnectionsAttributes" runat="server"></asp:PlaceHolder>
                    <asp:Repeater ID="rptExternalConnectionsAttributes" runat="server" OnItemDataBound="pung">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfPropId" runat="server" />
                            <asp:Label ID="lblPropertyName" runat="server"></asp:Label>
                            <br />                            
                            <asp:TextBox ID="tbPropertyValue" runat="server"></asp:TextBox>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                    <hr />
                    <br />
                    <br />
                </asp:Panel>
                <br />
                
            </td>
        </tr>
        <tr>
            <td class="headerstyle" valign="top" colspan="5" height="20">
                Mailadresser
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnMail" runat="server" CssClass="inputs" Text="Mailadresser" OnClick="btnMail_Click" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="headerstyle" valign="top" colspan="5" height="20">
                Lägg till/redigera beskrivningen till kontaktformuläret
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="phForm" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table>
                    <tr>
                        <td style="width: 350px;">
                            <asp:Button ID="btnAdd" runat="server" CssClass="inputs" Text="Lägg till post" Visible="true"
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Spara formulär" CssClass="inputs" OnClick="btnSave_Click" />
            </td>
        </tr>
        <!--<tr>
                <td>
                    <asp:Label ID="lblRole" runat="server" Width="680px" CssClass="headerstyle" Visible="false">Välj roller för formulärsida:<br /></asp:Label>
                    <asp:CheckBoxList CssClass="normal" ID="cblRoleProperty" runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>-->
    </table>
</asp:Content>
