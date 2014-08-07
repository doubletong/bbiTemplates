<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="DepartmentDetail.aspx.cs" Inherits="Admin_DepartmentDetail" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Departments</h1>
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
                        <li><a href="ManageOrderStatuses.aspx"><span>OrderStatuses</span></a></li>
                        <li><a href="AddEditOrderStatus.aspx"><span>New OrderStatus</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal runat="server" ID="ltlStatus"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <div runat="server" id="dEditForm" style="width: 90%;">
                    <div class="sectiontitle">
                        <h1>
                            <asp:Literal runat="server" ID="Literal1" Text="Create or Edit an Departments" /></h1>
                    </div>
                    <table cellspacing="1" cellpadding="1" width="97%" align="center" border="0">
                        <tr>
                            <td>
                                Department ID
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlDepartmentID"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Title
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="formField" ID="txtTitle"></asp:TextBox><sup><font
                                    color="red">*</font></sup>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTitle"
                                    ErrorMessage="You must enter a department title"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Importance
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="formField" ID="txtImportance"></asp:TextBox>
                                <asp:NumericUpDownExtender ID="txtImportance_NumericUpDownExtender" runat="server"
                                    Enabled="True" Maximum="100" Minimum="0" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                                    ServiceUpMethod="" Step="5" Tag="" TargetButtonDownID="" TargetButtonUpID=""
                                    TargetControlID="txtImportance" Width="0">
                                </asp:NumericUpDownExtender>
                                <sup><font color="red">*</font></sup>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Description
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox runat="server" CssClass="formField" ID="txtDescription" Rows="5" TextMode="MultiLine"
                                    MaxLength="4000" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Image Url
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="formField" ID="txtImageUrl"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Added Date
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlAddedDate"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Added By
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlAddedBy"></asp:Literal>
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
                            <td align="center" colspan="2">
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
                                                Text="Delete" OnClientClick="return confirm('Warning: This will delete the Departments from the database.');" />
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

