<%@ Page Language="C#" MasterPageFile="../../Site.Master"  AutoEventWireup="true" CodeBehind="ImportGroup.aspx.cs" Inherits="AimNews.ImportGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<div>
<div style="height:30px;">
<asp:Label ID="lblTemplateText" runat="server"></asp:Label>&nbsp;&nbsp;<a id="aExcelLink" runat="server" target="_blank"><img style="border:0px;" src="/Images/excel.gif" /></a> 
</div>
 <div style="width:840px; text-align:right;">
<asp:ImageButton ID="ibBack" runat="server" ImageUrl="/images/arrow_left_grey.gif"
                            OnClick="ibBack_OnClick" />
                            </div>
<rad:RadUpload ID="ruImport" runat="server" ControlObjectsVisibility="None" />
                       

                            <asp:Button ID="btnImport" runat="server" CssClass="RadUploadButton" OnClick="btnImport_Click" />
                            <asp:Label ID="lblStatus" runat="server" CssClass="confirm"></asp:Label>
</div>                            
</asp:Content>