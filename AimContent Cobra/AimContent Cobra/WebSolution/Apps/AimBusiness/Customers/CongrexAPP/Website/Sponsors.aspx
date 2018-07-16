<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Sponsors.aspx.cs" Inherits="AimBusiness.Customers.CongrexAPP.Website.Sponsors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
<div>
    <asp:LinkButton ID="lbSponsors" PostBackUrl="ShowSponsors.aspx" runat="server">Visa sponsorer</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lbExhibitors" PostBackUrl="Exhibitors.aspx" runat="server">Lägg till utställare</asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="lbShowExhibitors" PostBackUrl="ShowExhibitors.aspx" runat="server">Visa utställare</asp:LinkButton>
<br />
<br />
Namn:<asp:TextBox ID="txtName" runat="server" /><br />
Länk:<asp:TextBox ID="txtLink" runat="server" /><br />
<br /><br />
Logo:<br />
<asp:FileUpload ID="FileUpload1" runat="server" /><br /><br />
<asp:Button ID="btnSave" OnClick="SaveSponsor" runat="server" Text="Spara" />
</div>
</asp:Content>
