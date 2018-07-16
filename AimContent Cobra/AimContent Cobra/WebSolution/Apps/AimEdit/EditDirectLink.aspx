<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="EditDirectLink.aspx.cs" Inherits="AimEdit.EditDirectLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <asp:PlaceHolder ID="phPagePreference" runat="server" Visible="false" />
    <div>        
        <asp:Panel ID="pnlContent" runat="server">
            <div>
                <asp:LinkButton ID="lbPreference" runat="server" OnClick="lbPreference_OnClick" />                    
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddLinkType" runat="server">
	                            <asp:ListItem Value="0">http://</asp:ListItem>
	                            <asp:ListItem Value="1">https://</asp:ListItem>
	                            <asp:ListItem Value="2"></asp:ListItem>
	                        </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="tbLink2" runat="server" Width="468px" />
                        </td>
                    </tr>
                </table>        
	            <br /><br />
	            <asp:RadioButtonList id="rblListType" runat="server" />                   
                <br /><br />          
			    <asp:button id="btnSave" Runat="server" onclick="btnSave_Click" />
            </div>
        </asp:Panel>        
    </div>
</asp:Content>
