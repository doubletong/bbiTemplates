using System;
using BBICMS;
using BBICMS.BLL.Store;
using BBICMS.Store;
using BBICMS.UI;

partial class Admin_AddEditOrder : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOrderStatusesToListControl(ddlOrderStatuses, true);

            if (OrderId > 0)
            {
                BindOrder();
            }
            else
            {
                ClearItems();
            }
        }
    }

    protected void ClearItems()
    {
        ltlOrderID.Text = string.Empty;
        ltlAddedDate.Text = string.Empty;
        ddlOrderStatuses.ClearSelection();
        ltlShippingMethod.Text = string.Empty;
        ltlSubTotal.Text = string.Empty;
        ltlShipping.Text = string.Empty;
        ltlCustomerEmail.Text = string.Empty;
        ltlShippingName.Text = string.Empty;
        ltlShippingStreet.Text = string.Empty;
        ltlShippingStreet.Text = string.Empty;
        ltlCityStateZip.Text = string.Empty;
        ltlShipping.Text = string.Empty;
        txtCustomerFax.Text = string.Empty;
        txtShippedDate.Text = string.Empty;
        txtTransactionID.Text = string.Empty;
        txtTrackingID.Text = string.Empty;
        ltlUpdatedDate.Text = string.Empty;


        ltlStatus.Text = string.Empty;
    }

    protected void BindOrder()
    {
        using (OrdersRepository lOrdersrpt = new OrdersRepository())
        {
            Order lOrders = lOrdersrpt.GetOrderById(OrderId);

            if ((lOrders != null))
            {
                ltlOrderID.Text = lOrders.OrderID.ToString();
                ltlAddedDate.Text = lOrders.AddedDate.ToShortDateString();
                ddlOrderStatuses.SelectedValue = lOrders.StatusID.ToString();
                ltlShippingMethod.Text = lOrders.ShippingMethod;
                ltlSubTotal.Text = string.Format("{0:C}", lOrders.SubTotal);
                ltlShipping.Text = string.Format("{0:C}", lOrders.Shipping);
                ltlShippingName.Text = string.Format("{0} {1}", lOrders.ShippingFirstName, lOrders.ShippingLastName);
                ltlShippingStreet.Text = lOrders.ShippingStreet;
                ltlCityStateZip.Text = string.Format("{0} {1} {2} {3}", lOrders.ShippingCity, lOrders.ShippingState,
                                                     lOrders.ShippingPostalCode, lOrders.ShippingCountry);
                ltlCustomerEmail.Text = lOrders.CustomerEmail;
                ltlCustomerPhone.Text = Helpers.FormatPhoneNumber(lOrders.CustomerPhone);
                txtCustomerFax.Text = Helpers.FormatPhoneNumber(lOrders.CustomerFax);
                txtShippedDate.Text = (lOrders.ShippedDate != null) ? lOrders.ShippedDate.ToString() : string.Empty;
                txtTransactionID.Text = lOrders.TransactionID;
                txtTrackingID.Text = lOrders.TrackingID;
                ltlUpdatedDate.Text = lOrders.UpdatedDate.ToShortDateString();
                ltlActive.Text = lOrders.Active.ToString();
                ltlAddedBy.Text = lOrders.AddedBy;
                ltlUpdatedBy.Text = lOrders.UpdatedBy;

                repOrderItems.DataSource = lOrders.OrderItems;
                repOrderItems.DataBind();

                ltlStatus.Text = "Edit The Order";
            }
            else
            {
                OrderId = 0;
                ltlStatus.Text = "You cannot creat a new order from the Administration";
            }
        }
    }

    protected void ManageOrders()
    {
        Response.Redirect("ManageOrders.aspx");
    }

    protected void UpdateOrders()
    {
        using (OrdersRepository lOrdersrpt = new OrdersRepository())
        {
            Order lOrders = new Order();

            if (OrderId > 0)
            {
                lOrders = lOrdersrpt.GetOrderById(OrderId);
            }
            else
            {
                lOrders = new Order();
            }

            lOrders.StatusID = int.Parse(ddlOrderStatuses.SelectedValue);
            lOrders.ShippedDate = DateTime.Parse(txtShippedDate.Text);
            lOrders.TransactionID = txtTransactionID.Text;
            lOrders.TrackingID = txtTrackingID.Text;

            lOrders.UpdatedDate = DateTime.Now;
            lOrders.UpdatedBy = UserName;

            if ((lOrdersrpt.AddOrder(lOrders) != null))
            {
                IndicateUpdated(lOrdersrpt, "Order", ltlStatus, cmdDelete);
            }
            else
            {
                IndicateNotUpdated(lOrdersrpt, "Order", ltlStatus);
            }
        }
    }


    protected void DeleteOrder()
    {
        using (OrdersRepository lOrdersrpt = new OrdersRepository())
        {
            lOrdersrpt.DeleteOrder(lOrdersrpt.GetOrderById(OrderId));
        }
        ManageOrders();
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        DeleteOrder();
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        UpdateOrders();
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        ManageOrders();
    }
}