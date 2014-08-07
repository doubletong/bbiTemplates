<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageUnapprovedPosts.aspx.cs" Inherits="Admin_ManageUnapprovedPosts" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Unapproved Post</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManageForums.aspx"><span>Manage Forums</span></a></li>
                        <li><a href="AddEditForum.aspx"><span>New Forum</span></a></li>
                        <li><a href="ManageUnapprovedPosts.aspx"><span>Manage Unapproved Post</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="uppnlUnApproved">
                    <ContentTemplate>
                        <asp:ListView ID="lvPosts" runat="server" DataKeyNames="PostID">
                            <LayoutTemplate>
                                <table cellspacing="0" cellpadding="0" class="AdminList">
                                    <tr class="AdminListHeader">
                                        <td>
                                            Title
                                        </td>
                                        <td>
                                            Forum
                                        </td>
                                        <td>
                                            Added Date
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <span runat="server" id="dTitle">
                                            <%# Eval("Title") %></span>
                                        <br />
                                        <img src="~/images/GoDown.gif" runat="server" id="iGoDown" /><small>by
                                            <asp:Label ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>'></asp:Label><br />
                                            <div id="body<%# Eval("PostId").ToString() %>" class="MoveThreadBody">
                                                <%# Eval("Body") %></div>
                                        </small>
                                    </td>
                                    <td>
                                        <%# Eval("ForumTitle") %>
                                    </td>
                                    <td>
                                        <%#String.Format("{0:d}<br/>{0:t}", Eval("AddedDate"))%>
                                    </td>
                                    <td>
                                        <asp:ImageButton runat="server" ID="btnApprove" CommandArgument='<%# Eval("PostId") %>'
                                            CommandName="Approve" ImageUrl="~/images/ok.gif" AlternateText="Delete" CssClass="AdminImg" />
                                    </td>
                                    <td>
                                        <asp:ImageButton runat="server" ID="btnDelete" CommandArgument='<%# Eval("PostId").ToString() %>'
                                            CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                            OnClientClick="return confirm('Warning: This will delete the Post from the database.');" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <b>No posts to show</b></EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

