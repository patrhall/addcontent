<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="CreateSiteNews.aspx.cs" Inherits="AimSiteNews.CreateSiteNews" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server"> 
    <div>
        <p>
            <asp:Label id="NewsTitle" runat="server"></asp:Label><br />
            <asp:TextBox id="tbTopic" runat="server" Width="400px" /><br />
            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Du måste fylla i rubrik!" ControlToValidate="tbTopic" CssClass="error" />
        </p>
        <p><asp:CheckBox  ID="cbHeadNews" text="Huvudrelease" runat="server" /></p>			        
    </div>   
    <asp:Panel ID="pnlPreText" runat="server">
        <span class="title">Ingress:</span><br />
        <asp:TextBox ID="tbPreText" runat="server" TextMode="MultiLine" Rows="10" Columns="40"></asp:TextBox>
    </asp:Panel>             			    		            
    <p>
        <rad:radeditor language="en-US" id="editor1" ToolsFile="/App_Data/RadControls/Editor/ToolsFile.xml" Width="800px" Height="500px" Runat="server" />
    </p>
    <asp:Panel ID="pnlCurrentImageFile" runat="server">
        <p>
            <span class="title">Nuvarande bild: </span>
            <asp:Label ID="lblCurrentNewsImgFile" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Image ID="imgCurrentNewsImg" runat="server" Width="50px" Visible="false" />
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlImageUploader" runat="server">
        <p>
            <span class="title">Bilduppladdning</span><br />
            Om du vill kan du ladda upp en separat bild till nyheten.<br />
            <asp:FileUpload ID="fuImageUploader" runat="server" Width="400px" />    
            <br />  
            <asp:CheckBox ID="cbDeleteImage" runat="server" Text="Ta bort bild" Visible="false" />
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlRegistration" runat="server">
        <p class="title">Anmälan</p>
        <p>                       
            Skall det finnas möjlighet till anmälan?<br />
            <asp:RadioButtonList ID="rblRegistration" runat="server" RepeatColumns="2">
                <asp:ListItem>Ja</asp:ListItem>
                <asp:ListItem Selected="True">Nej</asp:ListItem>
            </asp:RadioButtonList>                        
            <br />         
            Formulär som anmälan skall vara kopplad till       
            <asp:RadioButtonList CssClass="normal" ID="rblForm" AutoPostBack="false" runat="server">
            </asp:RadioButtonList>
        </p>
    </asp:Panel>
    
    <asp:PlaceHolder ID="phPagePreference" runat="server" /> 
    <asp:Button ID="btSave" runat="server" Text="Spara nyhet" OnClick="btSave_OnClick" />
    
</asp:Content>
