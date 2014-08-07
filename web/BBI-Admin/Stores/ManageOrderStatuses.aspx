<%@ Page Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageOrderStatuses.aspx.cs" Inherits="Admin_ManageOrderStatuses" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Order Statuses</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManageProducts.aspx"><span>Products</span></a></li>
                        <li><a href="AddEditProduct.aspx"><span>New Product</span></a></li>
                        <li><a href="ManageDepartments.aspx"><span>Departments</span></a></li>
                        <li><a href="AddEditDepartment.aspx"><span>New Department</span></a></li>
                        <li><a href="ManageOrders.aspx"><span>Orders</span></a></li>
                        <li><a href="ManageOrderStatuses.aspx"><span>Order Statuses</span></a></li>
                        <li><a href="AddEditOrderStatus.aspx"><span>New Order Status</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="uppnlOrderStatusess">
                    <ContentTemplate>
                        <asp:ListView ID="lvOrderStatuses" runat="server">
                            <LayoutTemplate>
                                <table cellspacing="0" cellpadding="0" class="AdminList">
                                    <tr class="AdminListHeader">
                                        <td>
                                            Status
                                        </td>
                                        <td width="16">
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
                                    <tr>
                                        <td colspan="4">
                                            <div class="pager">
                                                <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvOrderStatuses">
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
                            <EmptyDataTemplate>
                                <tr>
                                    <td colspan="4">
                                        <p>
                                            Sorry there are no Order Statuses available at this time.</p>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="ListTitle">
                                        <a href='<%# String.Format("AddEditOrderStatus.aspx?OrderStatusid={0}", Eval("OrderStatusId")) %>'>
                                            <%#Eval("Title")%></a>
                                    </td>
                                    <td>
                                        <a href='<%# String.Format("manageorders.aspx?OrderStatusid={0}", Eval("OrderStatusid")) %>'>
                                            <img src="../images/ArrowR.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                    </td>

                                    <td align="center">
                                        <a href="<%# String.Format("AddEditOrderStatus.aspx?OrderStatusid={0}", Eval("OrderStatusid")) %>">
                                            <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton runat="server" ID="btnDelete" CommandArgument='<%# Eval("OrderStatusId").ToString() %>'
                                            CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                            OnClientClick="return confirm('Warning: This will delete the Product from the database.');" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
