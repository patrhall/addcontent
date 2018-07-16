<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="True"
    CodeBehind="TagList.aspx.cs" Inherits="AimTags.TagList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<br />
<br />
<asp:Label ID="lblChooseTagType" runat="server" />
<asp:DropDownList ID="ddlTagTypes" OnSelectedIndexChanged="ddlTagTypes_SelectedIndexChanged" AutoPostBack="true"
            Width="200px" runat="server" />
            <br />
            <br />
    <rad:RadGrid AutoGenerateColumns="false" OnItemCommand="rgTags_ItemCommand" ID="rgTags" runat="server" Width="100%"
        OnItemCreated="rgTags_ItemCreated">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID">
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNew" runat="server" CommandName="new" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridBoundColumn DataField="Name" />
                <rad:GridBoundColumn DataField="Slug" />
                <rad:GridBoundColumn DataField="TagType" />
                <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="editadmin">
                    <HeaderStyle Width="20px" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" ConfirmText="Är du säker?" ImageUrl="/Images/delete.gif"
                    CommandName="removeadmin">
                    <HeaderStyle Width="20px" />
                </rad:GridButtonColumn>
            </Columns>
        </MasterTableView>
    </rad:RadGrid>
</asp:Content>
