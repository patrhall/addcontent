<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Company.aspx.cs" Inherits="AimBusiness.Customers.Mekpoint.Website.Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <asp:Panel ID="pnlOverview" runat="server">
        <asp:HiddenField ID="hfSearchFilter" runat="server" />
        <asp:HiddenField ID="hfLatestFilter" runat="server" />
        <asp:DropDownList ID="ddlCategoryFilter" runat="server" OnSelectedIndexChanged="ddlCategoryFilter_SelectedIndexChanged" AutoPostBack="true" />
        <asp:TextBox ID="tbSearch" runat="server" />
        <asp:Button ID="btnSearchSubmit" runat="server" Text="Sök" OnClick="btnSearchSubmit_Click" />
        <asp:LinkButton ID="lbLatestFilter" runat="server" Text="Visa senaste" OnClick="lbLatestFilter_Click" />
        <br />
        
        <rad:RadGrid ID="rgCompany" runat="server" 
                     Width="100%" 
                     OnNeedDataSource="rgCompany_NeedDataSource"                 
                     OnItemDataBound="rgCompany_ItemDataBound"
                     OnItemCreated="rgCompany_ItemCreated"
                     OnItemCommand="rgCompany_ItemCommand"
                     AllowSorting="true" 
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
                    <rad:GridBoundColumn DataField="Url" HeaderText="Länk" />            
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
        <br /><br />Länk:<br />
        <asp:TextBox ID="tbUrl" runat="server" />
        <br /><br />Beskrivning:<br />
        <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="50" />
        <br /><br />Kategori:<br />
        <asp:DropDownList ID="ddlCategories" runat="server" />
        <br /><br />Övriga val:<br />
        <asp:CheckBox ID="cbLK" runat="server" Text="Ledig Kapacitet" />
        <asp:CheckBox ID="cbEP" runat="server" Text="Egna Produkter" />
        <asp:CheckBox ID="cbCC" runat="server" Text="CAD/CAM" />
        <br /><br />
        <asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" />
    </asp:Panel>
</asp:Content>