using System;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using AjaxControlToolkit;
using BBICMS.UI.Store;
using BBICMS;
using BBICMS.Store;

public partial class ShowProduct : StoreAdminPage
{
    protected bool UserCanEdit
    {
        get { return (User.Identity.IsAuthenticated && (User.IsInRole("Administrators") || User.IsInRole("StoreKeepers"))); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ProductId < 1)
        {
            throw new ApplicationException("Missing parameter on the querystring.");
        }

        if (!IsPostBack)
        {
            // try to load the product with the specified ID, and raise an exception if it doesn't exist
            BindProduct();

            // hide the rating box controls if the current user has already voted for this product
            //int userRating = GetUserRating();
            //if (userRating > 0) ShowUserRating(userRating);
        }
    }

    protected void BindProduct()
    {
        using (ProductsRepository lProductrpt = new ProductsRepository())
        {
            Product vProduct = lProductrpt.GetProductById(ProductId);
            if ((vProduct == null)) throw new ApplicationException("No product was found for the specified ID.");

            rpThumbs.DataSource = vProduct.Photos;
            rpThumbs.DataBind();

            rpProPhotos.DataSource = vProduct.Photos;
            rpProPhotos.DataBind();

            // display all article's data on the page
            lblTitle.Text = vProduct.Title;
            Title = vProduct.Title;
            //lblRating.Text = string.Format(lblRating.Text, vProduct.Votes);
            //availDisplay.Value = vProduct.UnitsInStock;
            lblDescription.Text = vProduct.Description;
         
            lblPrice.Text = FormatPrice(vProduct.FinalUnitPrice);
            lblDiscountedPrice.Text = string.Format(lblDiscountedPrice.Text, FormatPrice(vProduct.UnitPrice),
                                                    vProduct.DiscountPercentage);
            lblDiscountedPrice.Visible = (vProduct.DiscountPercentage > 0);

         //   ltlAvgRating.Text = string.Format(ltlAvgRating.Text, vProduct.AverageRating, vProduct.Title);

            //if (vProduct.SmallImageUrl.Length > 0) imgProduct.Src = vProduct.SmallImageUrl;
            //imgProduct.Alt = vProduct.Title;
            //if (vProduct.FullImageUrl.Length > 0)
            //{
            //    lnkFullImage.NavigateUrl = vProduct.FullImageUrl;
            //    lnkFullImage.Visible = true;
            //}
            //else
            //{
            //    lnkFullImage.Visible = false;
            //}

           
        }
    }

    //protected void ShowUserRating(int rating)
    //{
    //    lblUserRating.Text = string.Format(lblUserRating.Text, rating);
    //    ProductRating.Visible = false;

    //    lblUserRating.Visible = true;
    //}

    protected int GetUserRating()
    {
        int rating = 0;
        HttpCookie cookie = Request.Cookies["Rating_Product" + ProductId.ToString()];
        if ((cookie != null)) rating = int.Parse(cookie.Value);
        return rating;
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        using (ProductsRepository lProductrpt = new ProductsRepository())
        {
            lProductrpt.DeleteProduct(ProductId);
            Response.Redirect("BrowseProducts.aspx", false);
        }
    }

    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        using (ProductsRepository lProductrpt = new ProductsRepository())
        {
            Product lProduct = lProductrpt.GetProductById(ProductId);
            ProfileBase lprofile = Helpers.GetUserProfile();
            ShoppingCart lShoppingCart = Profile.ShoppingCart;
            lShoppingCart.InsertItem(lProduct.ProductID, lProduct.Title, lProduct.SKU, lProduct.FinalUnitPrice);
            Profile.ShoppingCart = lShoppingCart;

            Response.Redirect("ShoppingCart.aspx", false);
        }
    }

    //protected void ProductRating_Changed(object sender, RatingEventArgs e)
    //{
    //    // check whether the user has already rated this article
    //    int userRating = GetUserRating();
    //    if (userRating > 0)
    //    {
    //        ShowUserRating(userRating);
    //    }
    //    else
    //    {
    //        using (ProductsRepository lProductrpt = new ProductsRepository())
    //        {
    //            // rate the product, then create a cookie to remember this user's rating
    //            userRating = ProductRating.CurrentRating;
    //            lProductrpt.RateProduct(ProductId, userRating);
    //            ShowUserRating(userRating);

    //            HttpCookie cookie = new HttpCookie("Rating_Product" + ProductId.ToString(), userRating.ToString());
    //            cookie.Expires = DateTime.Now.AddDays(Settings.Store.RatingLockInterval);
    //            Response.Cookies.Add(cookie);
    //        }
    //    }
    //}
}