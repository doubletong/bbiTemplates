<%@ Page Language="C#" MasterPageFile="~/LCMaster.master" AutoEventWireup="true" CodeFile="BrowseThreads.aspx.cs" Inherits="BrowseThreads" %>

<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div class="sectiontitle">
        <asp:Literal runat="server" ID="lblPageTitle" Text="Threads from forum: " />
        <asp:DropDownList ID="ddlForums" runat="server" DataTextField="Title" DataValueField="ForumID"
            onchange="javascript:document.location.href='BrowseThreads.aspx?ForumID=' + this.value;">
        </asp:DropDownList>
    </div>
    <br />
    <div class="ForumHeaderNav">
        <asp:HyperLink runat="server" ID="lnkNewThread1" NavigateUrl="AddEditPost.aspx?ForumID={0}">Create new thread</asp:HyperLink></div>
    <br />
    <asp:UpdatePanel runat="server" ID="uppnlThreads">
        <ContentTemplate>
            <div class="pager">
                <asp:DataPager ID="pagerTop" runat="server" PageSize="5" QueryStringField="pageNo" PagedControlID="lvThreads">
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
            </div><br />
            <asp:ListView runat="server" ID="lvThreads" ItemPlaceholderID="itemPlaceHolder" DataKeyNames="PostId">
                <LayoutTemplate>
                    <table cellpadding="0" cellspacing="0" border="1" width="95%">
                        <tr class="posttitle">
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Title
                            </td>
                            <td>
                                Last Post
                            </td>
                            <td>
                                Replies
                            </td>
                            <td>
                                Views
                            </td>
                            <td id="Td1" runat="server" visible="<%# isModerator %>" colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="itemPlaceHolder">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Image runat="server" ID="imgThread" ImageUrl="~/Images/Thread.gif" Visible='<%# (int)(Eval("ReplyCount")) < Settings.Forums.HotThreadPosts %>'
                                GenerateEmptyAlternateText="true" />
                            <asp:Image runat="server" ID="imgHotThread" ImageUrl="~/Images/ThreadHot.gif" Visible='<%# (int)(Eval("ReplyCount")) >= Settings.Forums.HotThreadPosts %>'
                                GenerateEmptyAlternateText="true" />
                        </td>
                        <td>
                            <a href="<%# "ShowThread.aspx?ThreadID=" + Eval("PostID") %>" title="<%# Eval("TitleBody") %>">
                                <%# Eval("Title") %></a><br />
                            <small>by
                                <asp:Label ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>'></asp:Label></small>
                        </td>
                        <td>
                            <small>
                                <asp:Label ID="lblLastPostDate" runat="server" Text='<%# Eval("LastPostDate", "{0:g}") %>'></asp:Label><br />
                                by
                                <asp:Label ID="lblLastPostBy" runat="server" Text='<%# Eval("LastPostBy") %>'></asp:Label></small>
                        </td>
                        <td class="CenterItem">
                            <%#Eval("ReplyCount")%>
                        </td>
                        <td class="CenterItem">
                            <%#Eval("ViewCount")%>
                        </td>
                        <td id="Td2" runat="server" visible="<%# isModerator %>">
                            <asp:HyperLink runat="server" ID="hlnkMoveThread">
                        <img border='0' src='Images/MoveThread.gif' alt='Move thread' /></asp:HyperLink>
                        </td>
                        <td id="Td3" runat="server" visible="<%# isModerator %>">
                            <asp:ImageButton runat="server" ID="ibtnClose" ImageUrl="~/Images/LockSmall.gif"
                                CommandName="Close" CommandArgument='<%# Eval("PostID").ToString() %>' />
                        </td>
                        <td id="Td4" runat="server" visible="<%# isModerator %>">
                            <asp:ImageButton runat="server" ID="iBtnDelete" ImageUrl="~/Images/delete.gif" CommandName="Delete" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <div class="pager">
                <asp:DataPager ID="pagerBottom" runat="server" PageSize="5" QueryStringField="pageNo" PagedControlID="lvThreads">
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
    <br />
    <div class="ForumHeaderNav">
        <asp:HyperLink runat="server" ID="lnkNewThread2" NavigateUrl="AddEditPost.aspx?ForumID={0}">Create new thread</asp:HyperLink></div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="LeftColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:NewsletterBox ID="NewsletterBox1" runat="server" />
</asp:Content>
