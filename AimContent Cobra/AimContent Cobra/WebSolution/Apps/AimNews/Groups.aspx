<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="Groups.aspx.cs" Inherits="AimNews.Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">

<rad:RadGrid ID="rgGroups" runat="server" Width="100%" 
        OnNeedDataSource="rgGroups_NeedDataSource"                 
        OnItemCommand="rgGroups_ItemCommand"  
        OnItemCreated="rgGroups_ItemCreated" 
        AllowSorting="true" AllowPaging="true" GridLines="None" PageSize="30"
        AutoGenerateColumns="false"                
    >
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID">                    
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNewGroup" runat="server" href="EditGroup.aspx?ID=0" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                    
                        <%# DataBinder.Eval(Container.DataItem,"GroupName") %> 
                    </ItemTemplate>
                   <HeaderStyle Width="90%" />
                </rad:GridTemplateColumn>
                
                <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="editGroup" >                    
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteButton"  ImageUrl="/Images/delete.gif" CommandName="deleteGroup" >
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
            </Columns>
        </MasterTableView>        
    </rad:RadGrid>   
</asp:Content>

