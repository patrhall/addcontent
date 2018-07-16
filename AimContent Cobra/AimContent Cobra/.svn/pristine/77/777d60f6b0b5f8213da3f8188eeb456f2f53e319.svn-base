<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="True"
    CodeBehind="Content.aspx.cs" Inherits="AimTags.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/Tags.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<br />
<br />
<asp:Label ID="lblChooseTagType" runat="server" />
<asp:DropDownList ID="ddlTagTypes" Visible="false" AutoPostBack="true"
            Width="200px" runat="server" />
            <br />
            <br />
    <rad:RadGrid AutoGenerateColumns="false" OnItemDataBound="rgContent_ItemDataBound" OnItemCommand="rgContent_ItemCommand" ID="rgContent" runat="server" Width="100%"
        OnItemCreated="rgContent_ItemCreated">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID">
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNew" runat="server" CommandName="new" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridBoundColumn DataField="Name" />
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:Repeater ID="rptTags" OnItemCommand="rptTags_ItemCommand" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton CssClass="tag" OnClientClick="return confirm('Vill du ta bort denna tagg från innehållet?');" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("TagName") %>' CommandName="delete" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </rad:GridTemplateColumn>
                <rad:GridBoundColumn DataField="DateAdded" />
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