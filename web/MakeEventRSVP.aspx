<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true" CodeFile="MakeEventRSVP.aspx.cs" Inherits="MakeEventRSVP" %>


<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div runat="server" id="dEditForm" style="width: 90%;" class="AdminLayout">
        <div>
            <div id="ContentTitle">
                <h1>
                    Change your password</h1>
                <h1>
                    <asp:Literal runat="server" ID="ltlStatus" Text="Please Fill Out the Following Information" /></h1>
            </div>
        </div>
        <div id="ContentBody">
            <asp:MultiView runat="server" ID="mvRegistration">
                <asp:View runat="server" ID="vRegister">
                    <table cellspacing="1" cellpadding="1" width="97%" align="center" border="0">
                        <tr>
                            <td>
                                Event
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtEventId"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                First Name
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="formField" ID="txtFirstName"></asp:TextBox>
                                <span class="RequiredField">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="txtFirstName" Display="Dynamic" ErrorMessage="You must enter your First name"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Last Name
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="formField" ID="txtLastName"></asp:TextBox>
                                <span class="RequiredField">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="txtLastName" Display="Dynamic" ErrorMessage="You must enter your Last name"></asp:RequiredFieldValidator>
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
                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtPhone"
                                    Mask="(999)999-9999" MaskType="Number" ClearMaskOnLostFocus="false">
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
                                <asp:TextBox runat="server" CssClass="formField" TextMode="MultiLine" Rows="4" ID="txtGuestNames"></asp:TextBox>
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
                                            <asp:Button ID="cmdUpdate" runat="server" BorderStyle="None" Text="Register" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View runat="server" ID="vConfirmation">
                    <table id="Table3" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
                        <tr>
                            <td width="175" align="left">
                                <font size="2"><strong>Event:</strong></font>
                            </td>
                            <td align="left" width="280">
                                <asp:Label ID="EvTitle" runat="server" Font-Size="Small" Font-Bold="True" EnableViewState="false"></asp:Label>
                            </td>
                            <td align="center" rowspan="6" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <font size="2"><strong>Date:</strong></font>
                            </td>
                            <td align="left">
                                <asp:Label EnableViewState="false" ID="EvDate" runat="server" Font-Size="Small" Font-Bold="True"></asp:Label><strong></strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <font size="2"><strong>Time</strong></font>
                            </td>
                            <td align="left">
                                <asp:Label EnableViewState="false" ID="EvTime" runat="server" Font-Size="Small" Font-Bold="True"></asp:Label><strong></strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <font size="2"><strong>Location</strong></font>
                            </td>
                            <td align="left">
                                <asp:Label EnableViewState="false" ID="EvLoc" runat="server" Font-Size="Small" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <font size="2"><strong>Address</strong></font>
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblAddress" Font-Size="Small" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <font size="2"><strong>City</strong></font>
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblCity" Font-Size="Small" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <font size="2"><strong>State</strong></font>
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblState" Font-Size="Small" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <font size="2"><strong>Zip Code</strong></font>
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblZipCode" Font-Size="Small" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <fieldset>
                                    <legend>Registration Information</legend>
                                    <table id="Table1" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
                                        <tr>
                                            <td>
                                                First Name
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlFirstName"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Last Name
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlLastname"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                RSVP
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlRSVP"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Organization
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlOrganization"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Job Title
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlJobTitle"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                E-Mail
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlEmail"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Phone
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlPhone"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Number of Guests
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlNoGuests"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Guest Names
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltlGuestsNames"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <br />
                                <asp:Label EnableViewState="false" ID="EventDesc" runat="server" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="RightColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:NewsletterBox ID="NewsletterBox1" runat="server" />
</asp:Content>
