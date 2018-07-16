<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" MasterPageFile="~/Site.Master" Inherits="AimBusiness.Customers.ModulSystem.Website.Overview" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<div>
    Url:<asp:TextBox ID="txtUrl" runat="server"></asp:TextBox><br />
    Description:<br />
    <rad:radeditor language="en-US" id="reDescription" ToolsFile="/App_Data/RadControls/Editor/ToolsFile.xml" Height="400px" Width="400px" Runat="server"  />
    <br />
    <asp:Button ID="btnSaveUrl" runat="server" Text="Save" OnClick="btnSaveUrl_Click"/>
</div>
    <div>
        <p>
            <asp:HyperLink ID="hlUploadProductExcel" runat="server" NavigateUrl="Upload.aspx">Upload new products structure</asp:HyperLink>
        </p>        
		<p>
			<a href="ArticleGroups.aspx">Edit dimensions and article groups</a>
		</p>
        <p>
            <telerik:RadComboBox ID="ddArticleGroups" runat="server" DataValueField="Id" DataTextField="Name" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="ddArticleGroups_Changed" />
        </p>
       
        <telerik:RadGrid ID="rgArticle" runat="server" OnNeedDataSource="rgArticle_Source">
            <MasterTableView DataKeyNames="Id">
                <Columns>                    
                    <telerik:GridHyperLinkColumn DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Article.aspx?Id={0}" Text="Edit" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div>
        <asp:LinkButton ID="lbExportAll" Visible="false" runat="server" Text="Exportera excel" OnClick="lbExportAll_Click" />
        <telerik:RadGrid ID="rgExportAll" runat="server" Visible="false">
        </telerik:RadGrid>
    </div>
</asp:Content>