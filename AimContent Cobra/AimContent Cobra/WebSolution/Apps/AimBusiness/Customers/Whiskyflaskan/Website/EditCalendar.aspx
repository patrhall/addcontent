<%@ Page Language="C#" MasterPageFile="../../../../../Site.Master" AutoEventWireup="true" CodeBehind="EditCalendar.aspx.cs" Inherits="AimBusiness.Customers.Whiskyflaskan.Website.EditCalendar" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
		td{vertical-align:top;}
	</style>
	<link rel="stylesheet" href="Color/js_color_picker_v2.css" media="screen"/>
	<script type="text/javascript" src="Color/color_functions.js"></script>
	<script type="text/javascript" src="Color/js_color_picker_v2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-family:Verdana; font-size:x-small;">
		<tr>
			<td style="height: 16px">
			</td>
			<td colspan="2" style="text-align:right; height: 16px;">
				&nbsp;</td>
						
				<td style="height: 16px"></td>
		</tr>
		<tr>
		<td>
			</td>
			<td style="height:20px;" colspan="1"><asp:Label ID="lblHeader" Font-Names="Verdana" Font-Bold="True" runat="server" Text="Kalender"></asp:Label></td>
		</tr>
		<tr>
			<td>
			</td>
			<td style="width: 200px">Namn</td>
			<td style="width: 300px">Beskrivning</td>
			<td></td>
		</tr>
		<tr>
			<td style="">
			</td>
			<td style="width: 200px;"> 
				<asp:TextBox ID="tbName" runat="server" Width="200px"></asp:TextBox></td>
			<td style="width: 300px;">
				<asp:TextBox ID="tbDescription" runat="server" Width="300px" ></asp:TextBox>                        
			</td>
			<td style="height: 24px">
				&nbsp;</td>
		</tr>
		<tr>
			<td style="height:20px;">&nbsp;</td>
			<td colspan="5">
				<asp:Label ID="lblError" ForeColor="red" runat="server" Text=""></asp:Label></td>
		</tr>
		<tr>
			<td></td>
			<td>
				<div class="normal">
					<input class="textfield" id="filePic" type="file" runat="server"/>
				</div>
			</td>
			<td>
				Aktuell bild:
				<asp:Label ID="lblPicture" runat="server"></asp:Label></td>
			<td></td>
			<td></td>                                    
		</tr>                
				
	<tr>
	<td></td>
		<td>
			<table>
				<tr>                                                      
					<td class="normal" >
						Nuvarande färg:
					</td>
					<td>
						<asp:Label ID="lblChosenColor" runat="server" BorderColor="black" BorderStyle="Solid"
							BorderWidth="1" Height="10" Width="20"></asp:Label>
					</td>                            
				</tr>
				<tr>                            
					<td class="normal" style="width: 100px" >
						Välj ny färg:
						</td>
						<td>
						<input id="rgb2" maxlength="7" runat="server" name="rgb2" size="7" style="border-right: black 1px solid;
							border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;
							height: 20px" type="text" />
					</td>
					<td class="normal" > 
                        <asp:Button ID="colorPickBtn" runat="server" Text="Välj färg" />
					</td>
				</tr>
				<tr>                            
					<td class="normal" colspan="3">
					Meddelande
					</td>
				</tr>
				<tr>                           
					<td colspan="3">
						<asp:TextBox ID="tbMessage" runat="server" TextMode="MultiLine" Width="209px"></asp:TextBox></td>
				</tr>
			</table>
		</td>                
		<td></td>
	</tr> 
				
		<tr>
			<td>
			</td>
			<td style="width: 200px"> 
				<asp:Label ID="lblID" Visible="false" runat="server" Text=""></asp:Label></td>
			<td style="width: 300px">
				<asp:Button ID="btnSave" runat="server" Text="Spara kalender" OnClick="btnSave_Click" />
				<asp:Button ID="btnNew" runat="server" Text="Skapa lucka" OnClick="btnNew_Click" /></td>
			<td></td>
		</tr>
		<tr>
			<td style="height:20px;">&nbsp;</td>
		</tr>
		<tr>
			<td style="width: 40px">
				&nbsp;</td>
			<td style="width: 100%" valign="top" colspan="2">
	<asp:GridView ID="gvHatches" runat="server" AutoGenerateColumns="False" 
	RowStyle-CssClass="itemstyle" AlternatingRowStyle-CssClass="alterstyle" 
	HeaderStyle-CssClass="headerstyle" Width="100%" BorderWidth="1px" GridLines="None" 
	BorderColor="black" OnRowDataBound="gvHatches_DataBound">
	<Columns>							
		<asp:TemplateField>
			<ItemStyle Width="20px" HorizontalAlign="Center" />
		</asp:TemplateField>
		<asp:BoundField HeaderText="Lucka nr:" DataField="HatchNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />		        
		<asp:BoundField HeaderText="Datum:" DataField="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />		   		        
		<asp:TemplateField>
			<ItemTemplate>
				<asp:LinkButton ID="linkEdit" CommandName="EDIT1" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.LID") %>' Runat="server" OnCommand="gvHatches_Edit">Editera</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<ItemTemplate>
				<asp:LinkButton ID="linkDelete" CommandName="DELETE1" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.LID") %>' Runat="server" OnCommand="gvHatches_Delete">Radera</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>											
	</Columns>
		<RowStyle CssClass="itemstyle" Height="30px" />
		<AlternatingRowStyle CssClass="alterstyle" />
		<HeaderStyle CssClass="headerstyle" />
	</asp:GridView>
			</td>
			<td style="width: 100px">
			</td>
		</tr>                 
	</table>            
</asp:Content>


