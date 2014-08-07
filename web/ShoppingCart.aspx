<%@ Page Title="我的购物车_{0}" Language="C#" MasterPageFile="~/TBHMain.master" Theme="DarkBeer" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="ShoppingCart_Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


        
   <asp:Wizard ID="wizSubmitOrder" runat="server" ActiveStepIndex="0" CancelButtonText="Continue Shopping"
                CancelButtonType="Link" CancelDestinationPageUrl="~/BrowseProducts.aspx" DisplayCancelButton="True"
                DisplaySideBar="False" FinishPreviousButtonType="Link" StartNextButtonText="Proceed with order"
                StartNextButtonType="Link" Width="100%" StepNextButtonText="Proceed with order" OnActiveStepChanged="wizSubmitOrder_ActiveStepChanged"
                StepNextButtonType="Link" StepPreviousButtonText="Modify data in previous step" OnFinishButtonClick="wizSubmitOrder_FinishButtonClick" OnNextButtonClick="wizSubmitOrder_NextButtonClick"
       
                StepPreviousButtonType="Link" FinishCompleteButtonText="Submit Order" FinishCompleteButtonType="Link"
                FinishPreviousButtonText="Modify data in previous step">
                <WizardSteps>
                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Shopping Cart">
                        <div>
                            <div id="ContentTitle">
                                <h1>
                                    Shopping Cart</h1>
                            </div>
                            <div id="ContentBody">
                                <p>
                                    Review and update the quantity of the products added to the cart before proceeding
                                    to checkout, or continue shopping.
                                </p>
                                <div id="dShoppingCart">
                                    <asp:ListView ID="lvOrderItems" runat="server" OnItemDeleting="lvOrderItems_ItemDeleting" DataKeyNames="ID">
                                        <LayoutTemplate>
                                            <table>
                                                <tr class="AdminListHeader">
                                                    <td class="CartProductName">
                                                        Product
                                                    </td>
                                                    <td>
                                                        Price
                                                    </td>
                                                    <td>
                                                        Quantity
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="itemPlaceHolder">
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="hlnkProduct" ForeColor="#800000" NavigateUrl=''><%# Eval("Title") %></asp:HyperLink>
                                                </td>
                                                <td class="AdminRightCell">
                                                    <%# FormatPrice(Eval("UnitPrice")) %>
                                                </td>
                                                <td class="AdminRightCell">
                                                    <asp:TextBox runat="server" ID="txtQuantity" Text='<%# Bind("Quantity") %>' MaxLength="6"
                                                        Width="30px" AutoPostBack="true" OnTextChanged="QtyChanged"></asp:TextBox>
                                                    
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="btnDelete" CommandArgument='<%# Eval("ID").ToString() %>'
                                                        CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                                        OnClientClick="return confirm('Warning: This will delete the Product from the shopping cart.');" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <b>The shopping cart is empty</b></EmptyDataTemplate>
                                    </asp:ListView>
                                    <asp:Panel runat="server" ID="panTotals">
                                        <div style="text-align: right; font-weight: bold; padding-top: 4px;">
                                            Subtotal:
                                            <asp:Literal runat="server" ID="lblSubtotal" />
                                            <p>
                                                Shipping Method:
                                                <asp:DropDownList ID="ddlShippingMethods" runat="server" DataTextField="TitleAndPrice" 
                                                    OnSelectedIndexChanged="ddlShippingMethods_SelectedIndexChanged"
                                                    DataValueField="Price" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="valRequireQuantity" runat="server" ControlToValidate="ddlShippingMethods"
                                                    SetFocusOnError="true" ValidationGroup="ShippingMethod" Text="A Shipping method is required."
                                                    ToolTip="A Shipping method is required." Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </p>
                                            <p>
                                                <u>Total:</u>
                                                <asp:Literal runat="server" ID="lblTotal" />
                                            </p>
                                            <asp:Button ID="btnUpdateTotals" runat="server" OnClick="btnUpdateTotals_Click" Text="Update totals" />
                                            <br />
                                            <br />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep2" runat="server" Title="Shipping Address">
                        <div>
                            <div id="Div1">
                                <h1>
                                    Shipping Address</h1>
                            </div>
                            <div id="Div2">
                                <asp:MultiView ID="mvwShipping" runat="server">
                                    <asp:View ID="vwLoginRequired" runat="server">
                                        <p>
                                            An account is required to proceed with the order submission. If you already have
                                            an account please login now, otherwise <a href="Register.aspx">create a new account</a>
                                            for free.</p>
                                    </asp:View>
                                    <asp:View ID="vwShipping" runat="server">
                                        <p>
                                            Fill the form below with the shipping address for your order. All information is
                                            required, except for phone and fax numbers.
                                        </p>
                                        <table cellpadding="2" width="410">
                                            <tr>
                                                <td width="110" class="fieldname">
                                                    <asp:Label runat="server" ID="lblFullName" AssociatedControlID="txtFullName" Text="姓名:" />
                                                </td>
                                                <td width="300">
                                                    <asp:TextBox ID="txtFullName" runat="server" Width="100%" />
                                                    <asp:RequiredFieldValidator ID="valRequireFirstName" runat="server" ControlToValidate="txtFullName"
                                                        SetFocusOnError="True" ValidationGroup="ShippingAddress" Text="The Your Name field is required."
                                                        ToolTip="The Your Name field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="fieldname">
                                                    <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" Text="E-mail:" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtEmail" Width="100%" />
                                                    <asp:RequiredFieldValidator ID="valRequireEmail" runat="server" ControlToValidate="txtEmail"
                                                        SetFocusOnError="True" ValidationGroup="ShippingAddress" Text="The E-mail field is required."
                                                        ToolTip="The E-mail field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ID="valEmailPattern" Display="Dynamic"
                                                        SetFocusOnError="True" ValidationGroup="ShippingAddress" ControlToValidate="txtEmail"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Text="The E-mail address you specified is not well-formed."
                                                        ToolTip="The E-mail address you specified is not well-formed."></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="fieldname">
                                                    <asp:Label runat="server" ID="lblStreet" AssociatedControlID="txtStreet" Text="Street:" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtStreet" Width="100%" />
                                                    <asp:RequiredFieldValidator ID="valRequireStreet" runat="server" ControlToValidate="txtStreet"
                                                        SetFocusOnError="True" ValidationGroup="ShippingAddress" Text="The Street field is required."
                                                        ToolTip="The Street field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="fieldname">
                                                    <asp:Label runat="server" ID="lblPostalCode" AssociatedControlID="txtPostalCode"
                                                        Text="Zip / Postal code:" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtPostalCode" Width="100%" />
                                                    <asp:RequiredFieldValidator ID="valRequirePostalCode" runat="server" ControlToValidate="txtPostalCode"
                                                        SetFocusOnError="True" ValidationGroup="ShippingAddress" Text="The Postal Code field is required."
                                                        ToolTip="The Postal Code field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="fieldname">
                                                    <asp:Label runat="server" ID="lblCity" AssociatedControlID="txtCity" Text="City:" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtCity" Width="100%" />
                                                    <asp:RequiredFieldValidator ID="valRequireCity" runat="server" ControlToValidate="txtCity"
                                                        SetFocusOnError="True" ValidationGroup="ShippingAddress" Text="The City field is required."
                                                        ToolTip="The City field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="fieldname">
                                                    <asp:Label runat="server" ID="lblState" AssociatedControlID="ddlState" Text="State / Region:" />
                                                </td>
                                                <td>
                                                    <asp:StateDropDownList runat="server" ID="ddlState" Width="100%" CssClass="formField">
                                                    </asp:StateDropDownList>
                                                    <asp:RequiredFieldValidator ID="valRequireState" runat="server" ControlToValidate="ddlState"
                                                        SetFocusOnError="True" ValidationGroup="ShippingAddress" Text="The State field is required."
                                                        ToolTip="The State field is required." InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="fieldname">
                                                    <asp:Label runat="server" ID="lblCountry" AssociatedControlID="ddlCountries" Text="Country:" />
                                                </td>
                                                <td>
                                                    <asp:CountryDropDownList ID="ddlCountries" runat="server" CssClass="formField">
                                                    </asp:CountryDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="fieldname">
                                                    <asp:Label runat="server" ID="lblPhone" AssociatedControlID="txtPhone" Text="Phone:" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtPhone" Width="100%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="fieldname">
                                                    <asp:Label runat="server" ID="lblFax" AssociatedControlID="txtFax" Text="Fax:" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtFax" Width="100%" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView></div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep3" runat="server" Title="Order Confirmation">
                        <div>
                            <div id="Div3">
                                <h1>
                                    Order Summary
                                </h1>
                            </div>
                            <div id="Div4">
                                <p>
                                    Please carefully review the order information below. If you want to change something
                                    click the link below to go back to the previous pages and make the corrections.
                                    If everything is ok go ahead and submit your order.
                                </p>
                                <img src="Images/paypal.gif" style="float: right" alt="" />
                                <b>Order Details</b>
                                <br />
                                <asp:Repeater runat="server" ID="repOrderItems">
                                    <ItemTemplate>
                                        <img src="Images/ArrowR3.gif" border="0" alt="" />
                                        <%# Eval("Title") %>
                                        -
                                        <%# FormatPrice(Eval("UnitPrice")) %>
                                        &nbsp;&nbsp;<small>(Quantity =
                                            <%# Eval("Quantity") %>)</small>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <br />
                                Subtotal =
                                <asp:Literal runat="server" ID="lblReviewSubtotal" />
                                <br />
                                Shipping Method =
                                <asp:Literal runat="server" ID="lblReviewShippingMethod" />
                                <br />
                                <u>Total</u> =
                                <asp:Literal runat="server" ID="lblReviewTotal" />
                                <br />
                                <b>Shipping Details</b>
                                <br />
                                <asp:Literal runat="server" ID="lblReviewFullName" /><br />
                                <asp:Literal runat="server" ID="lblReviewStreet" /><br />
                                <asp:Literal runat="server" ID="lblReviewCity" />,
                                <asp:Literal runat="server" ID="lblReviewState" />
                                <asp:Literal runat="server" ID="lblReviewPostalCode" /><br />
                                <asp:Literal runat="server" ID="lblReviewCountry" />
                            </div>
                        </div>
                    </asp:WizardStep>
                </WizardSteps>
                <StepNextButtonStyle CssClass="BHButton" />
                <StartNextButtonStyle CssClass="BHButton" />
                <FinishCompleteButtonStyle CssClass="BHButton" />
                <FinishPreviousButtonStyle CssClass="BHButton" />
                <StepPreviousButtonStyle CssClass="BHButton" />
            </asp:Wizard>



    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>

