<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs"
    Inherits="AimBusiness.Customers.Boletus.Website.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <div>
            <p>
                Intern benämning av artikel<br />
                <asp:TextBox ID="tbName" runat="server" Width="300px" /> <asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="tbName" Text="*" ForeColor="Red" ValidationGroup="spara" Font-Bold="true" />
            </p>
            <p>
                <asp:Image ID="imgProductImage" runat="server" Visible="false" />
            </p>
            <p>
                Ladda upp en bild till produkten:<br />
                <asp:FileUpload ID="fuProductImage" runat="server" Width="300px" />
            </p>
        </div>
        <div>
            <asp:Repeater ID="rptProductItems" runat="server" OnItemDataBound="rptProductItems_Bound">
                <ItemTemplate>
                    <asp:HiddenField ID="hfLanguage" runat="server" />
                    <p style="font-weight: bold; font-size: 12px;">
                        <asp:Label ID="lblLanguageTitle" runat="server" /></p>
                    <p>
                        Namn:<br />
                        <asp:TextBox ID="tbName" runat="server" Width="300px" />
                    </p>
                    <p>
                        Gruppnamn:<br />
                        <asp:TextBox ID="tbGroupName" runat="server" Width="300px" />
                    </p>
                    <p>
                        Guidetext:<br />
                        <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Width="300px"
                            Rows="5" />
                    </p>
                </ItemTemplate>
                <SeparatorTemplate>
                    <!--<hr style="width: 300px; text-align: left;" />-->
                </SeparatorTemplate>
            </asp:Repeater>
        </div>
        <div>
            <p class="title">
                Produkten skall synas under:</p>
            <asp:Repeater ID="rptCategoryGroups" runat="server" OnItemDataBound="rptCategoryGroups_Bound">
                <ItemTemplate>
                    <p>
                        <asp:Label ID="lblCategoryGroupTitle" runat="server" Font-Bold="true" /><br />
                        <asp:CheckBoxList ID="cblCategories" runat="server" />
                    </p>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <p>
                <asp:Button ID="btSave" runat="server" Text="Spara" OnClick="btSave_Click" ValidationGroup="spara" />&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hlCancel" runat="server" Text="Återgå utan att spara" NavigateUrl="Overview.aspx" /><br /><br />
                <asp:RequiredFieldValidator ID="reqName2" runat="server" ControlToValidate="tbName" Text="Internt namn måste anges" ForeColor="Red" ValidationGroup="spara" />
            </p>
        </div>
    </div>
</asp:Content>
