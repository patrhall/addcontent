<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="EditPage.aspx.cs" Inherits="AimApp.EditPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">               
    <asp:PlaceHolder ID="phPagePreference" runat="server" Visible="false" />
    <div>
        <asp:Panel ID="pnlContent" runat="server">		    
            <div>
                <asp:LinkButton ID="lbPreference" runat="server" OnClick="lbPreference_OnClick" />
            </div>
	        <div style="width:90%;">  
                <asp:Panel ID="pnlTemplates" runat="server" Visible="false">
                    <p style="font-weight:bold;">Mallar</p>
                    Här väljs den mall man vill att sidan skall följa. Tillgängliga mallar visas nedan:<br />
                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rblTemplates" runat="server" /><br />                      
                </asp:Panel>                     
                <rad:radeditor language="en-US" id="editor1" ToolsFile="/App_Data/RadControls/Editor/AppFile.xml" Height="480px"
                Width="320px" Runat="server" onsubmitclicked="editor1_SubmitClicked" EnableResize="false"/>                
                <asp:Panel ID="pnlObjectImage" runat="server" Visible="false">
                    <p style="font-weight:bold;">Bild</p>
                    <asp:Image ID="imgObjectImage" runat="server" Visible="false" /><br />
                    Nedan väljs bild till sidan, som visas i bildrutan på webbsidan:<br />
                    <asp:FileUpload ID="fuObjectImage" runat="server" />
	            </asp:Panel>     	            	            
                <div style="width:320px;text-align:right;">                    
                    <asp:Button ID="bRoleConfirm" Text="Spara" runat="server" Visible="true" OnClick="editor1_SubmitClicked"  /><br />
                </div>
            </div>    
	    </asp:Panel>				    
	</div>
</asp:Content>