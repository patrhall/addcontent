<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="True"
    CodeBehind="EditUser.aspx.cs" Inherits="AimNews.EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <div style="padding-top: 0px; padding-bottom: 10px;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                    </td>
                    <td valign="top">
                        <asp:ImageButton ID="ibBack" runat="server" ImageUrl="/images/arrow_left_grey.gif"
                            OnClick="ibBack_OnClick" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width: 730px;">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="lblFirstname" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbFirstname" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvFirstname" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbFirstname" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                                <td style="width: 50%; padding-left: 15px;">
                                    <asp:Label ID="lblLastName" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbLastName" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvLastname" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbLastname" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="lblEmail" runat="server" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbEmail" runat="server" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvEmail" ValidationGroup="vgUser" ControlToValidate="tbEmail"
                                        runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revEmail"
                                            runat="server" Display="Dynamic" ValidationGroup="vgUser" ControlToValidate="tbEmail"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                                </td>
                                <td style="width: 50%; padding-left: 15px;">
                                    <asp:Label ID="lblCompany" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbCompany" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvCompany" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbCompany" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                 <td style="width: 50%;">
                                    <asp:Label ID="lblTelephone" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbTelephone" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvTelephone" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbTelephone" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                                 <td style="width: 50%; padding-left: 15px;">
                                    <asp:Label ID="lblMobile" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbMobile" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvMobile" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbMobile" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>                           
                            <tr>
                                 <td style="width: 50%;">
                                    <asp:Label ID="lblFax" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbFax" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvFax" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbFax" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                                 <td style="width: 50%; padding-left: 15px;">                                    
                                </td>
                            </tr>
                            <tr>
                                 <td style="width: 50%;">
                                    <asp:Label ID="lblAddress" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbAddress" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvAddress" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbAddress" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                                 <td style="width: 50%; padding-left: 15px;">
                                    <asp:Label ID="lblPostal" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbPostal" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvPostal" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbPostal" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                 <td style="width: 50%;">
                                    <asp:Label ID="lblCity" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbCity" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvCity" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbCity" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                                 <td style="width: 50%; padding-left: 15px;">
                                    <asp:Label ID="lblCountry" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbCountry" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvCountry" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbCountry" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                 <td style="width: 50%;">
                                    <asp:Label ID="lblOrgnr" runat="server" Visible="false" CssClass="title"></asp:Label><br />
                                    <asp:TextBox ID="tbOrgnr" runat="server" Visible="false" Width="350px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvOrgnr" Visible="false" ValidationGroup="vgUser"
                                        ControlToValidate="tbOrgnr" runat="server"></asp:RequiredFieldValidator><br />
                                </td>
                                 <td style="width: 50%; padding-left: 15px;">                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                </tr>
     
                <tr>
                    <td style="text-align: right;">
                        <asp:Button ID="btnSave" CssClass="inputs" runat="server" ValidationGroup="vgUser"
                            Visible="true" OnClick="btnSave_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
      <div style="padding-top: 7px;">
        <asp:Label ID="lblStatus" runat="server" Visible="false" CssClass="confirm"></asp:Label>
    </div>
</asp:Content>
