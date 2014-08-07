<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManagePolls.aspx.cs" Inherits="Admin_ManagePolls" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Polls</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManagePolls.aspx"><span>Manage Polls</span></a></li>
                        <li><a href="AddEditPoll.aspx"><span>New Poll</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="uppnlCategories">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="lvPolls" DataKeyNames="PollId">
                            <LayoutTemplate>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr class="AdminListHeader">
                                        <td>
                                            ID
                                        </td>
                                        <td>
                                            Poll
                                        </td>
                                        <td>
                                            Votes
                                        </td>
                                        <td>
                                            Is Current
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Eval("PollId")%>
                                    </td>
                                    <td>
                                        <%#Eval("QuestionText")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Votes")%>
                                    </td>
                                    <td align="center">
                                        <asp:Image ID="imgIsCurrent" runat="server" ImageUrl="~/Images/OK.gif" Visible='<%# Eval("IsCurrent") %>' />
                                    </td>
                                    <td align="center">
                                        <a href="<%# String.Format("AddEditPoll.aspx?PollID={0}", Eval("PollId")) %>">
                                            <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton runat="server" ID="ibtnArchive" CommandArgument='<%# Eval("PollID").ToString() %>'
                                            CommandName="Archive" ImageUrl="~/images/folder.gif" AlternateText="Archive"
                                            CssClass="AdminImg" />
                                    </td>
                                    <td>
                                        <asp:ImageButton runat="server" ID="btnDeleteOption" CommandArgument='<%# Eval("PollID").ToString() %>'
                                            CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                            OnClientClick="return confirm('Warning: This will delete the Event from the database.');" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                        <div class="pager">
                            <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvPolls">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

