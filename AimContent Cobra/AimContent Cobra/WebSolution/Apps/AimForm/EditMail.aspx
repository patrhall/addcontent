<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="EditMail.aspx.cs" Inherits="AimForm.EditMail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
<asp:content id="Content1" contentplaceholderid="cphHead" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="cphDefault" runat="server"> 
<input id="hiddenActorID" type="hidden" name="hiddenActorID" runat="server" />
        <table cellspacing="1" cellpadding="2" width="910" border="0">
            <tr>
                <td class="framed-tlrb" valign="top" align="center" width="100%">
                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                        <tr>
                            <td valign="top" colspan="5" height="40">
                                <div class="headline">
                                    <b>Redigera mailadresser </b>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="normal">
                    <asp:Label ID="lblFromMail" runat="server" CssClass="normal">Avsändaradress:</asp:Label>
                    <asp:TextBox ID="tbFrommail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revEFrommail" runat="server" Display="Dynamic"
                        ControlToValidate="tbFrommail" ErrorMessage="Var vänlig ange giltig epost-adress."
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Size="X-Small"></asp:RegularExpressionValidator><br />
                </td>
            </tr>
            <tr>
                <td class="headerstyle" valign="top" colspan="5" height="20">
                  Lägg till/redigera mailadresser som ska ta emot uppgifter från ifyllt formulär
                </td>
            </tr>
            <tr>
                <td style="width: 480px" id="Alternatives" class="bread" runat="server" valign="top">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 480px;">
                                <tr>
                                    <td class="normal" style="width: 260px;">
                                        <b>Mailadress:</b><br />
                                        <asp:TextBox ID="tbRepeat" Width="320" Text='<%# DataBinder.Eval(Container.DataItem,"Mail") %>'
                                            runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic" ControlToValidate="tbRepeat"
                                            ErrorMessage="Var vänlig ange giltig epost-adress." ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="bread" align="left" style="width: 80px;">
                                        <br />
                                        <asp:Label ID="lbl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Button ID="btnDelete" CssClass="inputs" runat="server" CommandName="DeleteAlternative"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Text="Radera" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 10px;">
                    <asp:Button ID="btnNew" runat="server" CssClass="inputs" Text="Lägg till ny mottagaradress"
                        OnClick="btnNew_Click" />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px;">
                    <asp:Button ID="btnNext" runat="server" CssClass="inputs" Text="Spara" OnClick="btnNext_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
</asp:content>
