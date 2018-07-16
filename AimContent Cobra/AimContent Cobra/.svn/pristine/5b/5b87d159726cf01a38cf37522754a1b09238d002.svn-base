<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ShowExhibitors.aspx.cs" Inherits="AimBusiness.Customers.CongrexAPP.Website.ShowExhibitors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
   <style type="text/css">
.btnSortUp
{
    width:15px;
    height:15px;
    border:none;
    background:none;
    cursor:pointer;
    background-image: url('arrow_down.gif');
}
.btnSortDown
{
    width:15px;
    height:15px;
    border:none;
    background:none;
    cursor:pointer;
    background-image: url('arrow_up.gif');
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<div>
    <asp:LinkButton ID="lbSponsors" PostBackUrl="Sponsors.aspx" runat="server">Lägg till sponsorer</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lbShowSponsors" PostBackUrl="ShowSponsors.aspx" runat="server">Visa sponsorer</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lbExhibitors" PostBackUrl="Exhibitors.aspx" runat="server">Lägg till utställare</asp:LinkButton>
<br />
<br />
    <rad:RadGrid ID="rgSponsors" runat="server" Width="450px" AutoGenerateColumns="False" 
                onneeddatasource="rgSponsors_NeedDataSource">
    <MasterTableView>
                    <Columns>
                        <rad:GridBoundColumn DataField="Name" HeaderText="Namn" />
                        <rad:GridBoundColumn DataField="Email" HeaderText="E-post" />
                        <rad:GridBoundColumn DataField="Monter" HeaderText="Monter" />
                        <rad:GridBoundColumn DataField="Place" HeaderText="Plats" />
                        <rad:GridTemplateColumn HeaderText="Sortera">
                        <ItemTemplate>
                            <asp:Button ID="moveDown1" CssClass="btnSortDown" ToolTip="Flytta uppåt i listan" runat="server" 
                                CommandArgument='<%# Eval("Id")%>'  Text="" onclick="moveDown_Click" />
                            <asp:Button  ID="moveUp1" CssClass="btnSortUp" ToolTip="Flytta neråt i listan" runat="server" 
                                CommandArgument='<%# Eval("Id")%>'  Text="" onclick="moveUp_Click" />
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
<%--                        <rad:GridTemplateColumn HeaderText="Ta bort">
                        <ItemTemplate>
                        <asp:ImageButton ID="DelTask" runat="server" CommandArgument='<%# Eval("Id")%>' 
                                ImageUrl='<%# getImage(Convert.ToBoolean(Eval("Hide")))%>' ToolTip="Aktivera/Deaktivera"
                                onclick="DelTask_Click">
                            </asp:ImageButton>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>--%>
                    </Columns>
                </MasterTableView>
    </rad:RadGrid>
</div>
</asp:Content>
