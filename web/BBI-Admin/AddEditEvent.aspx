<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditEvent.aspx.cs" Inherits="Admin_AddEditEvent" %>

<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Manage Event</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="ManageEvents.aspx"><span>Events</span></a></li>
                <li><a href="AddEditEvent.aspx"><span>New Event</span></a></li>
                <li><a href="ManageEventRSVPs.aspx"><span>Event RSVPs</span></a></li>
                <li><a href="AddEditEventRSVP.aspx"><span>New Event RSVP</span></a></li>
            </ul>
        </div>
    </div>
    <div id="sectiontitle">
        <h1>
            <asp:Literal runat="server" ID="ltlStatus" Text="Manage an Event" /></h1>
    </div>
    <div id="ContentBody">
        <table cellspacing="1" cellpadding="1" width="97%" align="center" border="0">
            <tr>
                <td>
                    Event Id
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltlEventId"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    Event Title
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEventTitle"></asp:TextBox>
                    <span class="RequiredField">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtEventTitle" Display="Dynamic" ErrorMessage="You must enter an Event Title"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Event Desc
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEventDesc" TextMode="MultiLine" Rows="4" Columns="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Event Date
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEventDate" Width="70"></asp:TextBox><asp:Image
                        runat="Server" ID="iEventDate" ImageUrl="~/images/Calendar.png" /><span class="RequiredField">*</span><br />
                    <asp:CalendarExtender ID="ceEventDate" runat="server" TargetControlID="txtEventDate"
                        PopupButtonID="iEventDate">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="meeEventDate" runat="server" TargetControlID="txtEventDate"
                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:MaskedEditValidator ID="mevEventDate" runat="server" ControlExtender="meeEventDate"
                        ControlToValidate="txtEventDate" IsValidEmpty="True" EmptyValueMessage="Date is required"
                        InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic"
                        TooltipMessage="Input a Date" />
                </td>
            </tr>
            <tr>
                <td>
                    Event End Date
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEventEndDate" Width="70"></asp:TextBox><asp:Image
                        runat="Server" ID="iEventEndDate" ImageUrl="~/images/Calendar.png" /><br />
                    <asp:CalendarExtender ID="ceEventEndDate" runat="server" TargetControlID="txtEventEndDate"
                        PopupButtonID="iEventEndDate">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="meeEventEndDate" runat="server" TargetControlID="txtEventEndDate"
                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:MaskedEditValidator ID="mevEventEndDate" runat="server" ControlExtender="meeEventEndDate"
                        ControlToValidate="txtEventEndDate" IsValidEmpty="True" EmptyValueMessage="Date is required"
                        InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic"
                        TooltipMessage="Input a Date" />
                </td>
            </tr>
            <tr>
                <td>
                    Event Expires
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEventExpires" Width="70"></asp:TextBox><asp:Image
                        runat="Server" ID="iEventExpires" ImageUrl="~/images/Calendar.png" /><br />
                    <asp:CalendarExtender ID="ceEventExpires" runat="server" TargetControlID="txtEventExpires"
                        PopupButtonID="iEventExpires">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="meeEventExpires" runat="server" TargetControlID="txtEventExpires"
                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:MaskedEditValidator ID="mevEventExpires" runat="server" ControlExtender="meeEventExpires"
                        ControlToValidate="txtEventExpires" IsValidEmpty="True" EmptyValueMessage="Date is required"
                        InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic"
                        TooltipMessage="Input a Date" />
                </td>
            </tr>
            <tr>
                <td>
                    Event Time
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEventTime" Width="85"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtEventTime"
                        Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                    <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                        ControlToValidate="txtEventTime" IsValidEmpty="False" EmptyValueMessage="Time is required"
                        InvalidValueMessage="Time is invalid" Display="Dynamic" TooltipMessage="Input a start time"
                        EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" />
                </td>
            </tr>
            <tr>
                <td>
                    End Time
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEndTime" Width="85"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEndTime"
                        Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                        ControlToValidate="txtEndTime" IsValidEmpty="True" EmptyValueMessage="Time is required"
                        InvalidValueMessage="Time is invalid" Display="Dynamic" TooltipMessage="Input an end time"
                        EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" />
                </td>
            </tr>
            <tr>
                <td>
                    Event Location
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEventLocation"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Address
                </td>
                <td>
                    <asp:TextBox runat="server" CssClass="formField" ID="txtAddress"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    City
                </td>
                <td>
                    <asp:TextBox runat="server" CssClass="formField" ID="txtCity"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    State
                </td>
                <td>
                    <asp:TextBox runat="server" CssClass="formField" ID="txtState"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Zip Code
                </td>
                <td>
                    <asp:TextBox runat="server" CssClass="formField" ID="txtZipCode"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Featured
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbFeatured"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    Duration
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDuration"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Importance
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtImportance"></asp:TextBox>
                    <asp:NumericUpDownExtender ID="txtImportance_NumericUpDownExtender" runat="server"
                        Maximum="100" Minimum="0" TargetControlID="txtImportance" Width="55" Step="5">
                    </asp:NumericUpDownExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Allow Registration
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbRegistration"></asp:CheckBox>
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
                    Added By
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltlAddedBy"></asp:Literal>
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
                    Updated By
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltlUpdatedBy"></asp:Literal>
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
                <td align="center" colspan="2">
                    &nbsp;
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
                                    Text="Delete" />
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
</asp:Content>
