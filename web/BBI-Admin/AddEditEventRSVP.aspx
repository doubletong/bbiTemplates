<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" 
CodeFile="AddEditEventRSVP.aspx.cs" Inherits="Admin_AddEditEventRSVP" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <div id="ContentTitle">
            <h1>
                Add Edit Event RSVP</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="ManageEventRSVPs.aspx"><span>Event RSVPs</span></a></li>
                <li><a href="AddEditEventRSVP.aspx"><span>New Event RSVP</span></a></li>
                <li><a href="ManageEvents.aspx"><span>Events</span></a></li>
                <li><a href="AddEditEvent.aspx"><span>New Event</span></a></li>
            </ul>
        </div>
    </div>
    <div runat="server" id="dEditForm" style="width: 90%;">
        <div id="sectiontitle">
            <h1>
                <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit an Event Registration" /></h1>
        </div>
        <div id="ContentBody">
            <table cellspacing="1" cellpadding="1" width="97%" align="center" border="0">
                <tr>
                    <td>
                        Event Registration Id
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlEventRSVPId"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Event
                    </td>
                    <td>
                        <asp:DropDownList runat="server" CssClass="formField" ID="ddlEvent">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        First Name
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="formField" ID="txtFirstName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Name
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="formField" ID="txtLastName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        RSVP Status
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblRSVP">
                            <asp:ListItem Value="1" Text="I Am Attending"></asp:ListItem>
                            <asp:ListItem Value="0" Text="I Will Not Be Attending"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Organization
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="formField" ID="txtOrganization"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Job Title
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="formField" ID="txtJobTitle"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        E-Mail
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="formField" ID="txtEMail"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Phone
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="formField" ID="txtPhone"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtPhone"
                            Mask="999-999-9999">
                        </asp:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Number of Guests Attending (in addition to you)
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="formField" ID="txtNoGuests"></asp:TextBox>
                        <asp:NumericUpDownExtender ID="txtImportance_NumericUpDownExtender" runat="server"
                            Maximum="100" Minimum="0" TargetControlID="txtNoGuests" Width="55" Step="1">
                        </asp:NumericUpDownExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Guest Names
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtGuestNames" TextMode="MultiLine" Rows="4"></asp:TextBox>
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
                    <td>
                        Date Added
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlDateAdded"></asp:Literal>
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
                        Date Updated
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlDateUpdated"></asp:Literal>
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
                                        Text="Delete" OnClientClick="return confirm('Warning: This will delete the EventRegistration from the database.');" />
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
    </div>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
