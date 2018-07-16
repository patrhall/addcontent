<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="PhotoMultipleAdmin.aspx.cs" Inherits="AimPhoto.PhotoMultipleAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script language="JavaScript" type="text/javascript">
	    function mysubmit()
	    {	   	        	  	        	           			    			   			    			    
		    form1.ActiveXPowUpload.Upload();			    			    			    
		    return false;		    
	    }    		    
	    
	    function ShowServerResponse()
	    {
		    var responselable = document.getElementById("serverresponse");
		    var temp = document.getElementById("ActiveXPowUpload");
		    responselable.innerHTML = "Response code " + temp.GetServerStatusCode();
		    responselable.innerHTML += " " + temp.GetServerStatusText();
		    responselable.innerHTML += "<br />" + temp.GetServerReply(65001);		
	    }
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">     

    <form id="uploadform" method="post" action="PhotoMultipleUpload.aspx">
        <asp:PlaceHolder ID="phUploader" runat="server" />                                
    </form>
    <p><asp:Label ID="lblError" runat="server" CssClass="error" Visible="false" /></p>
    <p><asp:HyperLink ID="hpBack" runat="server" Visible="true" /></p>
    <button style="display:none;" onclick="javascript:ShowServerResponse();" value="asdf" />
    <div style="display:none;" id="serverresponse">&nbsp;</div>
</asp:Content>