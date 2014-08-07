using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BBICMS;
using BBICMS.Store;
using BBICMS.UI;

partial class Admin_ManageOrders : AdminPage
{
    
    public string CustomerName {
        get { return txtCustomerName.Text; }
        set { txtCustomerName.Text = value; }
    }
    
    
    public DateTime FromDate {
        get {
            if (string.IsNullOrEmpty(txtFromDate.Text)) {
                return DateTime.MinValue ;
            }
            
            return DateTime.Parse(txtFromDate.Text);
        }
    }
    
    public DateTime ToDate {
        get {
            if (string.IsNullOrEmpty(txtToDate.Text) ) {
                return DateTime.MinValue;
            }
            
            return DateTime.Parse( txtToDate.Text);
        }
    }
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            BindOrderStatusesToListControl(ddlOrderStatuses, true);
            
            txtToDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            txtFromDate.Text =string.Format("{0:yyyy-MM-dd}", DateTime.Now.Subtract(
                new TimeSpan(Helpers.Settings.Store.DefaultOrderListInterval, 0, 0, 0)));
            
            BindOrders();
            
        }
    }
    
    protected void BindOrders()
    {
        using (OrdersRepository lOrdersrpt = new OrdersRepository()) {
            
            List<Order> lOrders =  new List<Order>();;

            if (OrderId > 0)
            {
                lOrders.Add(  lOrdersrpt.GetOrderById(OrderId));
            }else if (OrderStatusId > 0) {
                lOrders = lOrdersrpt.GetOrdersByOrderStatusId( OrderStatusId);
            }
            
            if (OrderStatusId > 0) {
                lOrders = lOrdersrpt.GetOrdersByCustomerName(CustomerName);
            }
            else if (FromDate > DateTime.MinValue || ToDate > DateTime.MinValue)
            {
                lOrders = lOrdersrpt.GetOrdersByDateRange(FromDate, (ToDate == DateTime.MinValue) ? DateTime.MaxValue :ToDate);
            }

            lvOrders.DataSource = lOrders;
            lvOrders.DataBind();
            
            DataPager pagerBottom = (DataPager)lvOrders.FindControl("pagerBottom");
            
            if ((pagerBottom != null)) {
                if (lOrders.Count <= pagerBottom.PageSize) {
                    pagerBottom.Visible = false;
                }
                else {
                    pagerBottom.Visible = true;
                }
            }
        }
    }
    
    protected void lvOrders_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindOrders();
    }
    
    protected void ddlOrderStatuses_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        OrderStatusId = int.Parse(ddlOrderStatuses.SelectedValue);
        BindOrders();
    }
    
    protected void btnListByStatus_Click(object sender, System.EventArgs e)
    {
        BindOrders();
    }
    
    protected void btnListByCustomer_Click(object sender, System.EventArgs e)
    {
        BindOrders();
    }
    
    protected void btnOrderLookup_Click(object sender, System.EventArgs e)
    {
        
        using (OrdersRepository lOrderrpt = new OrdersRepository()) {
            
            Order lOrder = lOrderrpt.GetOrderById(int.Parse(txtOrderID.Text));
            if ((lOrder == null)) {
                lblOrderNotFound.Visible = true;
                return;
            }
                
            Response.Redirect("AddEditOrder.aspx?orderid=" + txtOrderID.Text);
            
        }
    }
    
    
}