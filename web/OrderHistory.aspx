<%@ Page Language="C#" MasterPageFile="~/LCMaster.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs" Inherits="OrderHistory" %>
<%@ Import Namespace="BBICMS.Store"%>
<%@ Import Namespace="BBICMS.BLL.Store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CenterColumn" Runat="Server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <div id="ContentTitle">
            <h1>
                Order History</h1>
        </div>
        <div id="ContentBody">
            <p>
                Follows the list of orders you've submitted in the past, with their current status:
            </p>
            <asp:UpdatePanel runat="server" ID="upOrders">
                <ContentTemplate>
                    <asp:ListView runat="server" ID="lvOrders" DataKeyNames="OrderId"
                     OnItemDataBound="lvOrders_ItemDataBound" OnPagePropertiesChanged="lvOrders_PagePropertiesChanged" >
                        <LayoutTemplate>
                            <div runat="server" id="itemPlaceHolder">
                            </div>
                            <div class="pager">
                                <asp:DataPager ID="pagerBottom" runat="server" PageSize="5" PagedControlID="lvOrders">
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
                            <div class="sectionsubtitle">
                                Order #<%#Eval("OrderID")%>
                                -
                                <%# Eval("AddedDate", "{0:g}") %></div>
                            <br />
                            <img src="Images/ArrowR4.gif" border="0" alt="" />
                            <u>Total</u> =
                            <%# FormatPrice(double.Parse( Eval("SubTotal").ToString()) + double.Parse(Eval("Shipping").ToString())) %><br />
                            <img src="Images/ArrowR4.gif" border="0" alt="" />
                            <u>Status</u> =
                            <%# Eval("StatusTitle") %>
                            &nbsp;&nbsp;&nbsp;
                            <asp:HyperLink runat="server" ID="lnkPay" Font-Bold="true" Text="Pay Now" NavigateUrl='<%# BBICMS.StoreHelper.GetPayPalPaymentUrl((Order)Container.DataItem) %>'
                                Visible='<%# (int.Parse( Eval("StatusID").ToString()) == 1) %>' />
                            <br />
                            <br />
                            <small><b>Details</b><br />
                                <small>
                                    <asp:Repeater runat="server" ID="repOrderItems">
                                        <ItemTemplate>
                                            <img src="<%= ResolveUrl("~/Images/ArrowR3.gif") %>" border="0" alt="" />
                                            [<%= Eval("SKU") %>]
                                            <asp:HyperLink runat="server" ID="lnkProduct" Text='<%# Eval("Title") %>' NavigateUrl='<%# "~/ShowProduct.aspx?productID=" + Eval("ProductID") %>' />
                                            - (<%# Eval("Quantity") %>)
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </small>
                                <br />
                                Subtotal =
                                <%# FormatPrice(Eval("SubTotal")) %><br />
                                Shipping Method =
                                <%# Eval("ShippingMethod") %>
                                (<%# FormatPrice(Eval("Shipping")) %>) </small>
                        </ItemTemplate>
                        <ItemSeparatorTemplate>
                            <hr style="width: 99%;" />
                        </ItemSeparatorTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftColumn" Runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>

