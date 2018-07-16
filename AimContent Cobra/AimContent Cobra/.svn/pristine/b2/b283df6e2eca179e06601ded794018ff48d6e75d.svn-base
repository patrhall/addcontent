<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="EditAd.aspx.cs" Inherits="AimAds.EditAd"  ValidateRequest="false"  %>

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
                    <asp:TextBox ID="tbTitle" Width="400" CssClass="tbAd" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;<asp:HyperLink ID="hlReturn" Text="&lt;img src='/Images/arrow_left_grey.gif' alt='Gå tillbaka till listan' /&gt;" runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescription" Text="Beskrivning" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbDescription" Width="400" CssClass="tbAd" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblImageUploaderImg" Text="Ladda upp en bild" CssClass="title" runat="server"></asp:Label>
                    <br />
                    <img style="background-color:#88A0B8;" id="imgCurrent" src="" width="115" runat="server" alt=""/>
                </td>
                <td valign="top" align="left">
                    <asp:FileUpload ID="fuImageUploader" runat="server" Width="400px" />
                    <asp:RegularExpressionValidator
                        id="FileUpLoadValidator" Enabled="false" runat="server"
                        ErrorMessage="Tillåtna filtyper är .jpg, .gif eller .png."
                        ValidationExpression="^(.*\.(jpg|JPG||jpeg|JPEG|gif|GIF|png|PNG))$"
                        ControlToValidate="fuImageUploader">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHref" Text="Länk" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbHref" Width="400" CssClass="tbAdStyle" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTarget" Text="Öppnas i" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTarget" Width="100px" CssClass="normal" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBorder" Text="Ram" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>                    
                    <asp:DropDownList ID="ddlBorders" Width="100px" CssClass="normal" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr id="trAllowScripts" runat="server">
                <td>
                    <asp:Label ID="lblScriptCode" Text="Scriptkod" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbScriptCode" Width="400" Height="100" TextMode="MultiLine" CssClass="tbAd" runat="server"></asp:TextBox>
                </td>                
            </tr>
            <tr>
                <td>                    
                    <asp:Label ID="lblShow" Text="Visa" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>                                    
                    <asp:CheckBox ID="cbShow" CssClass="title" runat="server" />
                </td>
            </tr>
            <tr>
                 <td>
                    <asp:Label ID="lblPublishHeader" Text="Publiceringsdatum" runat="server" CssClass="title" />
                 </td>
                 <td>
                 </td>
            </tr>                             
            <tr>
                 <td></td>
                 <td>
                    <asp:Label ID="lblPublishFrom" Text="Från" runat="server" CssClass="title" /><br />
                    <rad:RadDateTimePicker Width="150px" ID="calFrom" runat="server" ></rad:RadDateTimePicker>
                    </td>
            </tr>                            
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblPublishTo" Text="Till" runat="server" CssClass="title" /><br />
                    <rad:RadDateTimePicker Width="150px" ID="calTo" runat="server" ></rad:RadDateTimePicker>
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td>
                    <asp:Label ID="lblDelete" Text="Delete" CssClass="title" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="cbDelete" CssClass="cbAd" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button Text="Spara" ID="btnSave" CssClass="btnAd" runat="server" OnClick="btnSave_OnClick" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
