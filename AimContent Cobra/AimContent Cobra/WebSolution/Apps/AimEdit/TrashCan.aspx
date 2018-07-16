<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="True" CodeBehind="TrashCan.aspx.cs" Inherits="AimEdit.TrashCan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div style="padding:5px 0 0 10px;">
        <asp:GridView ID="gvTrash" runat="server" DataKeyNames="ObjectID" AutoGenerateColumns="False" OnRowCommand="gvTrash_OnRowCommand" GridLines="None" BorderWidth="0" Width="500px">
            
            <Columns>                
                <asp:BoundField DataField="Title" HeaderText="Sidtitel">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" Font-Bold="true" />
                </asp:BoundField>
                <asp:BoundField DataField="ObjectType" HeaderText="Sidtyp">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" Font-Bold="true" />
                </asp:BoundField>
                <asp:BoundField DataField="TrashedDate" HeaderText="Raderingsdatum">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" Font-Bold="true" />
                </asp:BoundField>
                <asp:ButtonField ButtonType="Link" CommandName="GET" Text="Flytta till meny&nbsp&nbsp;" />
                <asp:ButtonField ButtonType="Image" CommandName="ERASE" ImageUrl="/Images/delete.gif" Text="Ta bort permanent" />                
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>    