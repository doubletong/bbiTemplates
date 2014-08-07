<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditPoll.aspx.cs" Inherits="Admin_AddEditPoll" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Polls</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManagePolls.aspx"><span>Manage Polls</span></a></li>
                        <li><a href="AddEditPoll.aspx"><span>New Poll</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="sectiontitle">
                    <h1>
                        <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit a Poll." /></h1>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="550">
                    <tr>
                        <td valign="top" >
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        Poll ID:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPollId"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date Added:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDateAdded"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Added By:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblAddedBy"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date Updated:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDateUpdated"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated By:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblUpdatedBy"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Votes:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblVotes"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Question:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblQuestion"></asp:Label><asp:TextBox runat="server"
                                            ID="txtQuestion"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Is Current:
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbIsCurrent" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:LinkButton runat="server" ID="lbtnInsertPoll" Text="Insert"></asp:LinkButton>&nbsp;&nbsp;<a
                                            href="ManagePolls.aspx">Cancel</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" runat="server" id="tOptionDetail">
                                <tr>
                                    <td>
                                        Option
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtOption"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="valRequireOption" runat="server" ControlToValidate="txtOption"
                                            Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                            ValidationGroup="Option">The Option field is required.</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:LinkButton runat="server" ID="lbInsert" Text="Insert"></asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton
                                            runat="server" ID="lbCancel" Text="Cancel"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListView runat="server" ID="lvPollOptions" DataKeyNames="OptionId">
                    <LayoutTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr class="AdminListHeader">
                                <td>
                                    Option
                                </td>
                                <td>
                                    Votes
                                </td>
                                <td>
                                    %
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
                                <%#Eval("OptionText")%>
                            </td>
                            <td>
                                <%#Eval("Votes")%>
                            </td>
                            <td>
                                <%#String.Format("{0:N1}", Eval("Percentage"))%>
                            </td>
                            <td align="center">
                                <asp:ImageButton runat="server" ID="ibtnEditOption" CommandArgument='<%# Eval("OptionID").ToString() %>'
                                    CommandName="Edit" ImageUrl="~/images/edit.gif" AlternateText="Edit" CssClass="AdminImg" />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibtnDeleteOption" CommandArgument='<%# Eval("OptionID").ToString() %>'
                                    CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                    OnClientClick="return confirm('Warning: This will delete the Poll Option from the database.');" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
    </table>
</asp:Content>
