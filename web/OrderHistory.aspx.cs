using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BBICMS;
using BBICMS.BLL.Store;
using BBICMS.Store;
using BBICMS.UI;

public partial class OrderHistory : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BindOrders();
        }
    }

    protected void BindOrders()
    {
        using (OrdersRepository lOrderrpt = new OrdersRepository())
        {
            List<Order> lOrders = lOrderrpt.GetOrdersByUser(Helpers.CurrentUserName);
            lvOrders.DataSource = lOrders;
            lvOrders.DataBind();

            DataPager pagerBottom = (DataPager) lvOrders.FindControl("pagerBottom");

            if (null != pagerBottom)
            {
                SetupListViewPager(lOrders.Count, pagerBottom);
            }
        }
    }

    protected void lvOrders_ItemDataBound(Object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
    {
        ListViewDataItem lvdi = (ListViewDataItem) e.Item;

        if (lvdi.ItemType == ListViewItemType.DataItem)
        {
            Repeater repOrderItems = (Repeater) lvdi.FindControl("repOrderItems");
            Order lOrder = (Order) lvdi.DataItem;

            repOrderItems.DataSource = lOrder.OrderItems;
            repOrderItems.DataBind();
        }
    }

    protected void lvOrders_PagePropertiesChanged(Object sender, System.EventArgs e)
    {
        BindOrders();
    }
}