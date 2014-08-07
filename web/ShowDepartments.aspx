<%@ Page Language="C#" MasterPageFile="~/LCMaster.master" Theme="DarkBeer" AutoEventWireup="true" CodeFile="ShowDepartments.aspx.cs" Inherits="ShowDepartments" %>


<%@ Import Namespace="System.IO" %>
<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <div id="ContentTitle">
            <h1>
                Store Departments</h1>
        </div>
        <div id="ContentBody">
            <p>
                Click on the title of the department for which you want to browse the products:</p>
            <asp:ListView ID="lvDepartments" runat="server" DataKeyField="DepartmentID" GroupItemCount="2">
                <LayoutTemplate>
                    <table id="tblDepartments" runat="server" cellspacing="0" cellpadding="6" style="width: 100%;"
                        border="0">
                        <tr runat="server" id="groupPlaceholder" />
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr runat="server" id="DepartmentsRow">
                        <td runat="server" id="itemPlaceholder" />
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td width="50%">
                        <div>
                            <a href='/Products/Department_<%# Eval("DepartmentID") %>/'>
                                <asp:Image runat="server" ID="imgDepartment" BorderWidth="0px" 
                                AlternateText='<%# Eval("Title") %>'
                                    ImageUrl='<%# Eval("ImageUrl") %>' ImageAlign="Middle" />
                            </a>
                        </div>
                        <div>
                            <div class="sectionsubtitle">
                                <asp:HyperLink runat="server" ID="lnkDepRss" 
                                NavigateUrl='<%# ResolveUrl("~/Products.Rss?DeptartmentID=" + Eval("departmentID")) %>'>
                     <img style="border-width: 0px;" src="Images/rss.gif" alt="Get the RSS for this Department" /></asp:HyperLink>
                                <a href='/Products/Department_<%# Eval("DepartmentID") %>/'>
                                    <%# Eval("Title") %></a>
                            </div>
                            <br />
                            <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Description") %>' />
                        </div>
                    </td>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="LeftColumn" runat="Server">
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>
