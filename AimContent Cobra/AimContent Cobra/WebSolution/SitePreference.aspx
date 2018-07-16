<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SitePreference.aspx.cs"
    Inherits="WebSolution.SitePreference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <fieldset style="width: 470px;">
        <legend>
            <asp:Label ID="lblMetaHeadTitle" runat="server" CssClass="title" /></legend>
        <p>
            <asp:Label ID="lblMetaHeadDescription" runat="server" /></p>
        <p>
            <asp:Label ID="lblMetaTitle" runat="server" /><br />
            <asp:TextBox ID="tbMetaTitle" runat="server" Width="450px" MaxLength="255" />
        </p>
        <p>
            <asp:Label ID="lblMetaKeywords" runat="server" /><br />
            <asp:TextBox ID="tbMetaKeywords" runat="server" Width="450px" MaxLength="1000" />
        </p>
        <p>
            <asp:Label ID="lblMetaDescription" runat="server" /><br />
            <asp:TextBox ID="tbMetaDescription" runat="server" Width="450px" TextMode="MultiLine"
                Rows="3" MaxLength="1000" />
        </p>
        <p>
            <asp:Button ID="btMetaSave" runat="server" OnClick="btMetaSave_OnClick" />
            <asp:Label ID="lblMetaMessage" CssClass="message" runat="server" />
        </p>
    </fieldset>
    <br />
    <fieldset style="width: 470px;">
        <legend>
            <asp:Label ID="lblPrePreferenceTitle" runat="server" CssClass="title" /></legend>
        <p>
            <asp:Label ID="lblPrePreferenceDescription" runat="server" /></p>
        <table>
            <tr>
                <td class="lightgray">
                    <asp:Label ID="lblPrePreferenceSiteUrl" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPrePreferenceSiteUrlValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lightgray">
                    <asp:Label ID="lblPrePreferenceMaxFilesize" runat="server" />
                </td>
                <td class="white">
                    <asp:Label ID="lblPrePreferenceMaxFilesizeValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lightgray">
                    <asp:Label ID="lblPrePreferenceLcid" runat="server" />
                </td>
                <td class="white">
                    <asp:Label ID="lblPrePreferenceLcidValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lightgray">
                    <asp:Label ID="lblPrePreferenceUseAdminRoles" runat="server" />
                </td>
                <td class="white">
                    <asp:Label ID="lblPrePreferenceUseAdminRolesValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lightgray">
                    <asp:Label ID="lblPrePreferenceSecureEditing" runat="server" />
                </td>
                <td class="white">
                    <asp:Label ID="lblPrePreferenceSecureEditingValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lightgray">
                    <asp:Label ID="lblPrePreferenceLiveStat" runat="server" />
                </td>
                <td class="white">
                    <asp:Label ID="lblPrePreferenceLiveStatValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lightgray">
                    <asp:Label ID="lblPrePreferenceID" runat="server" />
                </td>
                <td class="white">
                    <asp:Label ID="lblPrePreferenceIDValue" runat="server" />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel ID="pnlAdminRoles" runat="server">
        <fieldset style="width: 470px;">
            <legend>
                <asp:Label ID="lblAdminRolesTitle" runat="server" CssClass="title" /></legend>
            <p>
                <asp:Label ID="lblAdminRolesDescription" runat="server" /></p>
            <div>
                <asp:Repeater ID="rptModules" runat="server" OnItemDataBound="rptModules_Bound">
                    <ItemTemplate>
                        <p>
                            <asp:Label ID="lblModule" runat="server" />
                        </p>
                        <div>
                            <asp:CheckBoxList ID="cblAdminRoles" runat="server" RepeatDirection="Horizontal" />
                            <asp:HiddenField ID="hfModuleID" runat="server" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <p>
                    <asp:Button ID="btSaveAdminRoles" runat="server" OnClick="btSaveAdminRoles_Click" />
                </p>
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>
