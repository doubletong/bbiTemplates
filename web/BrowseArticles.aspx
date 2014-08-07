<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true"
    CodeFile="BrowseArticles.aspx.cs" Inherits="BrowseArticles" %>

<%@ Import Namespace="System.IO" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div id="ContentMain">
        <h1>
            Beer House Articles</h1>
        <div id="ContentBody">
            <div>
                <asp:Literal ID="lblCategoryPicker" runat="server" Text="Filter by category:"></asp:Literal>
                <asp:DropDownList ID="ddlCategories" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                    DataTextField="Title" DataValueField="CategoryID">
                    <asp:ListItem Value="0">All categories</asp:ListItem>
                </asp:DropDownList>
                <asp:Literal runat="server" ID="lblSeparator">&nbsp;&nbsp;&nbsp;</asp:Literal>
                <asp:Literal runat="server" ID="lblPageSizePicker"><small><b>Articles per page:</b></small></asp:Literal>
                <asp:DropDownList ID="ddlArticlesPerPage" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:UpdatePanel runat="server" ID="uppnlArticles">
                <ContentTemplate>
                    <div id="ListBox">
                        <asp:ListView ID="lvArticles" runat="server">
                            <LayoutTemplate>
                                <div id="itemPlaceholder" runat="server" />
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <p>
                                    Sorry there are no articles available at this time.</p>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <div runat="server" id="dRow" class="articlebox">
                                    <div class="article_entry_header">
                                        <a class="articletitle" href='<%# ResolveUrl("~/" + SEOFriendlyURL(
                    Settings.Articles.URLIndicator + "/" + Eval("Title").ToString(), ".aspx")) %>'>
                                            <%# HttpUtility.HtmlEncode(Eval("Title").ToString()) %></a>
                                        <img alt="Requires login" src="~/Images/key.gif" runat="server" id="iLogin" visible='<%# ((bool)Eval("OnlyForMembers") && !Page.User.Identity.IsAuthenticated) %>' />
                                        <asp:Label ID="lblNotApproved" runat="server" SkinID="NotApproved" Text="Not Approved" Visible='<%# (bool)Eval("Approved") %>'></asp:Label></div>
                                    <div class="articleabstract">
                                        <p>
                                            <%#HttpUtility.HtmlEncode(Eval("Abstract").ToString())%>...<a class="articleMore"
                                                href='<%# SEOFriendlyURL(
                    Path.Combine(Settings.Articles.URLIndicator, Eval("Title").ToString()), ".aspx") %>'>(More)</a></p>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <div runat="server" id="dRow" class="Altarticlebox">
                                    <div class="article_entry_header">
                                        <a class="articletitle" href='<%# ResolveUrl("~/" + SEOFriendlyURL(
                    Settings.Articles.URLIndicator + "/" + Eval("Title").ToString(), ".aspx")) %>'>
                                            <%# HttpUtility.HtmlEncode(Eval("Title").ToString()) %></a>
                                        <img alt="Requires login" src="~/Images/key.gif" runat="server" id="iLogin" visible='<%# ((bool)Eval("OnlyForMembers") && !Page.User.Identity.IsAuthenticated) %>'/>
                                        <asp:Label ID="lblNotApproved" runat="server" SkinID="NotApproved" Text="Not Approved" Visible='<%# (!(bool)Eval("Approved")) %>'></asp:Label></div>
                                    <div class="articleabstract">
                                        <p>
                                            <%#HttpUtility.HtmlEncode(Eval("Abstract").ToString())%>...<a class="articleMore"
                                                href='<%# SEOFriendlyURL(
                    Path.Combine(Settings.Articles.URLIndicator, Eval("Title").ToString()), ".aspx") %>'>(More)</a></p>
                                    </div>
                                </div>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                        <div class="pager">
                            <asp:DataPager ID="pagerBottom" runat="server" PageSize="6" PagedControlID="lvArticles">
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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="RightColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>
