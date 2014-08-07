<%@ Page Language="C#" MasterPageFile="~/LCMaster.master" AutoEventWireup="true" CodeFile="BrowseEvents.aspx.cs" Inherits="BrowseEvents" %>

<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <div id="ContentTitle">
            <h1>
                Calendar of Events</h1>
            <asp:HyperLink runat="server" ID="hlnkEdit" ImageUrl="~/Images/Edit.gif" Text="Edit"
                NavigateUrl="~/Admin/ManageEvents.aspx" />
        </div>
        <div id="ContentBody">
            <asp:UpdatePanel runat="server" ID="pnlCalendar">
                <ContentTemplate>
                    <table border="0" cellpadding="20" cellspacing="0" style="width: 100%; height: 100%">
                        <tr>
                            <td align="left" style="width: 200px" valign="top">
                                <br />
                                <div style="text-align: left">
                                    <asp:Calendar ID="objCalendar" runat="server" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px">
                                        <TodayDayStyle CssClass="TodayDayStyle"></TodayDayStyle>
                                        <DayStyle CssClass="DayStyle"></DayStyle>
                                        <DayHeaderStyle CssClass="DayHeaderStyle"></DayHeaderStyle>
                                        <TitleStyle CssClass="TitleStyle"></TitleStyle>
                                        <OtherMonthDayStyle CssClass="OtherMonthDayStyle"></OtherMonthDayStyle>
                                    </asp:Calendar>
                                </div>
                            </td>
                            <td align="left" valign="top">
                                <br />
                                <asp:ListView ID="lvEvents" runat="server" ItemPlaceholderID="itemPlaceHolder">
                                    <LayoutTemplate>
                                        <div runat="server" id="itemPlaceHolder" />
                                        <div>
                                            <asp:DataPager ID="pagerBottom" runat="server" PageSize="6" PagedControlID="lvEvents">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                                        NextPreviousButtonCssClass="command" />
                                                    <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <a href="ShowEvent.aspx?eventid=<%# Eval("EventId") %>">
                                                    <h2 class="EventTitle">
                                                        <%# Eval("EventTitle") %></h2>
                                                </a>
                                                <%#DateTime.Parse( Eval("EventDate").ToString() ).ToShortDateString()%>&nbsp;-&nbsp;<%# Eval("EventTime") %>
                                                <br />
                                                Location -
                                                <%# Eval("EventLocation") %>
                                                <div id="dEventLink">
                                                    <a href="ShowEvent.aspx?eventid=<%# Eval("EventId") %>">More Information</a>
                                                </div>
                                            </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="LeftColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:NewsletterBox ID="NewsletterBox1" runat="server" />
</asp:Content>
