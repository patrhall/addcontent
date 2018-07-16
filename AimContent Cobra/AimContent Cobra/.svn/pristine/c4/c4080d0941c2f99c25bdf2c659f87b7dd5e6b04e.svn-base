<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="AimUsers.Users" MasterPageFile="../../Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <rad:RadGrid ID="rgUsers" runat="server" AutoGenerateColumns="False" GridLines="None" OnNeedDataSource="gvList_NeedDataSource" AllowSorting="True" OnItemCommand="gvList_OnItemCommand" OnItemCreated="gvList_ItemCreated" AllowPaging="true" PageSize="50">
        <MasterTableView CellPadding="3" DataKeyNames="UserName" NoMasterRecordsText="Finns inga användare" CommandItemDisplay="Top">
            <PagerStyle Mode="NumericPages" Position="Bottom" />
            <PagerTemplate>
                Sida <%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %> av <%# (int)DataBinder.Eval(Container, "OwnerTableView.PageCount") %> <br />
                Bläddra bland användare: 
                <asp:LinkButton ID="lbPagePrev" CommandName="Page" CausesValidation="false" CommandArgument="Prev" runat="server">[Föregående sida]</asp:LinkButton>                
                <asp:LinkButton ID="lbPageNext" CommandName="Page" CausesValidation="false" CommandArgument="Next" runat="server">[Nästa sida]</asp:LinkButton>
            </PagerTemplate>
            <CommandItemTemplate>
                <asp:LinkButton ID="lbNewUser" runat="server" OnClick="lbNewUser_OnClick"><img src="/Images/newobject.gif" alt="" /> Skapa ny användare</asp:LinkButton>
            </CommandItemTemplate>
            <Columns>                                                  
                <rad:GridBoundColumn DataField="UserName" HeaderText="Användarnamn">
                    <HeaderStyle Width="200px" />
                </rad:GridBoundColumn>                    
                <rad:GridTemplateColumn HeaderText="Namn" SortExpression="NameLast,NameFirst">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem,"NameFirst") %> <%# DataBinder.Eval(Container.DataItem,"NameLast") %>
                    </ItemTemplate>
                </rad:GridTemplateColumn>   
                <rad:GridBoundColumn DataField="IsApproved" HeaderText="Verifierad">
                    <HeaderStyle Width="60px" />
                </rad:GridBoundColumn>                  
                <rad:GridBoundColumn DataField="CreationDate" HeaderText="Skapad">
                    <HeaderStyle Width="70px" />
                </rad:GridBoundColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="CHANGE">
                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                </rad:GridButtonColumn>
                <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/delete.gif" ConfirmText="Detta kommer radera användaren\nÄr du säker?" CommandName="REMOVE">
                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                    <ItemStyle Width="20px" />
                </rad:GridButtonColumn>
            </Columns>               
        </MasterTableView>
        <ClientSettings ReorderColumnsOnClient="True">
            <Resizing AllowColumnResize="True"></Resizing>
        </ClientSettings>
    </rad:RadGrid>
    <asp:Panel ID="pnlEdit" runat="server" Visible="false">
        <asp:HiddenField ID="hfEditId" runat="server" Value="-1" />
        <table>  
            <tr>
                 <td colspan="2" style="text-align:right;">
                    <asp:ImageButton ID="ibBack" runat="server" OnClick="ibBack_Click" ImageUrl="/Images/arrow_left_grey.gif" />
                 </td>
                <td> </td>
            </tr>      
	        <tr>
		        <td class="align_topleft">
			        <asp:Label ID="lblUserName" runat="server" />
		        </td>
		        <td class="align_topleft">									
		            <asp:textbox id="tbUserName" runat="server" Width="200px" />		            
		        </td>
		        <td class="align_topleft">	
		            <asp:RequiredFieldValidator ID="reqValUsername" runat="server" ControlToValidate="tbUserName" ValidationGroup="editing"  />		
		        </td>
	        </tr>
	        <tr>
		        <td class="align_topleft">
			        <asp:Label ID="lblPassword" runat="server" />
		        </td>
		        <td class="align_topleft">
			        <asp:textbox id="tbPassword" runat="server" Width="200px" TextMode="Password" />		            
		        </td>
		        <td class="align_topleft">			
                    <asp:RequiredFieldValidator ID="reqValPassword" runat="server" ControlToValidate="tbPassword" ValidationGroup="editing"  />
		        </td>
	        </tr>
	        <tr>
		        <td class="align_topleft">
			        <asp:Label ID="lblEditEmail" runat="server" />
		        </td>
		        <td class="align_topleft">
			        <asp:textbox id="tbEmail" runat="server" Width="200px" /></td>
		        <td class="align_topleft"></td>
	        </tr>
	        <tr>
		        <td class="align_topleft">
			        <asp:Label ID="lblFirstname" runat="server" />
		        </td>
		        <td class="align_topleft">
			        <asp:textbox id="tbFirstname" runat="server" Width="200px" /></td>
		        <td class="align_topleft"></td>
	        </tr>
	        <tr>
		        <td class="align_topleft">
			        <asp:Label ID="lblLastname" runat="server" />
		        </td>
		        <td class="align_topleft">
			        <asp:textbox id="tbLastname" runat="server" Width="200px" /></td>
		        <td class="align_topleft"></td>
	        </tr>
	        <tr>
		        <td class="align_topleft">  
			        <asp:Label ID="lblEditVerify" runat="server" />
		        </td>
		        <td class="align_topleft">
			        <asp:CheckBox id="cbVerify" runat="server" />
		        </td>
		        <td class="align_topleft">			
		        </td>
	        </tr>
	        <tr>
		        <td class="align_topleft">
			        <asp:Label ID="lblRoles" runat="server" />
		        </td>
		        <td class="align_topleft">
			        <asp:checkboxlist id="listGroups" runat="server" />
		        </td>
		        <td class="align_topleft">			
		        </td>
	        </tr>
	        <tr>
	            <td colspan="2" style="text-align:right;">
	                <asp:button id="btnSave" Runat="server" ValidationGroup="editing" onclick="btnSave_Click" />
	            </td>
	            <td class="align_topleft">			
		        </td>
	        </tr>
        </table>	
    </asp:Panel>
</asp:Content>
