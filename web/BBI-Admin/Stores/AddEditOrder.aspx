<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditOrder.aspx.cs" Inherits="Admin_AddEditOrder" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Orders</h1>
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
                <div class="sectiontitle">
                    <h1>
                        <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit an Order" /></h1>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="AddEditDetails">
                    <table cellspacing="1" cellpadding="1" width="97%" align="center" border="0">
                        <tr>
                            <td>
                                Order ID
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlOrderID"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Order Date
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlAddedDate"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ordered By
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlAddedBy"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <fieldset class="OrderItems">
                                    <legend>Ordered Items</legend><small>
                                        <asp:Repeater runat="server" ID="repOrderItems">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            SKU
                                                        </td>
                                                        <td>
                                                            Title
                                                        </td>
                                                        <td>
                                                            Unit Price
                                                        </td>
                                                        <td>
                                                            QTY
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        [<%# Eval("SKU") %>]
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink runat="server" ID="lnkProduct" Text='<%# Eval("Title") %>' NavigateUrl='<%# "~/ShowProduct.aspx?productID=" + Eval("ProductID") %>' />
                                                    </td>
                                                    <td>
                                                        <%#String.Format("{0:C}", Eval("UnitPrice"))%>
                                                    </td>
                                                    <td>
                                                        (<%# Eval("Quantity") %>)
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </small>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOrderStatuses">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Shipping Method
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlShippingMethod"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sub Total
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlSubTotal"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Shipping
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlShipping"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Shipped Date
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtShippedDate" Width="70"></asp:TextBox><asp:Image
                                    runat="Server" ID="iShippedDate" ImageUrl="~/images/Calendar.png" /><br />
                                <asp:CalendarExtender ID="ceShippedDate" runat="server" TargetControlID="txtShippedDate"
                                    PopupButtonID="iShippedDate">
                                </asp:CalendarExtender>
                                <asp:MaskedEditExtender ID="meeShippedDate" runat="server" TargetControlID="txtShippedDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                                <asp:MaskedEditValidator ID="mevShippedDate" runat="server" ControlExtender="meeShippedDate"
                                    ControlToValidate="txtShippedDate" IsValidEmpty="True" EmptyValueMessage="Date is required"
                                    InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic"
                                    TooltipMessage="Input a Date" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Email
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlCustomerEmail"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Phone
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlCustomerPhone"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Fax
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtCustomerFax"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlShippingName"></asp:Literal><br />
                                <asp:Literal runat="server" ID="ltlShippingStreet"></asp:Literal><br />
                                <asp:Literal runat="server" ID="ltlCityStateZip"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Transaction ID
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="formField" ID="txtTransactionID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tracking ID
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="formField" ID="txtTrackingID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Updated Date
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlUpdatedDate"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Updated By
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlUpdatedBy"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlActive"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                    <tr>
                                        <td align="center" style="height: 16px">
                                            <asp:Button ID="cmdCancel" runat="server" BorderStyle="None" Text="Cancel" />
                                        </td>
                                        <td align="center" style="height: 16px">
                                            <asp:Button ID="cmdDelete" Visible="False" runat="server" BorderStyle="None" CausesValidation="False"
                                                Text="Delete" OnClientClick="return confirm('Warning: This will delete the Orders from the database.');" />
                                        </td>
                                        <td align="center" style="height: 16px">
                                            <asp:Button ID="cmdUpdate" runat="server" BorderStyle="None" Text="Update" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
