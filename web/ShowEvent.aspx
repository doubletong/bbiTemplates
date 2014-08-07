<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true" 
CodeFile="ShowEvent.aspx.cs" Inherits="ShowEvent" %>

<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <table id="Table3" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
        <tr>
            <td colspan="2">
                <asp:HyperLink runat="server" ID="hlnkEdit" ImageUrl="~/Images/Edit.gif" Text="Edit" />
            </td>
        </tr>
        <tr>
            <td width="175" align="left">
                <font size="2"><strong>Event:</strong></font>
            </td>
            <td align="left" width="280">
                <asp:Label ID="EvTitle" runat="server" Font-Size="Small" Font-Bold="True" EnableViewState="false"></asp:Label>
            </td>
            <td class="AddEvent" rowspan="6">
                <asp:HyperLink ID="hlnkAddToCalendar" runat="server">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar.png" /><br />
                    Add to your Calendar</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td align="left">
                <font size="2"><strong>Date:</strong></font>
            </td>
            <td align="left">
                <asp:Label EnableViewState="false" ID="EvDate" runat="server"></asp:Label><strong></strong>
            </td>
        </tr>
        <tr>
            <td align="left">
                <font size="2"><strong>Time</strong></font>
            </td>
            <td align="left">
                <asp:Label EnableViewState="false" ID="EvTime" runat="server"></asp:Label><strong></strong>
            </td>
        </tr>
        <tr>
            <td align="left">
                <font size="2"><strong>Location</strong></font>
            </td>
            <td align="left">
                <asp:Label EnableViewState="false" ID="EvLoc" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <font size="2"><strong>Address</strong></font>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblAddress"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <font size="2"><strong>City</strong></font>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblCity"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <font size="2"><strong>State</strong></font>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblState"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <font size="2"><strong>Zip Code</strong></font>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblZipCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <br />
                <br />
                <asp:Label EnableViewState="false" ID="EventDesc" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Names="Arial"
                    ForeColor="Black" NavigateUrl="~/BrowseEvents.aspx">Back to Calendar</asp:HyperLink>
            </td>
            <td colspan="2">
                <asp:HyperLink ID="hlnkRegister" runat="server" Font-Bold="True" Font-Names="Arial"
                    ForeColor="Black">Register for Event</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="RightColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:NewsletterBox ID="NewsletterBox1" runat="server" />
</asp:Content>
