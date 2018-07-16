<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="AimBusiness.Customers.Boletus.Website.Overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <p>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
    </p>    
    <rad:RadGrid ID="rgProducts" runat="server" OnNeedDataSource="rgProducts_Source" AutoGenerateColumns="false" OnItemCommand="rgProducts_ItemCommand">
        <MasterTableView NoMasterRecordsText="Inga produkter för närvarande" CommandItemDisplay="Top" DataKeyNames="ID">
            <CommandItemTemplate>
                <asp:HyperLink ID="hlNew" runat="server" NavigateUrl="EditProduct.aspx"><img src="/Images/newobject.gif" alt="Skapa ny produkt" /> Skapa ny produkt</asp:HyperLink>
            </CommandItemTemplate>
            <Columns>
                <rad:GridBoundColumn HeaderText="Namn" DataField="Name" />
                <rad:GridBoundColumn HeaderText="Uppdaterad" DataField="LastUpdated" DataFormatString="{0:yyyy-MM-dd}" >
                    <HeaderStyle Width="80px" />
                </rad:GridBoundColumn>
                <rad:GridBoundColumn HeaderText="Skapad" DataField="Created" DataFormatString="{0:yyyy-MM-dd}" >
                    <HeaderStyle Width="80px" />
                </rad:GridBoundColumn>
                <rad:GridButtonColumn CommandName="CHANGE" ButtonType="ImageButton" ImageUrl="/Images/edit.gif">
                    <HeaderStyle Width="16px" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn CommandName="REMOVE" ButtonType="ImageButton" ImageUrl="/Images/delete.gif" ConfirmText="Är du säker?">
                    <HeaderStyle Width="16px" />
                </rad:GridButtonColumn>
            </Columns>
        </MasterTableView>
    </rad:RadGrid>
</asp:Content>