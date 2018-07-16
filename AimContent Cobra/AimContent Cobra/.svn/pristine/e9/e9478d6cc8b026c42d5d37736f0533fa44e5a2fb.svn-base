<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="MailList.aspx.cs" Inherits="AimNews.MailList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">

<rad:RadGrid ID="rgNewsLetter" runat="server" Width="100%" 
        OnNeedDataSource="rgNewsLetter_NeedDataSource"                 
        OnItemCommand="rgNewsLetter_ItemCommand"  
        OnItemCreated="rgNewsLetter_ItemCreated" 
        AllowSorting="true" AllowPaging="true" GridLines="None" PageSize="50"
        AutoGenerateColumns="false"                
    >
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID">                    
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNewMail" runat="server" href="EditMail.aspx?ID=0" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Header") %> 
                    </ItemTemplate>
                   <HeaderStyle Width="70%" />
                </rad:GridTemplateColumn>
                  <rad:GridTemplateColumn UniqueName="LastDateColumn">
                    <ItemTemplate >
                        <%# formatDate((DateTime?)DataBinder.Eval(Container.DataItem, "LastSendDate")) %> 
                    </ItemTemplate>
                   <HeaderStyle Width="15%" />
                </rad:GridTemplateColumn>
                <rad:GridButtonColumn UniqueName="StatisticsColumn" ButtonType="ImageButton" Visible="false" ImageUrl="/Images/diagram.png" CommandName="viewStat" >                    
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="editMail" >                    
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteButton"  ImageUrl="/Images/delete.gif" CommandName="removeMail" >
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
            </Columns>
        </MasterTableView>        
    </rad:RadGrid>
     <div style="padding-top: 7px;">
        <asp:Label ID="lblStatus" runat="server" Visible="false" CssClass="confirm"></asp:Label>
    </div>   
</asp:Content>

