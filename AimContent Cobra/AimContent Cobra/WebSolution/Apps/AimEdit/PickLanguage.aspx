<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="../../Site.Master" CodeBehind="PickLanguage.aspx.cs" Inherits="AimEdit.PickLanguage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">                
    <asp:PlaceHolder ID="phPagePreference" runat="server" Visible="false" />
    <div>        
        <asp:Panel ID="pnlContent" runat="server">
            <div>
                <asp:LinkButton ID="lbPreference" runat="server" OnClick="lbPreference_OnClick" />
            </div>
            <asp:RadioButtonList ID="rblLanguage" runat="server" />                    
            <div style="padding-left:5px;padding-top:7px;">
                <asp:Button ID="bLanguage" runat="server" Text="Spara" OnClick="bLanguage_Clicked" />
            </div>        
        </asp:Panel>        
    </div>
</asp:Content>