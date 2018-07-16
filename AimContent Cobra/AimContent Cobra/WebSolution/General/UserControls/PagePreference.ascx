<%@ Control Language="C#" AutoEventWireup="true" Inherits="AIM.General.UserControls.PagePreference" Codebehind="PagePreference.ascx.cs" %>

<div id="propertiespanel">
    <fieldset>
        <legend><asp:Label ID="lblHeadTitle" CssClass="bigtitle" runat="server" /></legend>        
        <p><asp:Label ID="lblHeadDescription" runat="server" /></p>        
        <asp:Panel ID="pnlUrl" runat="server">
            <fieldset>
                <legend><asp:Label ID="lblUrlTitle" CssClass="title" runat="server" /></legend>
                <p>
                <asp:Label id="lblSlugTitle" runat="server" Text="Länktitel/kort titel" /><br />
                <asp:TextBox ID="tbSlug" runat="server" Width="250px" CssClass="inputs" />
                    <br />
                    <br />
                    <asp:Label ID="lblUrlDescription" runat="server" /><br />
                    <asp:HyperLink ID="hpURL" runat="server" />
                </p> 
            </fieldset><br />
        </asp:Panel>
        <asp:Panel ID="pnlRoles" runat="server" Visible="false">
            <fieldset>
                <legend><asp:Label ID="lblRolesTitle" CssClass="title" runat="server" /></legend>        
                <p>
                    <asp:Label ID="lblRolesDescription" runat="server" /><br />
                    <asp:CheckBoxList CssClass="normal" ID="cblRoleProperty" RepeatDirection="horizontal" runat="server" />        
                </p>
            </fieldset><br />
        </asp:Panel>     
        <asp:Panel ID="pnlShowInMenu" runat="server">
            <fieldset>
                <legend><asp:Label ID="lblShowInMenuTitle" runat="server" CssClass="title" /></legend><br />
                <asp:Label ID="lblShowInMenuDescription" runat="server" />
                <table class="normal">
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbShowInMenu" runat="server" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>                                                                                                
        <asp:Panel ID="pnlPublish" runat="server">    
            <fieldset>
                <legend><asp:Label ID="lblPublishTitle" runat="server" CssClass="title" /></legend><br />
                <asp:Label ID="lblPublishDescription" runat="server" />            
                <table class="normal">                    
                    <tr>   
                        <td colspan="2">
                            <asp:CheckBox ID="cbUsePublicationDate" runat="server" Text="Använd publiceringsdatum" />
                        </td>
                    </tr>
                    <tr>
                        <td class="normal">
                            <asp:Label ID="lblPublishFrom" runat="server" CssClass="title" /><br />
                            <rad:RadDateTimePicker ID="calFrom" Width="240px" runat="server"></rad:RadDateTimePicker>
                        </td>
                        <td class="normal">
                            <asp:Label ID="lblPublishTo" runat="server" CssClass="title" /><br />
                            <rad:RadDateTimePicker ID="calTo" Width="240px" runat="server"></rad:RadDateTimePicker>                            
                        </td>                        
                    </tr>            
                </table> 
            </fieldset><br />
        </asp:Panel>        
        <asp:Panel ID="pnlAccessRights" runat="server">
            <fieldset>
                <legend><asp:Label ID="lblAccessRightTitle" runat="server" CssClass="title" /></legend>
                <p>
                    <asp:Label ID="lblAccessRightDescription" runat="server" />                    
                </p>
                <div>
                    <asp:CheckBoxList ID="cblObjectUserAccessRight" runat="server" RepeatDirection="Horizontal" />
                </div>
            </fieldset>
        </asp:Panel>           
        <asp:Panel ID="pnlMeta" runat="server">
            <fieldset>
                <legend><asp:Label ID="lblMetaHeadTitle" runat="server" CssClass="title" /> </legend>
                <p>
                    <asp:Label ID="lblMetaHeadDescription" runat="server" />
                </p>
                <p>
                    <asp:Label ID="lblMetaTitle" runat="server" /><br />
                    <asp:TextBox ID="tbMetaTitle" runat="server" Width="400px" CssClass="inputs" MaxLength="255" />
                </p>
                <p>
                    <asp:Label ID="lblMetaKeywords" runat="server" /><br />
                    <asp:TextBox ID="tbMetaKeywords" runat="server" Width="400px" CssClass="inputs" MaxLength="1000" />
                </p>
                <p>
                    <asp:Label ID="lblMetaDescription" runat="server" /><br />
                    <asp:TextBox ID="tbMetaDescription" runat="server" Width="400px" CssClass="inputs" TextMode="MultiLine" Rows="3" MaxLength="1000" />
                </p>
            </fieldset><br />
        </asp:Panel>
        <asp:Panel ID="pnlCreatedDate" runat="server">    
            <fieldset>
                <legend><asp:Label ID="lblCreatedDateTitle" runat="server" CssClass="title" /></legend><br />
                <asp:Label ID="lblCreatedDateDescription" runat="server" />            
                <table class="normal">                    
                    
                    <tr>
                        <td class="normal">
                            <asp:Label ID="lblCreatedDate" runat="server" CssClass="title" /><br />
                            <rad:RadDateTimePicker ID="calCreateDate" Width="240px" runat="server"></rad:RadDateTimePicker>
                        </td>
                        <td class="normal">
                            &nbsp;                            
                        </td>                        
                    </tr>            
                </table> 
            </fieldset><br />
        </asp:Panel>  
        <asp:Button ID="btSave" runat="server" OnClick="btSave_Clicked" />
    </fieldset>
</div>