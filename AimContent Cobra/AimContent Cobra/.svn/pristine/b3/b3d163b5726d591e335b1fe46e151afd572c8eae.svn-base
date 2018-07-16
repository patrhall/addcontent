<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleGroups.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="AimBusiness.Customers.ModulSystem.Website.ArticleGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <asp:Panel ID="pnlOverview" runat="server">
        <div>
            <h3>Dimensioner</h3>
            <asp:DropDownList ID="ddDimensions" runat="server" Width="200px" />
            <asp:ImageButton ID="ibEditDimension" runat="server" ImageUrl="/Images/edit.gif"
                OnClick="ibEditDimension_Click" />
        </div>
        <div>
            <h3>Artikelgrupper</h3>
            <asp:DropDownList ID="ddArticleGroups" runat="server" Width="200px"/>
            <asp:ImageButton ID="ibEditArticleGroup" runat="server" ImageUrl="/Images/edit.gif"
                OnClick="ibEditArticleGroup_Click" />
        </div>        
    </asp:Panel>
    <asp:Panel ID="pnlEdit" runat="server" Visible="false">
        <div>            
            <asp:HiddenField ID="hfWhat" runat="server" />
            <p><asp:Label ID="lblEditTitle" runat="server" /></p>
        </div>
        <asp:Repeater ID="rptLanguage" runat="server" OnItemDataBound="rptLanguage_Bound">
            <ItemTemplate>
                <asp:HiddenField ID="hfLanguageId" runat="server" />
                <div>
                    <p>
                        <asp:Label ID="lblLanguage" runat="server" />
                    </p>
                    <p>
                        Name<br />
                        <asp:TextBox ID="tbName" runat="server" />
                    </p>                    
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <p>
            <asp:Button ID="btSave" runat="server" Text="Spara" OnClick="btSave_Click" /> <a href="ArticleGroups.aspx">Ångra</a>
        </p>
    </asp:Panel>
</asp:Content>
