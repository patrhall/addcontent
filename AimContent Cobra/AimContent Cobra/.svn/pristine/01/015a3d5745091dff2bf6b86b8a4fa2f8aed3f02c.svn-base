<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ShowSponsors.aspx.cs" Inherits="AimBusiness.Customers.CongrexAPP.Website.ShowSponsors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
   <style type="text/css">
.btnSortUp
{
    width:17px;
    height:16px;
    border:none;
    background:none;
    cursor:pointer;
    background-image: url('/images/arrow_down.gif');
}
.btnSortDown
{
    width:17px;
    height:16px;
    border:none;
    background:none;
    cursor:pointer;
    background-image: url('/images/arrow_up.gif');
}
.del
{
    width:16px;
    height:16px;
    border:none;
    background:none;
    margin-left: 25%;
    cursor:pointer;
    background-image: url('/images/delete.gif');
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<div>
    <asp:LinkButton ID="lbSponsors" PostBackUrl="Sponsors.aspx" runat="server">Lägg till sponsor</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lbExhibitors" PostBackUrl="Exhibitors.aspx" runat="server">Lägg till utställare</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lbShowExhibitors" PostBackUrl="ShowExhibitors.aspx" runat="server">Visa utställare</asp:LinkButton>
<br />
<br />
    <rad:RadGrid ID="rgSponsors" runat="server" Width="450px" AutoGenerateColumns="False" 
                onneeddatasource="rgSponsors_NeedDataSource" OnItemDataBound="RadGrid_ItemDataBound">
    <MasterTableView DataKeyNames="Id,Name">
                    <Columns>
                    <rad:GridBoundColumn DataField="SortOrder" HeaderText="SortOrder" />
                        <rad:GridTemplateColumn HeaderText="Namn" UniqueName="Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbName" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>

                        <rad:GridBoundColumn DataField="Link" HeaderText="Länk" />
                        <rad:GridTemplateColumn HeaderText="Sortera">
                        <ItemTemplate>
                            <asp:Button ID="moveDown1" CssClass="btnSortDown" ToolTip="Flytta uppåt i listan" runat="server" 
                                CommandArgument='<%# Eval("Id")%>'  Text="" onclick="moveDown_Click" />
                            <asp:Button  ID="moveUp1" CssClass="btnSortUp" ToolTip="Flytta neråt i listan" runat="server" 
                                CommandArgument='<%# Eval("Id")%>'  Text="" onclick="moveUp_Click" />
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="Ta bort">
                        <ItemTemplate>
                        <asp:Button ID="DelTask" runat="server" CommandArgument='<%# Eval("Id")%>' 
                                CssClass="del" ToolTip="Radera"
                                onclick="DelTask_Click">
                            </asp:Button>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
    </rad:RadGrid>
</div>
</asp:Content>
