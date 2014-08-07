<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditRole.aspx.cs" Inherits="Admin_AddEditRole" Title="Untitled Page" %>

<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <div id="ContentTitle">
            <h1>
                User Roles Administration</h1>
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
    <div id="sectiontitle">
        <asp:Literal runat="server" ID="ltlStatus"></asp:Literal>
    </div>
    <div id="ContentBody">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View runat="server" ID="vEdit">
                <fieldset id="Fieldset2">
                    <table cellpadding="3" cellspacing="0" align="center" width="400" border="0">
                        <tr>
                            <td width="80">
                                Role Name
                            </td>
                            <td width="5">
                            </td>
                            <td>
                                <asp:TextBox ID="txtRole" runat="server" CssClass="formField"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRole"
                                    Display="Dynamic" ErrorMessage="You must supply a Role Name." ValidationGroup="RoleName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <table id="Table12" cellspacing="0" cellpadding="0" width="250" border="0">
                                    <tr>
                                        <td class="AdminDetailsAction">
                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="AdminButton" ValidationGroup="RoleName">Cancel</asp:LinkButton>
                                        </td>
                                        <td width="5">
                                            &nbsp;
                                        </td>
                                        <td class="AdminDetailsAction">
                                            <asp:LinkButton ID="btnUpdate" runat="server" CssClass="AdminButton" ValidationGroup="RoleName">Update</asp:LinkButton>
                                        </td>
                                        <td width="5">
                                            &nbsp;
                                        </td>
                                        <td class="AdminDetailsAction">
                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="AdminButton" ValidationGroup="RoleName">Delete</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <div runat="server" id="flUser">
                    <fieldset id="commentform">
                        <legend>Edit User's</legend>
                        <table>
                            <tr>
                                <td valign="top">
                                    Users<br />
                                    <asp:Button runat="server" ID="btnAddUserToRole" Text="Add User To Role" />
                                </td>
                                <td>
                                    <asp:ListView runat="server" ID="lvUsers">
                                        <LayoutTemplate>
                                            <ol>
                                                <li runat="server" id="itemPlaceholder" />
                                            </ol>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <%#Container.DataItem.ToString()%>&nbsp;<asp:ImageButton ID="btnDelete" AlternateText="Delete this User from the Role?"
                                                    ImageUrl='~/images/delete.gif' CausesValidation="False" runat="server" CommandName="DeleteUser"
                                                    OnClientClick="ConfirmMsg('User From the Role');" CommandArgument="<%#Container.DataItem.ToString()%>" /></li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </asp:View>
            <asp:View runat="server" ID="vAddUserToRole">
                <fieldset id="Fieldset1">
                    <legend>Assign New User</legend>
                    <br />
                    Role:
                    <asp:Literal runat="server" ID="ltlRole" />
                    <br />
                    User
                    <asp:TextBox ID="txtUser" runat="server" CssClass="formField"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnAddUserToRole2" Text="Add User To Role" />&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnCancelAddUserToRole" Text="Cancel" /><br />
                    <asp:AutoCompleteExtender runat="server" ID="autoComplete1" BehaviorID="AutoCompleteEx"
                        TargetControlID="txtUser" ServicePath="~/MembersService.asmx" ServiceMethod="SearchUsersByName"
                        MinimumPrefixLength="2" CompletionInterval="500" EnableCaching="true" CompletionSetCount="12" />
                </fieldset>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
