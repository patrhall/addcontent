<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true"
    CodeBehind="Tag.aspx.cs" Inherits="AimTags.Tag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<script type="text/javascript" src='http://maps.google.com/maps/api/js?sensor=false'></script>
    <script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js'></script>
    <script type='text/javascript' src='scripts/jquery.locationpicker.js'></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery('input.lnglat').locationPicker();
        });
    </script>	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <asp:Label ID="lblTagName" runat="server" />
    <br />
    <asp:TextBox ID="tbTagName" Width="200px" runat="server" />
    <br />
    <br />
    <asp:Label ID="lblTagSlug" runat="server" />
    <br />
    <asp:TextBox ID="tbTagSlug" Width="200px" runat="server" />
    <br />
    <br />
    <asp:Panel ID="pnlTagType" runat="server">
        <asp:Label ID="lblTagType" runat="server" />
        <br />
        <asp:DropDownList ID="ddlTagTypes" OnSelectedIndexChanged="ddlTagTypes_SelectedIndexChanged" AutoPostBack="true"
            Width="200px" runat="server" />
        <br />
    </asp:Panel>
    <asp:Panel ID="pnlTagProperties" runat="server">
        <br />
        <br />
        <asp:Repeater ID="rpTagProperties" runat="server" OnItemDataBound="rptTagProperties_Bound">
            <ItemTemplate>
                <div>
                    <asp:HiddenField ID="hfTagPropertyTypeId" runat="server" />
                    <asp:HiddenField ID="hfTagPropertyValueId" runat="server" />
                    <p>
                        <asp:Label ID="lblTitle" runat="server" />
                        <br />
                        <asp:TextBox ID="tbText" runat="server" Visible="false" />
                        <rad:RadEditor ID="radRichText" runat="server" Visible="false">
                            <Tools>
                                <rad:EditorToolGroup>
                                    <rad:EditorTool Name="PastePlainText" />
                                    <rad:EditorTool Name="ImageManager" />
                                    <rad:EditorTool Name="MediaManager" />
                                    <rad:EditorTool Name="DocumentManager" />
                                    <rad:EditorTool Name="LinkManager" />
                                    <rad:EditorTool Name="Unlink" />
                                    <rad:EditorTool Name="InsertSymbol" />
                                    <rad:EditorTool Name="ApplyClass" />
                                </rad:EditorToolGroup>
                            </Tools>
                        </rad:RadEditor>
                        <rad:RadDatePicker ID="rdpDate" runat="server" Visible="false" />
                        <asp:RequiredFieldValidator ID="reqItem" runat="server" ErrorMessage="*" ValidationGroup="widgetform"
                            Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="regItem" ControlToValidate="tbText" runat="server"
                            ErrorMessage="*" ValidationGroup="widgetform" Display="Dynamic" ForeColor="Red"
                            Enabled="false" />
                    </p>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" />
</asp:Content>
