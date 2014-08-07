<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageForums.aspx.cs" Inherits="Admin_ManageForums" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Forums</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManageForums.aspx"><span>Forums</span></a></li>
                        <li><a href="AddEditForum.aspx"><span>New Forum</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="uppnlCategories">
                    <ContentTemplate>
                        <asp:ListView ID="lvForums" runat="server" DataKeyNames="ForumID">
                            <LayoutTemplate>
                                <table cellspacing="0" cellpadding="0" class="AdminList">
                                    <tr class="AdminListHeader" id="itemPlaceholder" runat="server">
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div class="pager">
                                                <asp:DataPager ID="pagerBottom" runat="server" PageSize="15">
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
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Image runat="server" ID="iForumLogo" ImageUrl='<%# Eval("ImageUrl") %>' />
                                    </td>
                                    <td>
                                        <div class="sectionsubtitle">
                                            <a href="<%# String.Format("AddEditForum.aspx?Forumid={0}", Eval("Forumid")) %>"><b>
                                                <%# Eval("Title") %></b></a>
                                            <asp:Literal runat="Server" ID="lblIsModerated" Text=" <span style='text-decoration: overline underline; font-size: smaller;'>|moderated|</span>"
                                                Visible='<%# Eval("Moderated") %>' />
                                        </div>
                                        <br />
                                        <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Description") %>' />
                                    </td>
                                    <td align="center">
                                        <a href="<%# String.Format("AddEditForum.aspx?Forumid={0}", Eval("Forumid")) %>">
                                            <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton runat="server" ID="btnDeleteForum" CommandArgument='<%# Eval("ForumID").ToString() %>'
                                            CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                            OnClientClick="return confirm('Warning: This will delete the Forum from the database.');" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <ItemSeparatorTemplate>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                            </ItemSeparatorTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>