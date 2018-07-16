<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true"
    CodeBehind="FormAnswers.aspx.cs" Inherits="AimForm.FormAnswers" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div style="width: 100%;">
        <asp:Label ID="lblNoAnswers" runat="server" Visible="false"></asp:Label>
        <asp:HyperLink ID="hlReturnToList" Text="Återvänd till listan" NavigateUrl="/Apps/AimForm/FormList.aspx"
            runat="server"></asp:HyperLink>
        
        <asp:ImageButton ID="imgbtnExcel" OnClick="ibExcel_OnClick" ImageUrl="..\..\Images\excel.gif" AlternateText="" runat="server" />
        
        <rad:RadGrid ID="rgformanswers"  runat="server" Width="100%" OnNeedDataSource="rgformanswers_NeedDataSource"                 
                   OnItemDataBound="rgformanswers_ItemDataBound" OnItemCreated="rgformanswers_ItemCreated" OnPreRender="rgformanswers_PreRender" AllowSorting="true" 
                   AllowPaging="true" GridLines="None" PageSize="30" AutoGenerateColumns="true" >
                   <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                    <MasterTableView CommandItemDisplay="Top">                                            
                        <CommandItemTemplate>
                            <asp:LinkButton ID="lbNew" runat="server" CommandName="new" />
                        </CommandItemTemplate>
                        <Columns>                            
                        </Columns>                        
                    </MasterTableView>                         
        </rad:RadGrid>
        
        <asp:DataGrid Visible="false" ID="dgFormAnswers" runat="server" BorderColor="black" BorderStyle="Solid"
            CellSpacing="0" BorderWidth="1px" BackColor="White" CellPadding="3" GridLines="None"
            AutoGenerateColumns="true" Width="100%" AllowSorting="true" ShowHeader="true"
            OnItemDataBound="dgFormAnswers_ItemDataBound">
            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#9471DE"></SelectedItemStyle>
            <AlternatingItemStyle CssClass="alterstyle" BackColor="#e1e1e1"></AlternatingItemStyle>
            <ItemStyle CssClass="itemstyle" Width="100%"></ItemStyle>
            <HeaderStyle CssClass="headerstyle" Font-Bold="true" BackColor="#e1e1e1"></HeaderStyle>
            <Columns>
            </Columns>
            </asp:DataGrid>
    </div>
</asp:Content>
