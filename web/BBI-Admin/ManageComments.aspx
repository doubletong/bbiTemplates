<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageComments.aspx.cs" Inherits="Admin_ManageComments" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS.Web.UI" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Manage Comments</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="ManageArticles.aspx"><span>Manage Articles</span></a></li>
                <li><a href="AddEditArticle.aspx"><span>New Article</span></a></li>
                <li><a href="ManageCategories.aspx"><span>Manage Categories</span></a></li>
                <li><a href="AddEditCategory.aspx"><span>New Category</span></a></li>
            </ul>
        </div>
    </div>
    <div id="ContentBody">
        <asp:UpdatePanel runat="server" ID="uppnlComments">
            <ContentTemplate>
                <asp:ListView ID="lvComments" runat="server">
                    <LayoutTemplate>
                        <table cellspacing="0" cellpadding="0" class="AdminList">
                            <tr class="AdminListHeader">
                                <td>
                                    Comment
                                </td>
                                <td>
                                    Approve
                                </td>
                                <td>
                                    Approved
                                </td>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <EmptyDataTemplate>
                        <tr>
                            <td colspan="3">
                                <p>
                                    Sorry there are no Comments available at this time.</p>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="ListTitle">
                                <a href='<%# String.Format("AddEditComment.aspx?CommentId={0}", Eval("CommentId")) %>'>
                                    <%#Eval("DisplayTitle")%></a><br />
                                Added By:
                                <%#HttpUtility.HtmlEncode(Eval("AddedByEMail").ToString())%><br />
                                Added:
                                <%#Eval("AddedDate")%>
                            </td>
                            <td align="center">
                                <a href="<%# String.Format("AddEditComment.aspx?CommentId={0}", Eval("CommentId")) %>">
                                    <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                            </td>
                            <td align="center">
                                <img src="../images/<%# (bool.Parse(Eval("Approved").ToString())) ? "checkmark" : "uncheck" %>.gif" alt=""
                                    width="16" height="16" class="AdminImg" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color: #EFEFEF">
                            <td class="ListTitle">
                                <a href='<%# String.Format("AddEditComment.aspx?CommentId={0}", Eval("CommentId")) %>'>
                                    <%#Eval("DisplayTitle")%></a><br />
                                Added By:
                                <%#HttpUtility.HtmlEncode(Eval("AddedByEMail").ToString())%><br />
                                Added:
                                <%#Eval("AddedDate")%>
                            </td>
                            <td align="center">
                                <a href="<%# String.Format("AddEditComment.aspx?CommentId={0}", Eval("CommentId")) %>">
                                    <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                            </td>
                            <td align="center">
                                <img src="../images/<%# (bool.Parse(Eval("Approved").ToString())) ? "checkmark" : "uncheck" %>.gif" alt=""
                                    width="16" height="16" class="AdminImg" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <div class="pager">
                    <asp:DataPager ID="pagerBottom" runat="server" PageSize="10" PagedControlID="lvComments">
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
    </div>
</asp:Content>
