<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Overview.aspx.cs"
    Inherits="AimBusiness.Customers.Runo.EventsWebsite.Overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        <div style="float: left; width: 25%;">
            <p>
                Titel<br />
                <asp:TextBox ID="tbTitle" runat="server" />
            </p>
            <p>
                Undertitel<br />
                <asp:TextBox ID="tbSubTitle" runat="server" />
            </p>
            <p>
                Beskrivning<br />
                <asp:TextBox ID="tbDescription" TextMode="MultiLine" runat="server" />
            </p>
            <p>
                Bild (png eller jpg)<br />
                <asp:Image ID="imgImagePath" runat="server" Visible="false" /><br />
                <asp:FileUpload ID="fuImagePath" runat="server" />
            </p>
            <p>
                Pris<br />
                <asp:TextBox ID="tbPrice" runat="server" />
            </p>
            <p>
                Aktivitetspris<br />
                <asp:TextBox ID="tbActivityPrice" runat="server" />
            </p>
            <p>
                <asp:Button ID="btSave" runat="server" OnClick="btSave_Click" Text="Uppdatera event" />
            </p>
        </div>
        <div style="float: right; width: 70%;">
            <h3>
                Eventlist</h3>
            <p>
                <asp:LinkButton ID="lbNewEventList" runat="server" OnClick="lbNewEventList_Click">Lägg till ny eventlist</asp:LinkButton></p>
            <p>
                <asp:Repeater ID="rptEventList" runat="server">
                    <ItemTemplate>
                        <asp:TextBox ID="tbEventItem" runat="server" /><br />
                    </ItemTemplate>
                </asp:Repeater>
            </p>
            <div>
                <rad:RadGrid ID="rgEventItems" runat="server" OnNeedDataSource="rgEventItems_Source"
                    OnItemCommand="rgEventItems_Command">
                    <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" AutoGenerateColumns="false">
                        <Columns>
                            <rad:GridBoundColumn DataField="Title" HeaderText="Rubrik" />
                            <rad:GridButtonColumn CommandName="CHANGE" ButtonType="ImageButton" ImageUrl="/Images/edit.gif">
                                <HeaderStyle Width="16px" />
                            </rad:GridButtonColumn>
                            <rad:GridButtonColumn CommandName="REMOVE" ButtonType="ImageButton" ImageUrl="/Images/delete.gif"
                                ConfirmText="Säker?">
                                <HeaderStyle Width="16px" />
                            </rad:GridButtonColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <asp:LinkButton ID="lbNew" runat="server" CommandName="NEW"><img src="/Images/newobject.gif" alt="" /> Skapa ny NAMN</asp:LinkButton>
                        </CommandItemTemplate>
                    </MasterTableView>
                </rad:RadGrid>
                <asp:Panel ID="pnlEventItemForm" runat="server">
                    <div style="background-color: #EEEEEE; margin: 10px; padding: 10px;">
                        <asp:HiddenField ID="hfEventItemId" runat="server" />
                        <h2>
                            Skapa / redigera NAMN</h2>
                        <p>
                            Titel<br />
                            <asp:TextBox ID="tbEventItemTitle" runat="server" />
                        </p>
                        <p>
                            Bild (png eller jpg)<br />
                            <asp:Image ID="imgEventItemImagePath" runat="server" Visible="false" /><br />
                            <asp:FileUpload ID="fuEventItemImagePath" runat="server" />
                        </p>
                        <p>
                            Beskrivning<br />
                            <asp:TextBox ID="tbEventItemDescription" runat="server" TextMode="MultiLine" />
                        </p>
                        <p>
                            <asp:Button ID="btEventItemSave" runat="server" OnClick="btEventItemSave_Click" Text="Spara" />
                            <asp:LinkButton ID="lbEventItemBack" runat="server" OnClick="lbEventItemBack_Click" Text="Tillbaka utan att spara" />
                        </p>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
</asp:Content>
