<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="Overview.aspx.cs" Inherits="AimSiteNews.Overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
              
           <asp:PlaceHolder ID="phPagePreference" runat="server" Visible="false" />            
            <asp:Panel ID="pnlContent" runat="server">
                <p>                    
                    <asp:LinkButton ID="lbPreference" runat="server" OnClick="lbPreference_OnClick" />
                </p> 
                    
                
                <rad:RadGrid ID="rgNews" runat="server" Width="100%" 
                    OnNeedDataSource="rgNews_NeedDataSource"                 
                   OnItemDataBound="rgNews_ItemDataBound"
                   OnItemCreated="rgNews_ItemCreated"
                   OnItemCommand="rgNews_ItemCommand"
                    AllowSorting="true" AllowPaging="true" GridLines="None" PageSize="30"
                    AutoGenerateColumns="false"                
                >
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ObjectID">                    
                        <CommandItemTemplate>
                            <asp:LinkButton ID="lbNew" runat="server" CommandName="new" />
                        </CommandItemTemplate>
                        <Columns>
                            <rad:GridBoundColumn DataField="LastChanged" DataFormatString="{0:yyyy-MM-dd hh:mm}">
                                <HeaderStyle Width="130px" />
                            </rad:GridBoundColumn>
                            <rad:GridBoundColumn DataField="Title" />                          
                            <rad:GridBoundColumn DataField="email" />                
                            <rad:GridButtonColumn ButtonType="LinkButton" Visible="false" UniqueName="linkRegistration" CommandName="REGISTRATIONS" >                    
                                <HeaderStyle Width="110px" />
                            </rad:GridButtonColumn>
                            <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="editadmin" >                    
                                <HeaderStyle Width="20px" />
                            </rad:GridButtonColumn>
                            <rad:GridButtonColumn ButtonType="ImageButton" ConfirmText="Är du säker?" ImageUrl="/Images/delete.gif" CommandName="removeadmin" >
                                <HeaderStyle Width="20px" />
                            </rad:GridButtonColumn>
                        </Columns>
                    </MasterTableView>        
                </rad:RadGrid> 
                <p><asp:Label ID="lblMessage" runat="server" /></p>    
            </asp:Panel>
</asp:Content>
