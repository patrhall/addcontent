<%@ Page Language="C#" MasterPageFile="../../../../../Site.Master" AutoEventWireup="true" CodeBehind="HandleCalendarHatches.aspx.cs" Inherits="AimBusiness.Customers.Whiskyflaskan.Website.HandleCalendarHatches" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
		td{vertical-align:top;}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
	<script type="text/javascript">
		/*<![CDATA[*/
		var popup, calendarContainer;

		function HideCal()
		{
			if (popup && popup.IsVisible())
			{
				popup.Hide();
			}
		}
				
		function PositionX(inputElement) 
		{
			var offsetLeft = 0;
			if (inputElement.offsetParent)
			{
				while (inputElement.offsetParent)
				{
					offsetLeft += inputElement.offsetLeft;
					inputElement = inputElement.offsetParent;
				}
			}
			else if (inputElement.x)
			{
				offsetLeft += inputElement.x;
			}
			return offsetLeft;
		}
		function PositionY(inputElement) 
		{
			var offsetTop = 0;
			if (inputElement.offsetParent)
			{
				while (inputElement.offsetParent)
				{
					offsetTop += inputElement.offsetTop;
					inputElement = inputElement.offsetParent;
				}
			}
			else if (inputElement.y)
			{
				offsetTop += inputElement.y;
			}
			return offsetTop;
		}

		function onDateClick(renderDay)
		{     
			HideCal();    
		}
		/*]]>*/
	</script>
	<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-family:Verdana; font-size:x-small;">
		<tr> 
			<td></td>                   
			<td style="height:20px;" colspan="1"></td>
			<td>
				<asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label></td>
		</tr>
		<tr>
			<td style="width:20px"></td>                   
			<td>
				<asp:Label ID="lblHeader" Font-Names="Verdana" Font-Bold="True" runat="server" Text="Lucka nr:"></asp:Label></td>
			<td>
				<asp:Label ID="lblDate" Font-Names="Verdana" Font-Bold="True" runat="server" Text="Datum:"></asp:Label></td>
		</tr>
		<tr>
			<td></td>                   
			<td>
				<asp:TextBox ID="tbNo" runat="server" Width="30px"></asp:TextBox></td>
			<td>
				<rad:RadDatePicker ID="datePicker" runat="server" />
			</td>
		</tr>
		<tr>
			<td></td>                   
			<td></td>
			<td></td>
		</tr>                 
		<tr>
			<td></td>                   
			<td colspan="2">
				<rad:radeditor HasPermission="true" language="en-US" id="editor1" ToolsFile="ToolsFile.xml"
						Width="600px" Height="460px" Runat="server"></rad:radeditor>
				<asp:Button ID="editorSubmit" runat="server" Text="Skicka" OnClick="editor1_SubmitClicked" />
			</td>             
		</tr>              
	</table>
</asp:Content>
