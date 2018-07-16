<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../Site.Master"
    CodeBehind="EditWidgets.aspx.cs" Inherits="AimEdit.EditWidgets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <asp:Panel ID="pnlNoWidgetsUsed" runat="server" Visible="false">
            Er sida använder inga widgets
        </asp:Panel>
        <asp:Panel ID="pnlOverview" runat="server">
            <p>
                <asp:Label ID="lblArea" runat="server" /><br />
                <rad:RadComboBox ID="ddWidgetPlaceHolders" runat="server" AutoPostBack="true" Width="252px"
                    OnSelectedIndexChanged="dd_Changed" />
            </p>
            <div style="background-color: #EEEEEE; padding: 7px; margin-bottom: 10px; margin-top: 10px;
                width: 300px;">
                <p>
                    <asp:Label ID="lblNew" runat="server" /><br />
                    <rad:RadComboBox ID="ddSiteWidgets" runat="server" Width="252px" />
                </p>
                <p>
                    <asp:Label ID="lblName" runat="server" />
                    <asp:TextBox ID="tbName" runat="server" Width="250px" />
                </p>
                <p>
                    <asp:Button ID="btAdd" runat="server" Text="Lägg till" OnClick="btAdd_Click" />
                </p>
            </div>
            <div>
                <rad:RadGrid ID="rgObjectWidgets" runat="server" OnItemCommand="rgObjectWidgets_ItemCommand"
                    OnNeedDataSource="rgObjectWidgets_Source">
                    <MasterTableView DataKeyNames="ID" AutoGenerateColumns="false">
                        <Columns>
                            <rad:GridBoundColumn DataField="Name" HeaderText="Namn" />
                            <rad:GridBoundColumn DataField="WidgetName" HeaderText="Widget" />
                            <rad:GridCheckBoxColumn DataField="Published" HeaderText="Publicerad" />
                            <rad:GridCheckBoxColumn DataField="DefaultWidget" HeaderText="Default" />
                            <rad:GridButtonColumn ButtonType="LinkButton" CommandName="MoveUp" DataTextField="MoveUpText" />
                            <rad:GridButtonColumn ButtonType="LinkButton" CommandName="MoveDown" DataTextField="MoveDownText" />
                            <rad:GridButtonColumn ButtonType="LinkButton" CommandName="PUBLISH" DataTextField="PublishText" />
                            <rad:GridButtonColumn ButtonType="ImageButton" CommandName="Change" ImageUrl="/Images/edit.gif" />
                            <rad:GridButtonColumn ButtonType="ImageButton" CommandName="Remove" ConfirmText="Är du säker?"
                                ImageUrl="/Images/delete.gif" />
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlEditWidget" runat="server" Visible="false">
            <asp:HiddenField ID="hfEditWidgetId" runat="server" />
            <p>
                <asp:Label ID="lblEditLocalName" runat="server" /><br />
                <asp:TextBox ID="tbEditLocalName" runat="server" />
            </p>
            <p>
                <asp:CheckBox ID="cbEditDefault" runat="server" Width="250px" /><br />
                <asp:Label ID="lblEditDefaultDescription" runat="server" Width="300px" Font-Italic="true" />
            </p>
            <asp:Repeater ID="rptObjectWidgetProperties" runat="server" OnItemDataBound="rptObjectWidgetProperties_Bound">
                <ItemTemplate>
                    <div>
                        <asp:HiddenField ID="hfObjectWidgetPropertyId" runat="server" />
                        <p>
                            <asp:Label ID="lblTitle" runat="server" />
                            <br />
                            <asp:TextBox ID="tbText" runat="server" Visible="false" />
                            <rad:RadEditor ID="radRichText" runat="server" Visible="false">
                                <Tools>
                                    <rad:EditorToolGroup>
                                        <rad:EditorTool Name="PastePlainText" />
                                        <rad:EditorTool Name="ImageManager" />
                                        <rad:EditorTool Name="MediaManager" />
                                        <rad:EditorTool Name="DocumentManager" />
                                        <rad:EditorTool Name="LinkManager" />
                                        <rad:EditorTool Name="Unlink" />
                                        <rad:EditorTool Name="InsertSymbol" />
                                        <rad:EditorTool Name="ApplyClass" />
                                    </rad:EditorToolGroup>
                                </Tools>
                            </rad:RadEditor>
                            <rad:RadDatePicker ID="rdpDate" runat="server" Visible="false" />
                            <asp:RequiredFieldValidator ID="reqItem" runat="server" ErrorMessage="*" ValidationGroup="widgetform"
                                Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="regItem" ControlToValidate="tbText" runat="server"
                                ErrorMessage="*" ValidationGroup="widgetform" Display="Dynamic" ForeColor="Red"
                                Enabled="false" />
                        </p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <p>
                <asp:Button ID="btSave" runat="server" OnClick="btSave_Click" Text="Spara" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btBack" runat="server" OnClick="btBack_Click" Text="Tillbaka utan att spara" />
            </p>
        </asp:Panel>
    </div>
</asp:Content>
