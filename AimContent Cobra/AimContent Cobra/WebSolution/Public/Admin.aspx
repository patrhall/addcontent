<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="WebSolution.Public.Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>           
            <asp:TextBox ID="tbTengil" runat="server" TextMode="Password" />            
        </p>
        <p>
           <asp:Button ID="btTengil" runat="server" Text="All makt åt Tengil" OnClick="btTengil_Katla" /> 
        </p>
        <asp:Panel ID="pnlLoggedIn" runat="server" Visible="false">
            <p>
                <asp:DropDownList ID="ddUsers" runat="server" />
            </p>
            <p>
                <asp:Button ID="btLogin" runat="server" OnClick="btLogin_Click" Text="Logga in" />
            </p>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
