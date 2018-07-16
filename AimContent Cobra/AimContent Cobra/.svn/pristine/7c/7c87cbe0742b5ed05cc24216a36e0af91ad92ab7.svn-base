<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="Administrators.aspx.cs" Inherits="AimAdministrator.Administrators" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <rad:RadGrid ID="rgAdministrators" runat="server" Width="100%" 
        OnNeedDataSource="rgAdministrators_NeedDataSource"                 
        OnItemCommand="rgAdministrators_ItemCommand"
        OnItemCreated="rgAdministrators_ItemCreated"
        AllowSorting="true" AllowPaging="true" GridLines="None" PageSize="30"
        AutoGenerateColumns="false"                
    >
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID">                    
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNewAdmin" runat="server" CommandName="new" />
            </CommandItemTemplate>
            <Columns>
                <rad:GridBoundColumn DataField="username" />
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem,"firstname") %> <%# DataBinder.Eval(Container.DataItem,"lastname") %>
                    </ItemTemplate>
                </rad:GridTemplateColumn>
                <rad:GridBoundColumn DataField="email" HeaderText="E-mail" />                
                <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="editadmin" >                    
                    <HeaderStyle Width="20px" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteButton" ImageUrl="/Images/delete.gif" CommandName="removeadmin" >
                    <HeaderStyle Width="20px" />
                </rad:GridButtonColumn>
            </Columns>
        </MasterTableView>        
    </rad:RadGrid>    
    <asp:Panel ID="pnlEdit" runat="server" Visible="false">    
        <asp:HiddenField ID="hfID" runat="server" Value="-1" />
         <table>
            <tr>
                <td><asp:Label ID="lblFormTitle" runat="server" CssClass="title" /></td>
                <td colspan="2" class="align_middleright"><asp:ImageButton ID="ibBack" runat="server" ImageUrl="/images/arrow_left_grey.gif" OnClick="ibBack_OnClick" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFormUserName" runat="server" /></td>
                <td><asp:TextBox id="tbUserName" runat="server" Width="200px" /></td>
                <td><asp:RequiredFieldValidator id="reqUserName" runat="server" ValidationGroup="adminForm" Display="Dynamic" ControlToValidate="tbUserName" ErrorMessage="Du måste fylla i användarnamn" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFormPassword" runat="server" /></td>
                <td><asp:TextBox id="tbPassword" TextMode="Password" runat="server" Width="200px" /></td>
                <td><asp:RequiredFieldValidator id="reqPassword" runat="server" ValidationGroup="adminForm" Display="Dynamic" ControlToValidate="tbPassword" ErrorMessage="Du måste fylla i lösenord" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFormNameFirst" runat="server" /></td>
                <td><asp:TextBox id="tbFirstName" runat="server" Width="200px" /></td>
                <td><asp:RequiredFieldValidator id="reqFirstName" runat="server" ValidationGroup="adminForm" Display="Dynamic" ControlToValidate="tbFirstName" ErrorMessage="Du måste fylla i namn" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFormNamelast" runat="server" /></td>
                <td><asp:TextBox id="tbLastName" runat="server" Width="200px" /></td>
                <td><asp:RequiredFieldValidator id="reqLastName" runat="server" ValidationGroup="adminForm" Display="Dynamic" ControlToValidate="tbLastName" ErrorMessage="Du måste fylla i efternamn" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFormEmail" runat="server" /></td>
                <td><asp:TextBox id="tbEmail" runat="server" Width="200px" /></td>
                <td></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblAdminLanguage" runat="server" /></td>
                <td><asp:DropDownList ID="ddAdminLanguage" runat="server" Width="205px" /></td>
                <td></td>
            </tr>
            <tr id="tr_roles" runat="server">
                <td><asp:Label ID="lblUserGroup" runat="server" /></td>
                <td><asp:RadioButtonList id="rblGroups" runat="server" /></td>
                <td></td>
            </tr>
            <tr id="tr1" runat="server">
                <td><asp:Label ID="lblMailNotifications" runat="server" /></td>
                <td><asp:CheckBox id="cbxMailNotifications" runat="server" /><asp:Label ID="lblCbxMailNotifications" runat="server" /></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3"><asp:Button id="btSave" Runat="server" ValidationGroup="adminForm" OnClick="btSave_OnClick" /> </td>
            </tr>
         </table>
    </asp:Panel>
</asp:Content>