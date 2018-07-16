<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Article.aspx.cs" Inherits="AimBusiness.Customers.ModulSystem.Website.Article" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<div>        
        <p>
            Article number:<br />
            <asp:TextBox ID="tbArticleNumber" runat="server" />
        </p>
        <p>
            ArticleGroup:<br />
            <asp:DropDownList ID="ddArticleGroup" DataValueField="Id" DataTextField="Name" runat="server" />
        </p>
        <p>
            Dimension:<br />
            <asp:DropDownList ID="ddDimension" DataValueField="Id" DataTextField="Name" runat="server" />
        </p>
        <p>
            Image name:<br />
            <asp:TextBox ID="tbImageName" runat="server" />
        </p>
        <p>
            Width:<br />
            <asp:TextBox ID="tbWidth" runat="server" />
        </p>
        <p>
            Depth:<br />
            <asp:TextBox ID="tbDepth" runat="server" />
        </p>
        <p>
            Height:<br />
            <asp:TextBox ID="tbHeight" runat="server" />
        </p>
        <p>
            Weight:<br />
            <asp:TextBox ID="tbWeight" runat="server" />
        </p>
        <p>
            <asp:CheckBox ID="cbIsPublic" runat="server" Text="Is Public" />
        </p>
        <p>
            Installation Instruction Name<br />
            <asp:TextBox ID="tbInstallationInstructionName" runat="server" />
        </p>
        <p>
            PreviewImageName<br />
            <asp:TextBox ID="tbPreviewImageName" runat="server" />
        </p>
        <!--<p>
            Related articles<br />
            <asp:TextBox ID="tbRelatedArticles" runat="server" />
        </p>
        <p>
            Accesories<br />
            <asp:TextBox ID="tbRelatedArticlesAcc" runat="server" />
        </p>
        <p>
            Language exkluded<br />
            <asp:TextBox ID="tbLanguageExcluded" runat="server" />
        </p>-->
        
        <asp:Repeater ID="rptLanguage" runat="server" OnItemDataBound="rptLanguage_Bound">
            <ItemTemplate>               
                <asp:HiddenField ID="hfLanguageId" runat="server" />
                <div>
                    <p>
                        <asp:Label ID="lblLanguage" runat="server" />
                    </p>
                    <p>
                        Article name<br />
                        <asp:TextBox ID="tbArticleName" runat="server" />
                    </p>
                    <p>
                        Article description<br />
                        <asp:TextBox ID="tbArticleDescription" runat="server" TextMode="MultiLine"   />
                    </p>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <p>
            <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
        </p>
    </div>
</asp:Content>
