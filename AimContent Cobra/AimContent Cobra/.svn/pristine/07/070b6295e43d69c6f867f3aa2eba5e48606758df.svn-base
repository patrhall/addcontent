<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="AimPhoto.CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">     
    <asp:PlaceHolder ID="phPagePreference" runat="server" Visible="false" />    
    <asp:Panel ID="pnlContent" runat="server">	            
        <asp:LinkButton ID="lbPreference" runat="server" OnClick="lbPreference_OnClick" />
        <div>
            <rad:radGrid ID="rgCategories" runat="server" AllowSorting="True" HeaderStyle-HorizontalAlign="Left" HorizontalAlign="Left" GridLines="none" 
                AutoGenerateColumns="False" Width="90%" MasterTableView-DataKeyNames="ID" OnItemCommand="rgCategories_ItemCommand" OnNeedDataSource="rgCategories_NeedDataSource" OnItemCreated="rgCategories_ItemCreated" OnItemDataBound="rgCategories_ItemDataBound1">
                
                <MasterTableView  CommandItemDisplay="Top">
                    <CommandItemTemplate>                                            
                        <asp:LinkButton ID="lbCreateCategory" runat="server" OnClick="lbCreateCategory_OnClick" />
                    </CommandItemTemplate>
                    <Columns>                         
                        <rad:GridTemplateColumn UniqueName="Image"> 
                            <ItemTemplate>
                                <asp:ImageButton ID="AlbumImgB" CommandName="open" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' runat="server"/>                                        
                                <asp:Image Visible="false" ID="AlbumImg" runat="server" AlternateText=""/>
                            </ItemTemplate>
                             <ItemStyle Width="30px" Wrap="False" />
                        </rad:GridTemplateColumn> 
                                                        
                        <rad:GridBoundColumn DataField="CategoryName" SortExpression="CategoryName" UniqueName="CategoryName" DataFormatString="&lt;nobr&gt;{0}&lt;/nobr&gt;">
                            <HeaderStyle Width="100%" Font-Bold="True"/>                                            
                        </rad:GridBoundColumn>
                        <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="open" >                    
                            <HeaderStyle Width="15px" />
                        </rad:GridButtonColumn>
                        <rad:GridButtonColumn ButtonType="ImageButton" UniqueName="Action2"  ImageUrl="/Images/delete.gif" CommandName="del" >
                            <HeaderStyle Width="15px" />
                        </rad:GridButtonColumn>                                                                           
                        <rad:GridBoundColumn DataField="AlbumCover" SortExpression="AlbumCover"
                            UniqueName="AlbumCover" Visible="false" DataFormatString="&lt;nobr&gt;{0}&lt;/nobr&gt;"><HeaderStyle Width="60px" Font-Bold="True"/>
                        
                        </rad:GridBoundColumn>
                        <rad:GridBoundColumn Visible="false" DataField="img" SortExpression="img"
                            UniqueName="img" DataFormatString="&lt;nobr&gt;{0}&lt;/nobr&gt;"><HeaderStyle Width="60px" Font-Bold="True"/>
                        
                        </rad:GridBoundColumn>
                    </Columns>
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>                         
                    <HeaderStyle HorizontalAlign="Left" />
                </MasterTableView>                    
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True">                                        
                    <Resizing AllowColumnResize="True" ClipCellContentOnResize="False"/>
                </ClientSettings>
                <HeaderStyle HorizontalAlign="Left" />
            </rad:radGrid>
        </div>   
     </asp:Panel>   
</asp:Content>