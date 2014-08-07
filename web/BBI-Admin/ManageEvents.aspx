<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageEvents.aspx.cs" Inherits="Admin_ManageEvents" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Manage Events</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="ManageEvents.aspx"><span>Events</span></a></li>
                <li><a href="AddEditEvent.aspx"><span>New Event</span></a></li>
                <li><a href="ManageEventRSVPs.aspx"><span>Event RSVPs</span></a></li>
                <li><a href="AddEditEventRSVP.aspx"><span>New Event RSVP</span></a></li>
            </ul>
        </div>
    </div>
    <div id="ContentBody">
        <asp:UpdatePanel runat="server" ID="uppnlEvents">
            <ContentTemplate>
                <asp:ListView ID="lvEvents" runat="server" DataKeyNames="EventId">
                    <LayoutTemplate>
                        <table cellspacing="0" cellpadding="0" class="AdminList" width="95%">
                            <thead>
                                <tr class="AdminListHeader">
                                    <td>
                                        Event
                                    </td>
                                    <td>
                                        RSVP
                                    </td>
                                    <td>
                                        Edit
                                    </td>
                                    <td>
                                        Delete
                                    </td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="4">
                                        <div class="pager">
                                            <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvEvents">
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
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </LayoutTemplate>
                    <EmptyDataTemplate>
                        <tr>
                            <td colspan="4">
                                <p>
                                    Sorry there are no Events available at this time.</p>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="ListTitle">
                                <a href='<%# String.Format("AddEditEvent.aspx?Eventid={0}", Eval("EventId")) %>'>
                                    <%#Eval("EventTitle")%></a><br />
                                <%#DateTime.Parse(Eval("EventDate").ToString()).ToShortDateString()%>
                            </td>
                            <td align="center">
                                <a href="<%# String.Format("manageeventrsvps.aspx?Eventid={0}", Eval("Eventid")) %>">
                                    <img src="../images/checkmark.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                            </td>
                            <td align="center">
                                <a href="<%# String.Format("AddEditEvent.aspx?Eventid={0}", Eval("Eventid")) %>">
                                    <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                            </td>
                            <td align="center">
                                <asp:ImageButton runat="server" ID="btnDeleteEvents" CommandArgument='<%# Eval("EventID").ToString() %>'
                                    CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                    OnClientClick="return confirm('Warning: This will delete the Event from the database.');" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
