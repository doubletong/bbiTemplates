<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingCartBox.ascx.cs" Inherits="Controls_ShoppingCartBox" %>
<%@ Import Namespace="BBICMS.UI"%>
<div class="SideBarbox">
    <div class="SideBarTitle">
        <asp:Literal runat="server" ID="lblTitle" meta:resourcekey="lblTitleResource1" Text="Shopping Cart"></asp:Literal>
    </div>
    <div class="SideBarContent">
        <asp:ListView runat="server" ID="lvOrderItems" ItemPlaceholderID="itemPlaceHolder">
            <LayoutTemplate>
                <div runat="server" id="itemPlaceHolder">
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <div id="ShoppingCartItem">
                    <asp:Image runat="Server" ID="imgProduct" ImageUrl="~/Images/ArrowR3.gif" GenerateEmptyAlternateText="true" />
                    <%# Eval("Title") %>
                    -
                    <%#BBICMS.Helpers.FormatPrice(Eval("UnitPrice"))%>
                    &nbsp;&nbsp;<small>(<%# Eval("Quantity") %>)
                        <br />
                    </small>
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div>
                    <asp:Literal runat="server" ID="lblCartIsEmpty" Text="Your cart is currently empty."
                        meta:resourcekey="lblCartIsEmptyResource1" /></div>
            </EmptyDataTemplate>
        </asp:ListView>
        <br />
        <b>
            <asp:Literal runat="server" ID="lblSubtotalHeader" Text="Subtotal = " meta:resourcekey="lblSubtotalHeaderResource1" /><asp:Literal
                runat="server" ID="lblSubtotal" /></b>
        <br />
        <asp:Panel runat="server" ID="panLinkShoppingCart" meta:resourcekey="panLinkShoppingCartResource1">
            <asp:HyperLink runat="server" ID="lnkShoppingCart" NavigateUrl="~/ShoppingCart.aspx"
                meta:resourcekey="lnkShoppingCartResource1">Detailed Shopping Cart</asp:HyperLink><br />
        </asp:Panel>
        <asp:HyperLink runat="server" ID="lnkOrderHistory" NavigateUrl="~/OrderHistory.aspx"
            meta:resourcekey="lnkOrderHistoryResource1">Order History</asp:HyperLink>
    </div>
</div>
