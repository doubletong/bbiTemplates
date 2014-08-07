using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BBICMS;
using BBICMS.Store;

public partial class Controls_ShoppingCartBox : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            ShoppingCart lShoppingCart = Profile.ShoppingCart;
            lvOrderItems.DataSource = lShoppingCart.Items;
            lvOrderItems.DataBind();

            if ((lShoppingCart != null) && lShoppingCart.Items.Count > 0)
            {
                lblSubtotal.Text = Helpers.FormatPrice(lShoppingCart.Total);
                lblSubtotal.Visible = true;
                lblSubtotalHeader.Visible = true;
                panLinkShoppingCart.Visible = true;
            }
            else
            {
                lblSubtotal.Visible = false;
                lblSubtotalHeader.Visible = false;
                panLinkShoppingCart.Visible = false;
            }
        }
    }
}