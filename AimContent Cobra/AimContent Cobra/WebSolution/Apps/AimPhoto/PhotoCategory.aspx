<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true" CodeBehind="PhotoCategory.aspx.cs" Inherits="AimPhoto.PhotoCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server"> 

<div>
    <asp:Label ID="lblCategory" CssClass="title" runat="server" Font-Bold="true"></asp:Label><br />
    <table style="border:solid 2px black">        
    <tr>
        <td style="width: 158px; height: 21px">
            <asp:Label ID="lblCategoryName" runat="server" CssClass="title" /></td>
        <td style="width: 3px; height: 21px;">
            <asp:Label ID="lblCategoryComment" runat="server" CssClass="title" /></td>
        <td style="height: 21px"></td>
        <td><asp:HyperLink ID="hpBack" runat="server" /></td>
    </tr>
    <tr>
        <td style="width: 158px">
            <asp:TextBox ID="tbCategoryName" runat="server"></asp:TextBox></td>
        <td style="width: 3px" rowspan="2">
            <asp:TextBox ID="tbCategoryComment" runat="server" Height="113px" TextMode="MultiLine"></asp:TextBox></td>
        <td>
            <asp:Button ID="btnCategoryName" runat="server" OnClick="btnCategoryName_Click" /></td>
        <td></td>    
    </tr>
    <tr>
        <td style="width: 158px">            
            <asp:Image ID="imgAlbumCover" runat="server"/>           
            </td>
        <td>
            <asp:Label ID="lblUpdated" runat="server" CssClass="message" Visible="False" />
            <asp:Label ID="lblAlbumCover" runat="server" Visible="False" />
            
        </td>
        <td></td>                
    </tr>
    <tr >
        <td>
            <asp:Label ID="lblRoleTitle" runat="server" />
            <asp:CheckBoxList ID="cblRoles" runat="server" />
        </td>
        <td>
            <asp:Button ID="btRoles" Visible="false" Width="120px" runat="server" OnClick="btRoles_OnClick" />
        </td>            
    </tr>
    </table>        
    <br />      
    
    <asp:HyperLink ID="hpUploadPics" runat="server" /><br />
    
    <p class="title"><asp:Label ID="lblPicTitle" runat="server" /></p>
    <p style="width:430px;"><asp:Label ID="lblPicDescription" runat="server" /></p>
    <div style="width:630px;text-align:right;">                                        
        <asp:Button ID="btnSavePicCom" runat="server" OnClick="btnSavePic_Click" /><br />
    </div>
            
    <asp:GridView ID="gvItems" DataKeyNames="ID" runat="server" BorderWidth="0" Width="600px" CellPadding="3" GridLines="None" CellSpacing="2" ShowHeader="false" OnRowDataBound="gvItems_RowDataBound" OnRowCommand="gvCategoryListRowCommand" AutoGenerateColumns="false">
        <Columns>                
            <asp:ImageField DataImageUrlField="Filename">
                <ItemStyle CssClass="align_topleft" />
            </asp:ImageField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:TextBox ID="gvTbPicDesc" runat="server" Width="95%" Height="84px" TextMode="MultiLine" AutoPostBack="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Button ID="btnSavePicCom" runat="server" Text="Spara" OnClick="btnSavePicCom_Click" />
            </ItemTemplate><ItemStyle VerticalAlign="top"/>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Link" CommandName="UP" ItemStyle-VerticalAlign="top" ItemStyle-Width="20px" />
            <asp:ButtonField ButtonType="Link" CommandName="DOWN" ItemStyle-VerticalAlign="top" ItemStyle-Width="20px" />
            <asp:ButtonField ButtonType="Link" CommandName="setAlbumCover" ItemStyle-VerticalAlign="top" ItemStyle-Width="20px" />                
            <asp:ButtonField ButtonType="Link" CommandName="del" ItemStyle-VerticalAlign="top" ItemStyle-Width="20px" />                
        </Columns>
        <AlternatingRowStyle BackColor="#DDDDDD" />
    </asp:GridView>
    <div style="width:630px;text-align:right;">            
        <asp:Button ID="btnSavePic" runat="server" OnClick="btnSavePic_Click" /><br />
    </div>            
    <asp:Label ID="lbloID" runat="server" Visible="false"></asp:Label>
</div>

</asp:Content>