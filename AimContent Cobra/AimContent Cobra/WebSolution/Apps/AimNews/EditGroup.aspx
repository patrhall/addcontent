<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="True"
    CodeBehind="EditGroup.aspx.cs" Inherits="AimNews.EditGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <div style="padding-top: 10px; padding-bottom: 10px;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                    </td>
                    <td valign="top">
                        <asp:ImageButton ID="ibBack" runat="server" ImageUrl="/images/arrow_left_grey.gif"
                            OnClick="ibBack_OnClick" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 600px;">
                        <asp:Label ID="lblHeader" runat="server" CssClass="title"></asp:Label><br />
                        <asp:TextBox ID="tbHeader" runat="server" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvHeader" ValidationGroup="vgGroup" ControlToValidate="tbHeader"
                            runat="server"></asp:RequiredFieldValidator><br />
                        <br />
                        <asp:Label ID="lblDescription" runat="server" CssClass="title"></asp:Label><br />
                        <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Rows="3" Width="350px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 10px;">
                        <asp:Label ID="lblUsers" runat="server" CssClass="title"></asp:Label><br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 5px;">
                        <rad:radgrid id="rgUsers" runat="server" width="100%" onneeddatasource="rgUsers_NeedDataSource"
                            onitemcommand="rgUsers_ItemCommand" onitemcreated="rgUsers_ItemCreated" allowsorting="true"
                            PagerStyle-Mode="NextPrevAndNumeric" allowpaging="true" gridlines="None" pagesize="100" autogeneratecolumns="false">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID">                    
            <CommandItemTemplate>
            <asp:LinkButton ID="lbNewUser" runat="server" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridTemplateColumn SortExpression="FirstName" Visible="false" UniqueName="FirstName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%> 
                    </ItemTemplate>
                   <HeaderStyle Width="20%" />
                </rad:GridTemplateColumn>
                <rad:GridTemplateColumn SortExpression="LastName" Visible="false" UniqueName="LastName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LastName")%> 
                    </ItemTemplate>
                   <HeaderStyle Width="20%" />
                </rad:GridTemplateColumn>
                  <rad:GridTemplateColumn SortExpression="Email" Visible="false" UniqueName="Email">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Email")%> 
                    </ItemTemplate>
                   <HeaderStyle Width="30%" />
                </rad:GridTemplateColumn>
                  <rad:GridTemplateColumn SortExpression="Company" Visible="false" UniqueName="Company">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Company")%> 
                    </ItemTemplate>
                   <HeaderStyle Width="20%" />
                </rad:GridTemplateColumn>
              
                <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="editUser" >                    
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteButton"  ImageUrl="/Images/delete.gif" CommandName="deleteUser" >
                    <HeaderStyle Width="5%" />
                </rad:GridButtonColumn>
            </Columns>
        </MasterTableView>        
    </rad:radgrid>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 5px;">
                        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" /><br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; padding-top: 5px;">
                        <asp:Button ID="btnSave" CssClass="inputs" runat="server" ValidationGroup="vgGroup"
                            Visible="true" OnClick="btnSave_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblStatus" runat="server" Visible="false" CssClass="confirm"></asp:Label>
        </div>
    </div>
</asp:Content>
