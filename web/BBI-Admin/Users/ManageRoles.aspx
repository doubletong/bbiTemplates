<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageRoles.aspx.cs" Inherits="Admin_ManageRoles" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS.Web.UI" TagPrefix="asp" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <div id="ContentTitle">
            <h1>
                Role Management</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="ManageUsers.aspx"><span>Users</span></a></li>
                <li><a href="AddEditUser.aspx"><span>New User</span></a></li>
                <li><a href="ManageRoles.aspx"><span>Roles</span></a></li>
                <li><a href="AddEditRole.aspx"><span>New Role</span></a></li>
            </ul>
        </div>
    </div>
    <div id="ContentBody">
        <table style="width: 95%">
            <tr>
                <td align="right">
                    <asp:Button ID="btnNewRole" runat="server" Text="New Role" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel runat="server" ID="uppnlUsers">
                        <ContentTemplate>
                            <asp:ListView ID="lvRoles" runat="server">
                                <LayoutTemplate>
                                    <table cellspacing="0" cellpadding="0" class="AdminList">
                                        <tr class="AdminListHeader">
                                            <td>
                                                Role
                                            </td>
                                            <td>
                                                Edit
                                            </td>
                                            <td>
                                                Delete
                                            </td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                    <div class="pager">
                                        <asp:DataPager ID="pagerBottom" runat="server" PageSize="10" PagedControlID="lvRoles"
                                            QueryStringField="pageNo">
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
                                <EmptyDataTemplate>
                                    <tr>
                                        <td colspan="3">
                                            <p>
                                                Sorry there are no Roles available at this time.</p>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="ListTitle">
                                            <a href="<%# String.Format("AddEditRole.aspx?RoleName={0}", Container.DataItem.ToString()) %>">
                                                <%# Container.DataItem.ToString() %></a>
                                        </td>
                                        <td>
                                            <a href="<%# String.Format("AddEditRole.aspx?RoleName={0}", Container.DataItem.ToString()) %>">
                                                <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                        </td>
                                        <td>
                                            <asp:ImageButton runat="server" ID="btnDeleteEvents" CommandName="DeleteRole" ImageUrl="~/images/delete.gif"
                                                AlternateText="Delete" CssClass="AdminImg" OnClientClick="return confirm('Warning: This will delete the Role from the database.');" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
