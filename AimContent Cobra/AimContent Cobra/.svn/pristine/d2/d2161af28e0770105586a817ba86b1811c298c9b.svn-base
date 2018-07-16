<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="ChooseForm.aspx.cs" Inherits="AimForm.ChooseForm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
<asp:content id="Content1" contentplaceholderid="cphHead" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="cphDefault" runat="server"> 
    <div>
        <asp:PlaceHolder ID="phPagePreference" runat="server" Visible="false" />
        <asp:Panel ID="pnl_ChooseFrom" runat="server">        
        <asp:LinkButton ID="lbPreference" runat="server" OnClick="lbPreference_OnClick" ></asp:LinkButton>
        <table style="height: 100%; width:100%;">                
                <tr>
                    <td valign="top">
                        <table border="0">
                            <tr>
                                <td style="width: 350px; padding-left:15px;">
                                    <table border="0" style="height: 100%; width: 100%">
                                        <tr>
                                            <td class="normal" valign="top">
                                                <asp:Label id="lblForm" CssClass="normal" Visible="false" runat="Server"><b>Formulär saknas för denna sida. Skapa formulär under fliken "Kontaktformulär"</b></asp:Label><br />
                                                <asp:RadioButtonList CssClass="normal" ID="rblForm" AutoPostBack="true" runat="server">
                                                </asp:RadioButtonList>
                                                <div style="padding-left: 5px;">
                                                    <asp:Label ID="lblNoneSelected" runat="server" Visible="False" CssClass="fet" ForeColor="red">Du måste välja en undersökning</asp:Label><br />
                                                    <br />
                                                </div>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" style="width: 350px;">
                                  
                                </td>
                            </tr>
                           <tr>
                                <td style="padding-left:20px;">
                             
                                    <asp:Button CssClass="inputs" ID="btnSave" runat="server" Text="Spara"
                                        OnClick="btnSave_Clicked"></asp:Button>

                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
      
            </table>
            </asp:Panel>
    </div>
</asp:content>
