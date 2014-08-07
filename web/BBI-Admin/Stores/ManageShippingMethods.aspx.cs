using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.Store;
using BBICMS.UI;
using BBICMS;

partial class Admin_ManageShippingMethods : AdminPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = string.Format(this.Title, Helpers.Settings.SiteName);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindShippingMethod();
        }
    }

    protected void BindShippingMethod()
    {
        using (ShippingMethodsRepository lShippingMethodrpt = new ShippingMethodsRepository())
        {
            List<ShippingMethod> lShippingMethods = lShippingMethodrpt.GetShippingMethods();
            lvShippingMethods.DataSource = lShippingMethods;
            lvShippingMethods.DataBind();

            DataPager pagerBottom = (DataPager)lvShippingMethods.FindControl("pagerBottom");

            if ((pagerBottom != null))
            {
                if (lShippingMethods.Count <= pagerBottom.PageSize)
                {
                    pagerBottom.Visible = false;
                }
                else
                {
                    pagerBottom.Visible = true;
                }
            }
        }
    }

    protected void lvShippingMethods_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindShippingMethod();
    }
}