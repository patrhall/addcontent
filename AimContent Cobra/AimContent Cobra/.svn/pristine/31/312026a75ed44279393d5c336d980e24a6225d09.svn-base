<%@ Page Language="C#" MasterPageFile="../../Site.Master"  AutoEventWireup="true" CodeBehind="FormList.aspx.cs" Inherits="AimForm.FormList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<table style="height: 250px; width: 100%;">            
            <tr>
                <td valign="top" colspan="2" style="padding-left:20px;">
                    <br />
                    <rad:RadGrid ID="rgForm"  runat="server" Width="100%" OnNeedDataSource="rgForm_NeedDataSource"                 
                   AllowSorting="true"                   
                   OnItemCommand="rgForm_ItemCommand"                   
                   AllowPaging="true" GridLines="None" PageSize="30" AutoGenerateColumns="false" >
                   <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                    <MasterTableView CommandItemDisplay="Top" ClientDataKeyNames="FormID">                                            
                        <CommandItemTemplate>
                            <asp:LinkButton ID="lbNew" runat="server" CommandName="new" />
                        </CommandItemTemplate>
                        <Columns> 
                            <rad:GridTemplateColumn HeaderText="Formulär">
                                <ItemStyle ></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkEdit" CommandName="redigera" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.FormID") %>'
                                        runat="server">
											<%# DataBinder.Eval(Container, "DataItem.FormName") %>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                              
                                             
                            <rad:GridButtonColumn ButtonType="LinkButton" Visible="true" Text="Redigera" UniqueName="linkRegistration" CommandName="redigera">                    
                                <HeaderStyle  />
                                 </rad:GridButtonColumn>                                     
                            <rad:GridButtonColumn ButtonType="LinkButton" Visible="true" ConfirmText = "Är du säker på att du vill TA BORT formuläret och alla dess svar?" Text="Radera" UniqueName="linkRegistration" CommandName="radera">                    
                                <HeaderStyle />
                                 </rad:GridButtonColumn>
                            <rad:GridButtonColumn ButtonType="LinkButton" Visible="true" Text="Visa svaren" UniqueName="linkRegistration" CommandName="Answers">                    
                                <HeaderStyle />
                                 </rad:GridButtonColumn>
                            <rad:GridButtonColumn ButtonType="LinkButton" Visible="true" ConfirmText = "Är du säker på att du vill kopiera detta formuläret?" Text="Kopiera" UniqueName="copyForm" CommandName="copyForm">                    
                                <HeaderStyle />
                                 </rad:GridButtonColumn>
                            
                            <rad:GridTemplateColumn Visible="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FormID") %>'
                                        ID="Label1" NAME="Label1">
                                    </asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>    
                        </Columns>                        
                    </MasterTableView>                         
        </rad:RadGrid>                                         
                </td>
            </tr>
            <tr>
                <td style="padding-left:20px;">
   
                    <asp:Button ID="btnNew" runat="server" Text="Nytt formulär" CssClass="inputs" OnClick="btnNew_Click">
                        </asp:Button><br /><br />
                </td>
                <td valign="top" align="left">
                
                </td>
            </tr>
        </table>
</asp:Content>
