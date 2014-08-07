using System;
using System.IO;
using System.Web.UI;
using BBICMS;
using BBICMS.Store;

public partial class Featuredproduct : UserControl
{
    public int ProductId
    {
        get
        {
            if ((ViewState["ProductId"] != null))
            {
                return int.Parse(ViewState["ProductId"].ToString());
            }
            return 0;
        }
        set { ViewState["ProductId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        using (ProductsRepository Productrpt = new ProductsRepository())
        {
            Product lProduct = new Product();

            if (ProductId > 0)
            {
                lProduct = Productrpt.GetProductById(ProductId);
            }
            else
            {
                lProduct = Productrpt.GetRandomProduct();
                ProductId = lProduct.ProductID;
            }

            if ((lProduct != null))
            {
                ltlTitle.Text = string.Format("<b>{0}</b>", lProduct.Title);

                string sURL = string.Format("~/Store/Product_{0}/",lProduct.ProductID);

                hlnkProductImage.Text = lProduct.Title;
                hlnkProductImage.ImageUrl = ResolveUrl(lProduct.SmallImageUrl);
                hlnkProductImage.NavigateUrl = sURL;

                ltlUnitPrice.Text = Helpers.FormatPrice(lProduct.UnitPrice);


                hlnkMoreDetails.NavigateUrl = sURL;
            }
        }
    }

    protected void lbtnAddToCart_Click(object sender, EventArgs e)
    {
        using (ProductsRepository Productrpt = new ProductsRepository())
        {
            Product vProduct = Productrpt.GetProductById(ProductId);

            ShoppingCart lShoppingCart = Profile.ShoppingCart;
            //StoreHelper.GetShoppingCart()
            lShoppingCart.InsertItem(vProduct.ProductID, vProduct.Title, vProduct.SKU, vProduct.FinalUnitPrice);

            Response.Redirect("ShoppingCart.aspx", false);
        }
    }
}