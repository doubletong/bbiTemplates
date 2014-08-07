<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageCategories.aspx.cs" Inherits="Admin_ManageCategories" Title="Untitled Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Manage Categories</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="AddEditCategory.aspx"><span>New Category</span></a></li>
                <li><a href="ManageArticles.aspx"><span>Manage Articles</span></a></li>
                <li><a href="AddEditArticle.aspx"><span>New Article</span></a></li>
            </ul>
        </div>
    </div>
    <div id="ContentBody">
        <asp:UpdatePanel runat="server" ID="uppnlCategories">
            <ContentTemplate>
                <div id="ListBox">
                    <asp:ListView ID="lvCategories" runat="server" DataKeyNames="CategoryId">
                        <LayoutTemplate>
                            <table cellspacing="0" cellpadding="0" class="AdminList">
                                <thead>
                                    <tr class="AdminListHeader">
                                        <td>
                                            Title
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
                                        <td colspan="3">
                                            <div class="pager">
                                                <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvCategories">
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
                                <td colspan="3">
                                    <p>
                                        Sorry there are no Categories available at this time.</p>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="ListTitle">
                                    <a href='<%# String.Format("AddEditCategory.aspx?categoryid={0}", Eval("CategoryId")) %>'>
                                        <%# Eval("Title") %></a>
                                </td>
                                <td align="center">
                                    <a href="<%# String.Format("AddEditCategory.aspx?categoryid={0}", Eval("categoryid")) %>">
                                        <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                </td>
                                <td align="center">
                                    <asp:ImageButton runat="server" ID="btnDeleteCategory" CommandArgument='<%# Eval("CategoryID").ToString() %>'
                                        CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                        OnClientClick="return confirm('Warning: This will delete the Category from the database.');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
