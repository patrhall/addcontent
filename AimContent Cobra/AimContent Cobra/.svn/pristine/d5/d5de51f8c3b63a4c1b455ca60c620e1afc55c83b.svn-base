<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Magazine.aspx.cs" Inherits="AimBusiness.Customers.Mekpoint.Website.Magazine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />        
    <script src="scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <asp:Panel ID="pnlOverview" runat="server">
        <rad:RadGrid ID="rgMagazine" runat="server" 
                     Width="100%" 
                     OnNeedDataSource="rgMagazine_NeedDataSource"                 
                     OnItemDataBound="rgMagazine_ItemDataBound"
                     OnItemCreated="rgMagazine_ItemCreated"
                     OnItemCommand="rgMagazine_ItemCommand"
                     AllowSorting="false" 
                     AllowPaging="true" 
                     AllowRowSelect="false"
                     GridLines="None" 
                     PageSize="30"
                     AutoGenerateColumns="false">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id">                    
                <CommandItemTemplate>
                    <asp:LinkButton ID="lbNew" runat="server" CommandName="newitem" />
                </CommandItemTemplate>
                <Columns>
                    <rad:GridBoundColumn DataField="Name" HeaderText="Namn" />
                    <rad:GridButtonColumn ButtonType="LinkButton" CommandName="viewarticles" Text="Visa artiklar">
                        <HeaderStyle Width="80px" />
                    </rad:GridButtonColumn>
                    <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="edititem" >                    
                        <HeaderStyle Width="20px" />
                    </rad:GridButtonColumn>
                    <rad:GridButtonColumn ButtonType="ImageButton" ConfirmText="Är du säker?" ImageUrl="/Images/delete.gif" CommandName="deleteitem" >
                        <HeaderStyle Width="20px" />
                    </rad:GridButtonColumn>
                </Columns>
            </MasterTableView>        
        </rad:RadGrid>
    </asp:Panel>

    <asp:Panel ID="pnlDetail" runat="server">
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:Button ID="btnBack" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />
        <br /><br />
        Namn:<br />
        <asp:TextBox ID="tbName" runat="server" />
        <br /><br />Rubrik:<br />
        <asp:TextBox ID="tbHeadline" runat="server" />
        <br /><br />Beskrivning:<br />
        <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="50" />
        <br /><br />Datum:<br />   
        <rad:RadDatePicker ID="rdpTime" runat="server" />            
        <br /><br />Bild:<br />
        <asp:FileUpload ID="fuImage" runat="server" />
        <asp:Panel ID="pnlImage" runat="server">
            Uppladdad bild: 
            <asp:HyperLink ID="hlImage" runat="server" Text="Visa" Target="_blank" />
            - 
            <asp:LinkButton ID="lbImageDelete" runat="server" Text="Ta bort" OnClientClick="return confirm('Är du säker?');" OnClick="lbImageDelete_Click" />
        </asp:Panel>
        <br /><br />Pdf Del 1:<br />
        <asp:FileUpload ID="fuPdfPart1" runat="server" />
        <asp:Panel ID="pnlPdfPart1" runat="server">
            Uppladdad pdf: 
            <asp:HyperLink ID="hlPdfPart1" runat="server" Text="Visa" Target="_blank" />
            - 
            <asp:LinkButton ID="lbPdfPart1" runat="server" Text="Ta bort" OnClientClick="return confirm('Är du säker?');" OnClick="lbPdfPart1Delete_Click" />
        </asp:Panel>
        <br /><br />Pdf Del 2:<br />
        <asp:FileUpload ID="fuPdfPart2" runat="server" />
        <asp:Panel ID="pnlPdfPart2" runat="server">
            Uppladdad pdf: 
            <asp:HyperLink ID="hlPdfPart2" runat="server" Text="Visa" Target="_blank" />
            - 
            <asp:LinkButton ID="lbPdfPart2" runat="server" Text="Ta bort" OnClientClick="return confirm('Är du säker?');" OnClick="lbPdfPart2Delete_Click" />
        </asp:Panel>
        <br /><br />Pdf Del 3:<br />
        <asp:FileUpload ID="fuPdfPart3" runat="server" />
        <asp:Panel ID="pnlPdfPart3" runat="server">
            Uppladdad pdf: 
            <asp:HyperLink ID="hlPdfPart3" runat="server" Text="Visa" Target="_blank" />
            - 
            <asp:LinkButton ID="lbPdfPart3" runat="server" Text="Ta bort" OnClientClick="return confirm('Är du säker?');" OnClick="lbPdfPart3Delete_Click" />
        </asp:Panel>
        <br /><br />Html-läsning:<br />
        <asp:FileUpload ID="fuHtml" runat="server" />
        <asp:Panel ID="pnlHtml" runat="server">
            Uppladdad pdf: 
            <asp:HyperLink ID="hlHtml" runat="server" Text="Visa" Target="_blank" />
            - 
            <asp:LinkButton ID="lbHtml" runat="server" Text="Ta bort" OnClientClick="return confirm('Är du säker?');" OnClick="lbHtmlDelete_Click" />
        </asp:Panel>
        <br /><br />
        <asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlArticleList" runat="server">
        <asp:HiddenField ID="hfMagazineId" runat="server" />
        <asp:Button ID="btnBack2" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />
        <br /><br />
        <rad:RadGrid ID="rgArticleList" runat="server" 
                     Width="100%"
                     OnNeedDataSource="rgArticleList_NeedDataSource"                 
                     OnItemDataBound="rgArticleList_ItemDataBound"
                     OnItemCreated="rgArticleList_ItemCreated"
                     OnItemCommand="rgArticleList_ItemCommand"
                     AllowSorting="false" 
                     AllowPaging="false" 
                     AllowRowSelect="false"
                     GridLines="None" 
                     PageSize="30"
                     AutoGenerateColumns="false">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="ObjectID">                    
                <CommandItemTemplate>
                    <asp:LinkButton ID="lbNew" runat="server" CommandName="newitem" />
                </CommandItemTemplate>
                <Columns>
                    <rad:GridBoundColumn DataField="Title" HeaderText="Ämne" />
                    <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="edititem" >                    
                        <HeaderStyle Width="20px" />
                    </rad:GridButtonColumn>
                    <rad:GridButtonColumn ButtonType="ImageButton" ConfirmText="Är du säker?" ImageUrl="/Images/delete.gif" CommandName="deleteitem" >
                        <HeaderStyle Width="20px" />
                    </rad:GridButtonColumn>
                </Columns>
            </MasterTableView>        
        </rad:RadGrid>
    </asp:Panel>

    <asp:Panel ID="pnlArticle" runat="server">
        <asp:HiddenField ID="hfArticleId" runat="server" />
        <asp:Button ID="btnArticleBack" runat="server" Text="Tillbaka" OnClick="btnArticleBack_Click" />
        <div id="tabbedBox">
			<div id="firstTabButton" class="active tabButton">Redigera</div>
			<div id="secondTabButton" class="unactive tabButton">Inställningar</div>
			<div style="clear:both;"></div>
			
            <div id="firstTab" class="show">
                <br /><br />Innehåll:<br />
                <rad:RadEditor language="en-US" id="reArticle" ToolsFile="/App_Data/RadControls/Editor/ToolsFile.xml" Height="350px" Runat="server" OnSubmitClicked="btnArticleSave_Click" />
            </div>

            <div id="secondTab" class="hide">
                <br /><br />Rubrik:<br />
                <asp:TextBox ID="tbArticleHeadline" runat="server" />
                <br /><br />Ingress:<br />
                <asp:TextBox ID="tbArticleIntroText" runat="server" TextMode="MultiLine" Rows="5" Columns="50" />
                <br /><br />Ingressbild:<br />
                <asp:FileUpload ID="fuArticleImage" runat="server" />
                <asp:Panel ID="pnlArticleImage" runat="server">
                    Uppladdad bild: 
                    <asp:HyperLink ID="hlArticleImage" runat="server" Text="Visa" Target="_blank" />
                    - 
                    <asp:LinkButton ID="lbArticleImage" runat="server" Text="Ta bort" OnClientClick="return confirm('Är du säker?');" OnClick="lbArticleImageDelete_Click" />
                </asp:Panel>
                <br /><br />Roller:<br />
                <asp:CheckBoxList ID="cblRoleProperty" RepeatDirection="horizontal" runat="server" /> 
                <br /><br />Publiceringsdatum:<br />
                <table>                    
                    <tr>   
                        <td colspan="2">
                            <asp:CheckBox ID="cbUsePublicationDate" runat="server" Text="Använd publiceringsdatum" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Sidan publicerad från:</b><br />
                            <rad:RadDateTimePicker ID="calFrom" Width="240px" runat="server"></rad:RadDateTimePicker>
                        </td>
                        <td>
                            <b>Sidan publicerad till:</b><br />
                            <rad:RadDateTimePicker ID="calTo" Width="240px" runat="server"></rad:RadDateTimePicker>                            
                        </td>                        
                    </tr>            
                </table>
            </div>
        </div>
        <br /><br />        
        <asp:Button ID="btnArticleSave" runat="server" Text="Spara" OnClick="btnArticleSave_Click" />
    </asp:Panel>

    <script type="text/javascript">
        function inactivateAllTabs() {
            $("#tabbedBox .active").each(function () {
                $(this).removeClass("active").addClass("unactive");
            });

            $("#tabbedBox .show").each(function () {
                $(this).removeClass("show").addClass("hide");
            });
        }

        $("#firstTabButton").click(function () {

            inactivateAllTabs();

            $(this).removeClass("unactive").addClass("active");

            $("#firstTab").removeClass("hide").addClass("show");
        });

        $("#secondTabButton").click(function () {

            inactivateAllTabs();

            $(this).removeClass("unactive").addClass("active");

            $("#secondTab").removeClass("hide").addClass("show");
        });
    </script>

</asp:Content>