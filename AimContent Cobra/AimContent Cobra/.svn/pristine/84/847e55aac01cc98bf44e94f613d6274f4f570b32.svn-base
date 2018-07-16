<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="EditQuestionStep2.aspx.cs"
    Inherits="AimForm.EditQuestionStep2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
<asp:content id="Content1" contentplaceholderid="cphHead" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="cphDefault" runat="server"> 
<input id="hiddenActorID" type="hidden" name="hiddenActorID" runat="server" />
        <table cellspacing="1" cellpadding="2" width="700" border="0">
            <tr>
                <td class="framed-tlrb" valign="top" align="center" width="100%">
                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                        <tr>
                            <td valign="top" colspan="5" height="40">
                                <div class="headline">
                                    <b>Redigera kontaktformulär</b></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="headerstyle" valign="top" colspan="5" height="20">
                    <asp:Label ID="lblAlternative" Visible="false" runat="server"> Lägg till/redigera text till svarsalternativ:</asp:Label>
                    <asp:Label ID="lblCateogry" Visible="false" runat="server"> Lägg till/redigera text till underkategorier:</asp:Label>
                    <asp:Label ID="lblFree" Visible="false" runat="server">Lägg till/redigera text till fritextalternativ:</asp:Label>
                </td>
                <td class="headerstyle">
                </td>
            </tr>
            <tr>
                <td style="width: 480px" id="Alternatives" class="bread" runat="server" valign="top">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 480px;">
                                <tr>
                                    <td class="normal" style="width: 260px;">
                                        <b>Nuvarande text:</b><br />                                        
                                        <asp:TextBox ID="tbRepeat" Width="320" Text='<%# DataBinder.Eval(Container.DataItem,"AlternativeText") %>'
                                            runat="server"></asp:TextBox><br />
                                        <br />
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
                <td>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 10px;">
                    <asp:Button ID="btnNew" runat="server" CssClass="inputs" Text="Lägg till nytt alternativ"
                        OnClick="btnNew_Click" />
                    <asp:Button ID="btnNew3" runat="server" Visible="false" CssClass="inputs" Text="Lägg till ny underkategori"
                        OnClick="btnNew3_Click" /><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="headerstyle" valign="top" colspan="5" height="20">
                    <asp:Label ID="lblFree2" runat="server" Visible="false">Lägg till/redigera text till fritextalternativ:</asp:Label>
                    <asp:Label ID="lblAlternative2" runat="server" Visible="false"> Lägg till/redigera text till svarsalternativ:</asp:Label>
                </td>
                <td class="headerstyle">
                </td>
            </tr>
            <tr>
            <td>
                <asp:Repeater ID="Repeater3" Visible="false" runat="server" OnItemCommand="Repeater3_ItemCommand">
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 480px;">
                            <tr>
                                <td class="normal" style="width: 300px;">
                                    <br />
                                    <b>Nuvarande text:</b><br />
                                    <asp:TextBox ID="tbRepeat" Width="320"  Text='<%# DataBinder.Eval(Container.DataItem,"AlternativeText") %>' runat="server"></asp:TextBox>
                                </td>
                                <td class="normal" style="padding-left: 66px;">
                                    <br />
                                    <br />
                                    <asp:Label ID="lbl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Button ID="btnDelete" CssClass="inputs" runat="server" CommandName="DeleteQuestion"
                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Text="Radera" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater><br />
                </td>
            </tr>            
            <tr>
                <td style="width: 480px" id="Td1" class="bread" runat="server" valign="top">
                    <asp:Repeater ID="Repeater2" runat="server" Visible="false" OnItemCommand="Repeater2_ItemCommand">
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 480px;">
                                <tr>
                                    <td class="normal" valign="top" style="width: 260px;">
                                        <b>Nuvarande text:</b><br />
                                        <%# DataBinder.Eval(Container.DataItem,"AlternativeText") %>
                                        <br />
                                        <b>Ny&nbsp;text:</b>
                                        <br />
                                        <asp:TextBox ID="tbRepeat" Width="260" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="bread" valign="top">
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Label ID="lbl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'
                                            Visible="false"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete" CssClass="inputs"
                                            runat="server" CommandName="DeleteAlternative" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'
                                            Text="Radera" />
                                    </td>
                                    <td class="normal" align="left" valign="top" style="padding-left: 20px">
                                        <br />
                                        <br />
                                        <asp:Label ID="lblP2" Visible="false" runat="server">Antal poäng:</asp:Label><asp:TextBox
                                            ID="tbP2" Visible="false" Width="30px" MaxLength="2" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblJ2" Visible="false" runat="server">Om alternativ välljs hoppa till:</asp:Label><br />
                                        <asp:Label ID="lblI3" Visible="true" runat="server"></asp:Label><asp:TextBox ID="tbI3"
                                            Visible="false" Width="30px" MaxLength="2" runat="server"></asp:TextBox><br />
                                        <asp:DropDownList ID="ddlJ2" Visible="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlJump_SelectedIndexChanged">
                                        </asp:DropDownList><br />
                                        <asp:RegularExpressionValidator ID="revP" runat="server" Text="Välj poängantal mellan 0-99"
                                            ControlToValidate="tbP2" ValidationExpression="\d{1,2}"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="normal" align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblI4" Visible="true" runat="server"></asp:Label><asp:TextBox ID="tbI4"
                                            Visible="false" Width="30px" MaxLength="2" runat="server"></asp:TextBox><br />
                                        <asp:RegularExpressionValidator ID="revI" runat="server" Text="Välj poängantal mellan 0-99"
                                            ControlToValidate="tbI3" ValidationExpression="\d{1,2}"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="revI2" runat="server" Text="Välj poängantal mellan 0-99"
                                            ControlToValidate="tbI4" ValidationExpression="\d{1,2}"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 10px;" colspan="2">
                    <br />
                    <asp:Button ID="btnNew2" runat="server" Visible="false" CssClass="inputs" Text="Lägg till nytt alternativ"
                        OnClick="btnNew2_Click" /><br />
                    <br />
                    <asp:Label ID="lblMaxAlternatives" CssClass="normal" runat="server" Visible="false">Välj max antal valbara alternativ: </asp:Label>
                    <asp:DropDownList ID="ddlMaxAlternatives" runat="server" Visible="false" OnSelectedIndexChanged="ddlMaxAlternatives_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <table style="width: 415px">
                        <tr>
                            <td class="framed-tlrb" valign="top" align="Left" style="padding-left: 10px;">
                                <asp:Button ID="btnPrevious" runat="server" CssClass="inputs" Text="<< Föregående Steg"
                                    OnClick="btnPrevious_Click"></asp:Button>
                            </td>
                            <td class="framed-tlrb" valign="top" align="right" style="padding-left: 10px;">
                                <asp:Button ID="btnNext" runat="server" CssClass="inputs" Text="Spara" OnClick="btnNext_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</asp:content>
