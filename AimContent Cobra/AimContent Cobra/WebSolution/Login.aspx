<%@ Page language="c#" Inherits="AIM.Login" Codebehind="Login.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>Aimit Solutions AB - AimContent - Login</title>		
		<link rel="Stylesheet" type="text/css" href="/css/General.css" />
		<link rel="Stylesheet" type="text/css" href="/css/Login.css" />
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" /> 
		<div>
		    <div id="login_div_toplogo">
		        <a href="http://www.aimit.se" target="_blank"><img src="/Images/login/topright_logo.jpg" alt="Aimit Solutions" /></a>
		    </div>
		    <div id="login_div_wholebox">
		        <div id="login_div_loginbox">		        
	                <table id="login_tbl_login">
	                    <tr>
	                        <td class="login_td_description">
	                            <asp:Label ID="lblUserName" runat="server" />
	                        </td>
	                        <td>
	                            <asp:TextBox id="tbUserName" TabIndex="1" runat="server" Width="120px" />
	                        </td>
	                        <td>
	                            <asp:DropDownList ID="ddLanguage" runat="server" TabIndex="4" AutoPostBack="true" Width="100px">
	                                <asp:ListItem Value="Swedish" Text="Svenska" />
	                                <asp:ListItem Value="English" Text="English" />
	                            </asp:DropDownList>
	                        </td>
	                    </tr>
	                    <tr>
	                        <td class="login_td_description">
	                            <asp:Label ID="lblPassWord" runat="server" />
	                        </td>
	                        <td>
	                            <asp:textbox id="tbPassword" runat="server" TabIndex="2" TextMode="Password" Width="120px" />
	                        </td>
	                        <td>
	                            <asp:button id="btnLogin" runat="server" TabIndex="3" Text="Logga in" onclick="btnLogin_Click" Width="97px" />
	                        </td>
	                    </tr>
	                </table>
	                <table id="login_tbl_bottom">
	                    <tr>
	                        <td>
	                            <span id="span_password" class="mouselink">
	                                <img style="vertical-align:middle;" src="/Images/login/forgotpassword_icon.jpg" id="test" alt="Glömt lösenord" />
	                                <asp:Label ID="lblForgetPassword" runat="server" />
	                            </span>
	                            
	                        </td>	                        
	                        <td>
	                            <span id="span_about" class="mouselink">
	                                <img style="vertical-align:middle;" src="/Images/login/readmore_icon.jpg" alt="Läs mer" />	                        
	                                <asp:Label ID="lblReadMore" runat="server" />
	                            </span>
	                        </td>
	                    </tr>
	                </table>		        
		        </div>
		    </div>
		</div>					
		<rad:RadWindowManager ID="rwmManager" runat="server" Width="450px" Height="350px" VisibleStatusbar="False">		
		    <Windows>
		        <rad:RadWindow ID="rwAbout" runat="server" OpenerElementID="span_about" />
		        <rad:RadWindow ID="rwPassword" runat="server" OpenerElementID="span_password" />
		    </Windows>
		</rad:RadWindowManager>
		
		</form>
	</body>
</html>
