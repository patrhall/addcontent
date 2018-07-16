<%@ Page Language="C#" MasterPageFile="../../Site.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="EditQuestion.aspx.cs" Inherits="AimForm.EditQuestion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server"> 
<input id="hiddenActorID" type="hidden" name="hiddenActorID" runat="server" />
        <table cellspacing="1" cellpadding="2" width="700" border="0">
            <tr>
                <td class="framed-tlrb" valign="top" align="left" width="100%">
                    <table cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                        <tr>                            
                            <td valign="top" colspan="5">
                                <div class="headline">
                                    <b>Redigera&nbsp;kontaktformulär</b></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="bread" valign="top" colspan="5" height="40">
                                <p>
                                    Välj frågatyp samt skriv in fråga och klicka sedan "nästa steg".<br />
                                    &nbsp;</p>
                            </td>
                        </tr>
                        <tr>
                            <td class="headerstyle" valign="top" colspan="5" height="20">
                                Egenskaper post
                            </td>
                            <td class="headerstyle" colspan="1" height="20" style="width: 4px" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <div class="bread">
                                    Frågetyp:<br />
                                    <asp:RadioButtonList ID="rblTypes" AutoPostBack="true" runat="server" CssClass="bread"
                                        Height="92px" Width="545px">
                                        <asp:ListItem Text="Fritext" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fritext med flerradstextruta" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Enval" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Flerval" Value="2" Selected="True"></asp:ListItem>                                        
                                    </asp:RadioButtonList><br />
                                    
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="headerstyle" valign="top" colspan="5" height="20">
                                Lägg/till redigera text:
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:TextBox ID="tbQuestion" runat="server" Width="450" CssClass="inputs"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" 
                                    ControlToValidate="tbQuestion" ErrorMessage="Du måste fylla i fråga" Font-Names="Verdana"
                                    Font-Size="XX-Small"></asp:RequiredFieldValidator></td>
                            <td valign="top" style="width: 122px">
                            </td>
                            <td valign="top" style="width: 22px">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" >
                            <br />
                            <asp:Label ID="lblRequired" runat="server" CssClass="bread" Text="Obligatoriskt fält: "></asp:Label><asp:CheckBox ID="cbRequired" runat="server"  /> 
                                </td>
                            <td valign="top" style="width: 122px">
                            </td>
                            <td valign="top" style="width: 22px">
                            </td>
                        </tr>
                    </table>
                    <div class="bread" align="left">
                        &nbsp;</div>
                </td>
                <td valign="top" width="20">
                </td>
                <td valign="top">
                </td>
            </tr>
            <tr>
                <td class="framed-tlrb" valign="top" align="Left" style="padding-left: 10px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="inputs" Text="Nästa Steg >>" OnClick="btnSave_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
</asp:Content>
