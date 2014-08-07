<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true"
    CodeFile="ShowCategories.aspx.cs" Inherits="ShowCategories" %>

<%@ Import Namespace="System.IO" %>
<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1 class="TitleHeader">
                Article Categories</h1>
        </div>
        <div id="ContentBody">
            <asp:UpdatePanel runat="server" ID="uppnlArticles">
                <ContentTemplate>
                    <asp:ListView ID="lvCategories" runat="server" DataKeyField="CategoryID" GroupItemCount="2">
                        <LayoutTemplate>
                            <table id="tblCategories" runat="server" cellspacing="0" cellpadding="6" style="width: 100%;">
                                <tr runat="server" id="groupPlaceholder" />
                            </table>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <tr runat="server" id="CategoriesRow" style="background-color: #FFFFFF">
                                <td runat="server" id="itemPlaceholder" />
                            </tr>
                        </GroupTemplate>
                        <ItemTemplate>
                            <td style="width: 1px">
                                <asp:HyperLink ID="lnkCatImage" runat="server" NavigateUrl='<%# SEOFriendlyURL(
                                                    Path.Combine("Category", Eval("Title").ToString() ), ".aspx") %>'>
                                    <asp:Image ID="imgCategory" runat="server" BorderWidth="0px" AlternateText='<%# Eval("Title") %>'
                                        ImageUrl='<%# Eval("ImageUrl") %>' /></asp:HyperLink>
                            </td>
                            <td>
                                <div class="sectionsubtitle">
                                    <a class="articletitle" href='<%# SEOFriendlyURL(
                                    Path.Combine("Category", Eval("Title").ToString()), ".aspx") %>'>
                                        <%# HttpUtility.HtmlEncode(Eval("Title").ToString()) %></a>
                                    <br />
                                    <asp:Literal ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Literal>
                                    <a class="articletitle" href='<%# SEOFriendlyURL(
                                                    Path.Combine("Category", Eval("Title").ToString()), ".rss") %>'>
                                        <img src="Images/rss.gif" alt="Get the Rss for this category" style="border-width: 0px;" /></a>
                            </td>
                        </ItemTemplate>
                    </asp:ListView>
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
