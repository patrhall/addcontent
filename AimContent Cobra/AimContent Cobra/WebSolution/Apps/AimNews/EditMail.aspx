<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="True"
    CodeBehind="EditMail.aspx.cs" Inherits="AimNews.EditMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <div style="padding-top: 10px; padding-bottom: 10px;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:ImageButton ID="ibBack" runat="server" ImageUrl="/images/arrow_left_grey.gif"
                            OnClick="ibBack_OnClick" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 840px;">
                        <asp:Label ID="lblHeader" runat="server" CssClass="title"></asp:Label><br />
                        <asp:TextBox ID="tbHeader" runat="server" Width="350px"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvHeader" ValidationGroup="vgNews" ControlToValidate="tbHeader"
                            runat="server"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFromMail" runat="server" CssClass="title"></asp:Label><br />
                        <asp:TextBox ID="tbFromMail" runat="server" Width="350px"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvFromMail" ValidationGroup="vgNews" ControlToValidate="tbFromMail"
                            runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revFromMail"
                                runat="server" Display="Dynamic" ValidationGroup="vgNews" ControlToValidate="tbFromMail"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFromName" runat="server" CssClass="title"></asp:Label><br />
                        <asp:TextBox ID="tbFromName" runat="server" Width="350px"></asp:TextBox><br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top:20px;">
                        <asp:Label ID="lblUnregisterHeader" runat="server" CssClass="title"></asp:Label><br />
                        <asp:Label ID="lblUnregisterText" runat="server"></asp:Label><br />
                        <asp:TextBox ID="tbUnregister" runat="server" TextMode="MultiLine" Rows="3" Width="350px"></asp:TextBox><br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUnregisterChoice" runat="server"></asp:Label>&nbsp;<asp:CheckBox
                            ID="cbUnregister" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="padding-top: 20px;">
                    <td>
                        <asp:Label ID="lblWebLinkHeader" runat="server" CssClass="title"></asp:Label><br />
                        <asp:Label ID="lblWebLink" runat="server"></asp:Label>&nbsp;<asp:CheckBox ID="cbWebLink"
                            runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top:10px;">
            <asp:Label ID="lblEditor" runat="server" CssClass="title"></asp:Label><br />
            <rad:radeditor language="en-US" id="editor1"  toolsfile="/App_Data/RadControls/Editor/ToolsFile_Aimnews.xml"
                width="890px" height="480px" runat="server" onsubmitclicked="editor1_SubmitClicked" />
        </div>
        <div style="padding-top: 20px;">
            <asp:Label ID="lblTestmail" runat="server" CssClass="title"></asp:Label><br />
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblTestmailAdress" runat="server" CssClass="normal"></asp:Label>
                    </td>
                    <td style="padding-left: 10px;">
                        <asp:TextBox ID="tbTestmailAdress" runat="server" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTestMail" ValidationGroup="vgTest" ControlToValidate="tbTestmailAdress"
                            runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revTestMail"
                                runat="server" Display="Dynamic" ValidationGroup="vgTest" ControlToValidate="tbTestmailAdress"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top:10px;">
            <div><asp:Label ID="lblErrorTestMail" runat="server" ForeColor="Red" Visible="false" EnableViewState="false" /></div>
            <asp:Button ID="btnSendTest" runat="server" ValidationGroup="vgTest" Visible="true"
                OnClick="btnSendTest_Click" style="margin-top:10px;" />
        </div>
        <div style="padding-top: 20px;">
            <asp:Label ID="lblGroup" runat="server" CssClass="title"></asp:Label><br />
            <rad:radgrid id="rgGroups" runat="server" width="500px" onneeddatasource="rgGroups_NeedDataSource"
                onitemcreated="rgGroups_ItemCreated" allowsorting="true"
                allowpaging="false" gridlines="None" pagesize="100" autogeneratecolumns="false">
        <MasterTableView DataKeyNames="ID">                    
            <Columns>
                <rad:GridTemplateColumn>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem,"GroupName") %> 
                    </ItemTemplate>
                   <HeaderStyle Width="80%" />
                </rad:GridTemplateColumn>
            <rad:GridTemplateColumn HeaderText="Välj mottagare">
                <ItemTemplate>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 150px;" align="center">
                                <asp:CheckBox ID="cbChoice" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </rad:GridTemplateColumn>   
          </Columns>
        </MasterTableView>        
    </rad:radgrid>
        </div>
        <div style="width: 500px; padding-top: 10px;">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 90%">
                        <asp:Button ID="btnSend" runat="server" ValidationGroup="vgNews" Visible="true" OnPreRender="btnSend_PreRender"
                            OnClick="btnSend_Click" />
                            
                    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="vgNews" Visible="true" OnPreRender="btnSendTest_PreRender" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="padding-top: 7px;">
        <asp:Label ID="lblStatus" runat="server" Visible="false" CssClass="confirm"></asp:Label>
    </div>
</asp:Content>
