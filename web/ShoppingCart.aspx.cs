using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BBICMS;
using BBICMS.Store;
using BBICMS.UI.Store;

using ShoppingCartItem = BBICMS.Store.ShoppingCartItem;

public partial class ShoppingCart_Page : StoreAdminPage
{

    private ShoppingCart _lShoppingCart;
    protected ShoppingCart lShoppingCart
    {
        get
        {
            if ((_lShoppingCart == null))
            {
                _lShoppingCart = Profile.ShoppingCart;
            }
            return _lShoppingCart;
        }
        set { _lShoppingCart = value; }
    }

    protected void BindShippingMethods()
    {
        BindShippingMethods(ShippingMethodId);
    }

    protected void BindShippingMethods(int vShippingMethodId)
    {

        using (ShippingMethodsRepository lShippingMethodsrpt = new ShippingMethodsRepository())
        {

            ddlShippingMethods.DataSource = lShippingMethodsrpt.GetShippingMethods();
            ddlShippingMethods.DataBind();

            ddlShippingMethods.Items.Insert(0, new ListItem("Select Shipping Method", "0"));


            ddlShippingMethods.SelectedValue = vShippingMethodId.ToString();

        }
    }

    protected void Page_Init(object sender, System.EventArgs e)
    {

        ScriptManager sm = ScriptManager.GetCurrent(this);

        if ((sm != null))
        {

            sm.EnableHistory = true;
            sm.Navigate += ScriptManagerNavigate;

        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BindShippingMethods();
            BindShoppingCart();
            UpdateTotals();
        }
        else
        {
            bool isAuthenticated = this.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                mvwShipping.ActiveViewIndex = 1;
            }
            else
            {
                mvwShipping.ActiveViewIndex = 0;
                wizSubmitOrder.StartNextButtonText = string.Empty;
            }

        }
    }

    protected void BindShoppingCart()
    {

        lvOrderItems.DataSource = lShoppingCart.Items;
        lvOrderItems.DataBind();
    }

    protected void UpdateTotals()
    {

        int i = 0;

        foreach (ListViewDataItem lvdi in lvOrderItems.Items)
        {

            ShoppingCartItem lCartItem = null; // = (ShoppingCartItem)lShoppingCart.Items.ElementAtOrDefault(i);

            IEnumerator cartItems = lShoppingCart.Items.GetEnumerator();
            while (cartItems.MoveNext())
            {
                lCartItem = (ShoppingCartItem)cartItems.Current;
                break;
            }

            if (lCartItem != null)
            {

                int id = lCartItem.ID;
                int quantity = int.Parse(((TextBox)lvdi.FindControl("txtQuantity")).Text);

                lShoppingCart.UpdateItemQuantity(id, quantity);
            }

            i += 1;
        }

        // display the subtotal and the total amounts
        lblSubtotal.Text = FormatPrice(lShoppingCart.Total);
        lblTotal.Text = FormatPrice(lShoppingCart.Total + decimal.Parse(ddlShippingMethods.SelectedValue));

        // if the shopping cart is empty, hide the link to proceed
        if (lShoppingCart.Items.Count == 0)
        {
            wizSubmitOrder.StartNextButtonText = string.Empty;
            panTotals.Visible = false;
        }
        else
        {
            wizSubmitOrder.StartNextButtonText = "Proceed with order";
        }

        Profile.ShoppingCart = lShoppingCart;


        BindShoppingCart();
    }

    protected void lvOrderItems_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
    {

        ShoppingCart lShoppingCart = Profile.ShoppingCart;
        int lProductId = int.Parse(lvOrderItems.DataKeys[e.ItemIndex][0].ToString());

        lShoppingCart.DeleteItem(lProductId);

        Profile.ShoppingCart = lShoppingCart;
        lShoppingCart = null;

        BindShoppingCart();
        UpdateTotals();
    }

    protected void btnUpdateTotals_Click(object sender, System.EventArgs e)
    {
        UpdateTotals();
    }

    protected void ScriptManagerNavigate(object sender, HistoryEventArgs e)
    {

        string indexString = e.State["ShoppingCart"];

        if (string.IsNullOrEmpty(indexString))
        {
            wizSubmitOrder.ActiveStepIndex = 0;
        }
        else
        {
            int index = int.Parse(indexString);
            wizSubmitOrder.ActiveStepIndex = index;

        }
    }

    protected void wizSubmitOrder_ActiveStepChanged(object sender, System.EventArgs e)
    {

        ScriptManager sm = ScriptManager.GetCurrent(this);

        if ((sm != null))
        {
            if (sm.IsInAsyncPostBack & !sm.IsNavigating)
            {
                sm.AddHistoryPoint("ShoppingCart", wizSubmitOrder.ActiveStepIndex.ToString(),
                    "Shopping Cart Step " + wizSubmitOrder.ActiveStepIndex);
            }
        }

        if (wizSubmitOrder.ActiveStepIndex == 1)
        {
            UpdateTotals();

            if (this.User.Identity.IsAuthenticated)
            {

                if (txtFullName.Text.Trim().Length == 0) txtFullName.Text = Profile.FullName;
                if (txtEmail.Text.Trim().Length == 0) txtEmail.Text = Membership.GetUser().Email;
                if (txtStreet.Text.Trim().Length == 0) txtStreet.Text = Profile.Address.Street;
                if (txtPostalCode.Text.Trim().Length == 0) txtPostalCode.Text = Profile.Address.PostalCode;
                if (txtCity.Text.Trim().Length == 0) txtCity.Text = Profile.Address.City;

                if (ddlState.SelectedIndex == 0 && !string.IsNullOrEmpty(Profile.Address.State)) ddlState.SelectedValue = Profile.Address.State;

                if (ddlCountries.SelectedIndex == 0 && !string.IsNullOrEmpty(Profile.Address.Country)) ddlCountries.SelectedValue = Profile.Address.Country;

                if (txtPhone.Text.Trim().Length == 0) txtPhone.Text = Profile.Contacts.Phone;
                if (txtFax.Text.Trim().Length == 0) txtFax.Text = Profile.Contacts.Fax;

            }
        }
        else if (wizSubmitOrder.ActiveStepIndex == 2)
        {
            lblReviewFullName.Text = txtFullName.Text;

            lblReviewStreet.Text = txtStreet.Text;
            lblReviewCity.Text = txtCity.Text;
            lblReviewState.Text = ddlState.SelectedValue;
            lblReviewPostalCode.Text = txtPostalCode.Text;
            lblReviewCountry.Text = ddlCountries.SelectedValue;

            lblReviewSubtotal.Text = this.FormatPrice(lShoppingCart.Total);
            lblReviewShippingMethod.Text = ddlShippingMethods.SelectedItem.Text;
            lblReviewTotal.Text = this.FormatPrice(lShoppingCart.Total + decimal.Parse(ddlShippingMethods.SelectedValue));

            repOrderItems.DataSource = lShoppingCart.Items;

            repOrderItems.DataBind();

        }
    }

    protected void wizSubmitOrder_FinishButtonClick(object sender, System.Web.UI.WebControls.WizardNavigationEventArgs e)
    {

        // check that the user is still logged in (the cookie may have expired)
        // and if not redirect to the login page
        if (this.User.Identity.IsAuthenticated)
        {

            //We can reasonably assume if a customer is going to pay for the order via PayPal, 
            //the order can only be migrated from the cart to the Order tables once. 
            //Otherwise interaction with PayPal becomes a lot more tricky.
            string shippingMethod = ddlShippingMethods.SelectedItem.Text;
            shippingMethod = shippingMethod.Substring(0, shippingMethod.LastIndexOf("(")).Trim();

            Order lorder = new Order();
            using (OrdersRepository lorderrpt = new OrdersRepository())
            {

                ShoppingCart lShoppingCart = Profile.ShoppingCart;

                // saves the order into the DB, and clear the shopping cart in the profile
                lorder = lorderrpt.InsertOrder(lShoppingCart, shippingMethod, decimal.Parse(ddlShippingMethods.SelectedValue), txtFullName.Text, "", txtStreet.Text, txtPostalCode.Text, txtCity.Text, ddlState.SelectedValue, ddlCountries.SelectedValue,
                txtEmail.Text, txtPhone.Text, txtFax.Text, "");

                lShoppingCart.Clear();

                Profile.ShoppingCart = lShoppingCart;
            }

            // redirect to PayPal for the credit-card payment

            this.Response.Redirect(StoreHelper.GetPayPalPaymentUrl(lorder), false);
        }
        else
        {
            this.RequestLogin();
        }
    }

    protected void wizSubmitOrder_NextButtonClick(object sender, System.Web.UI.WebControls.WizardNavigationEventArgs e)
    {

        //Make sure a shipping method has been selected.
        if (wizSubmitOrder.ActiveStepIndex == 0)
        {
            Page.Validate("ShippingMethod");
            if (!Page.IsValid)
            {
                e.Cancel = true;
            }
            ShippingMethodId = int.Parse(ddlShippingMethods.SelectedValue);

        }
    }

    protected void ddlShippingMethods_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        UpdateTotals();
    }

    protected void QtyChanged(object sender, System.EventArgs e)
    {
        UpdateTotals();
    }
}