<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="Users.aspx.cs" Inherits="AimNews.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">

<rad:RadGrid ID="rgUsers" runat="server" Width="100%" 
        OnNeedDataSource="rgUsers_NeedDataSource"                 
        OnItemCommand="rgUsers_ItemCommand"  
        OnItemCreated="rgUsers_ItemCreated" 
        AllowSorting="true" AllowPaging="true" GridLines="None" PageSize="30"
        AutoGenerateColumns="false"                
    >
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID">                    
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNewUser" runat="server" href="EditUser.aspx?ID=0" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridTemplateColumn SortExpression="FirstName" Visible="false" UniqueName="FirstName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%> 
                    </ItemTemplate>
                   <HeaderStyle Width="20%" />
                </rad:GridTemplateColumn>
                <rad:GridTemplateColumn SortExpression="LastName" Visible="false" UniqueName="LastName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LastName")%> 
                    </ItemTemplate>
                   <HeaderStyle Width="20%" />
                </rad:GridTemplateColumn>
                  <rad:GridTemplateColumn SortExpression="Email" Visible="false" UniqueName="Email">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Email")%> 
                    </ItemTemplate>
                   <HeaderStyle Width="30%" />
                </rad:GridTemplateColumn>
                  <rad:GridTemplateColumn SortExpression="Company" Visible="false" UniqueName="Company">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Company")%> 
                    </ItemTemplate>
                   <HeaderStyle Width="20%" />
                </rad:GridTemplateColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="editUser" >                    
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteButton"  ImageUrl="/Images/delete.gif" CommandName="deleteUser" >
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
            </Columns>
        </MasterTableView>        
    </rad:RadGrid>   
</asp:Content>

