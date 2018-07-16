<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" MasterPageFile="../../Site.Master" Inherits="AimNews.Statistics" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">  
    <asp:Panel ID="pnlContent" runat="server">
    <div style="width:840px; text-align:right;">
     <asp:ImageButton ID="ibBack" runat="server" ImageUrl="/images/arrow_left_grey.gif"
                            OnClick="ibBack_OnClick" />
                            </div>
        <p><asp:Label ID="lblMailTitle" runat="server" CssClass="title" /> &nbsp;&nbsp;<asp:DropDownList ID="ddMailHistory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddMailHistory_Change" /></p>
        <fieldset style="width:820px;">
            <legend><asp:Label ID="lblSummaryHeader" runat="server" CssClass="title" /></legend>
            <table cellpadding="4" cellspacing="0">
                <tr class="lightgray_noalign">
                    <td><asp:Label ID="lblSendTime" runat="server" /></td>
                    <td><asp:Label ID="lblSendTime_value" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblNbrRecipients" runat="server" /></td>
                    <td><asp:Label ID="lblNbrRecipients_value" runat="server" /></td>
                </tr>                      
                <tr class="lightgray_noalign">
                    <td><asp:Label ID="lblNbrDelivered" runat="server" /></td>
                    <td><asp:Label ID="lblNbrDelivered_value" runat="server" /></td>
                </tr>                
                <tr>
                    <td><asp:Label ID="lblNbrOpens" runat="server" /></td>
                    <td><asp:Label ID="lblNbrOpens_value" runat="server" /></td>
                </tr>
                <tr class="lightgray_noalign">
                    <td><asp:Label ID="lblPercentOpened" runat="server" /></td>
                    <td><asp:Label ID="lblPercentOpened_value" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblNbrUniqueClickers" runat="server" /></td>
                    <td><asp:Label ID="lblNbrUniqueClickers_value" runat="server" /></td>
                </tr>
                <tr class="lightgray_noalign">
                    <td><asp:Label ID="lblPercentClickers" runat="server" /></td>
                    <td><asp:Label ID="lblPercentClickers_value" runat="server" />&nbsp;&nbsp;</td>
                </tr>
                <tr >
                    <td><asp:Label ID="lblNbrIsError" runat="server" /></td>
                    <td><asp:Label ID="lblNbrIsError_value" runat="server" /></td>
                </tr>                                                
            </table>
        </fieldset>
        <br /><br />
        <asp:Panel ID="pnlDetails" runat="server">
            <fieldset style="width:820px;">
                <legend><asp:Label ID="lblLinkSummaryHeader" runat="server" CssClass="title" /></legend>                
                <asp:Repeater ID="repLinks" runat="server" OnItemDataBound="repLinks_OnItemDataBound" >
                    <HeaderTemplate>
                        <table  cellpadding="4" cellspacing="0">
                        <tr class="title">
                            <td><asp:Label ID="lblRepLinksHeader_links" runat="server" /></td>
                            <td><asp:Label ID="lblRepLinksHeader_NbrUniqueClick" runat="server" /></td>
                            <td><asp:Label ID="lblRepLinksHeader_PercentTot" runat="server" /></td>
                            <td><asp:Label ID="lblRepLinksHeader_totClicks" runat="server" /></td>
                            <td><asp:Label ID="lblRepLinksHeader_avgClicks" runat="server" /></td>
                            <td>&nbsp;</td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><asp:HyperLink ID="hpRepLinks_links" runat="server" Target="_blank" /></td>
                            <td class="text_center"><asp:Label ID="lblRepLinks_NbrUniqueClick" runat="server" /></td>
                            <td class="text_center"><asp:Label ID="lblRepLinks_PercentTot" runat="server" /></td>
                            <td class="text_center"><asp:Label ID="lblRepLinks_totClicks" runat="server" /></td>
                            <td class="text_center"><asp:Label ID="lblRepLinks_avgClicks" runat="server" /></td>
                            <td><asp:ImageButton ID="ibExport" runat="server" OnCommand="ibExport_Command" CommandName="export" ImageUrl="/Images/excel.gif" /></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="lightgray_noalign">
                            <td><asp:HyperLink ID="hpRepLinks_links" runat="server" Target="_blank" /></td>
                            <td class="text_center"><asp:Label ID="lblRepLinks_NbrUniqueClick" runat="server" /></td>
                            <td class="text_center"><asp:Label ID="lblRepLinks_PercentTot" runat="server" /></td>
                            <td class="text_center"><asp:Label ID="lblRepLinks_totClicks" runat="server" /></td>
                            <td class="text_center"><asp:Label ID="lblRepLinks_avgClicks" runat="server" /></td>
                            <td><asp:ImageButton ID="ibExport" runat="server" OnCommand="ibExport_Command" CommandName="export" ImageUrl="/Images/excel.gif" /></td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>                
                <p><asp:Label ID="lblLinkSummaryError" runat="server" Visible="false" /></p>
            </fieldset>
            <br /><br />
            <fieldset style="width:820px;">
                <legend><asp:Label ID="lblDaySummaryHeader" runat="server" CssClass="title" /></legend>
                <p><asp:Label ID="lblStatisticByDayHeader" runat="server" /></p>
                <p><asp:LinkButton ID="lbDetailedStat" runat="server" OnClick="lbDetailedStat_Click" /></p>
                <asp:Table ID="tblStatisticByDay" runat="server" CellPadding="4" CellSpacing="0" />
                <br /><br />   
                <rad:RadChart ID="rcDayAfterSending" runat="server" Width="800px" Skin="WebBlue">
                    <Legend Visible="false" />
                    <PlotArea>                        
                        <XAxis LayoutMode="Normal" AutoScale="false">
                            <Appearance ValueFormat="General" MajorGridLines-Visible="false">
                                <LabelAppearance RotationAngle="45" Position-AlignedPosition="Top">
                                </LabelAppearance>
                            </Appearance>
                        </XAxis>
                        <YAxis IsZeroBased="true" AutoScale="false">
                            <Appearance ValueFormat="Percent"  />
                        </YAxis>
                    </PlotArea>
                </rad:RadChart>
                <br /><br />
                <p><asp:Label ID="lblWeekBydayDescription" runat="server" /></p>
                <rad:RadChart ID="rcWeekByDay" runat="server" Width="800px" Skin="WebBlue">      
                    <PlotArea>
                        <XAxis LayoutMode="Normal" AutoScale="false">
                            <Appearance ValueFormat="General" MajorGridLines-Visible="false">
                                <LabelAppearance RotationAngle="45" Position-AlignedPosition="Top" />                             
                            </Appearance>                            
                        </XAxis>
                        <YAxis IsZeroBased="true" AutoScale="false">
                            <Appearance ValueFormat="Percent"  />
                        </YAxis>
                    </PlotArea>
                </rad:RadChart>             
                
                <p><asp:Label ID="lblStatisticByWeekHeader" runat="server" /></p>
                <asp:Table ID="tblStatisticByWeek" runat="server" CellPadding="4" CellSpacing="0" />                
            </fieldset></asp:Panel>
            <br /><br />
            <rad:RadGrid ID="rgDetailOpenStatistics" runat="server" Visible="false" />                
    </asp:Panel>
    <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false" />
</asp:Content>