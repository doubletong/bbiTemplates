using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using BBICMS.Store;

namespace BBICMS
{
    public class StoreHelper
    {
        public static string GetPayPalPaymentUrl(Order vOrder)
        {
            string serverUrl;
            if (!vOrder.IsValid)
            {
                return "Not a valid order";
            }
            if (Globals.Settings.Store.SandboxMode)
            {
                serverUrl = "https://www.sandbox.paypal.com/us/cgi-bin/webscr";
            }
            else
            {
                serverUrl = "https://www.paypal.com/us/cgi-bin/webscr";
            }
            string amount = vOrder.SubTotal.ToString("N2").Replace(",", ".");
            string shipping = vOrder.Shipping.ToString("N2").Replace(",", ".");
            string firstname = HttpUtility.UrlEncode(vOrder.ShippingFirstName);
            string lastname = HttpUtility.UrlEncode(vOrder.ShippingLastName);
            string address = HttpUtility.UrlEncode(vOrder.ShippingStreet);
            string city = HttpUtility.UrlEncode(vOrder.ShippingCity);
            string state = HttpUtility.UrlEncode(vOrder.ShippingState);
            string zip = HttpUtility.UrlEncode(vOrder.ShippingPostalCode);
            string baseUrl =
                HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") +
                HttpContext.Current.Request.ApplicationPath;
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl + "/";
            }
            string notifyUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/PayPalIPN.ashx");
            string returnUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/OrderCompleted.aspx?OrderID=" + vOrder.OrderID);
            string cancelUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/OrderCancelled.aspx");
            string business = HttpUtility.UrlEncode(Globals.Settings.Store.BusinessEmail);
            string itemName = HttpUtility.UrlEncode("Order #" + vOrder.OrderID);
            var url = new StringBuilder();
            url.AppendFormat(
                "{0}?cmd=_xclick&upload=1&rm=2&no_shipping=1&no_note=1&currency_code={1}&business={2}&item_number={3}&custom={3}&item_name={4}&amount={5}&shipping={6}&notify_url={7}&return={8}&cancel_return={9}&first_name={10}&last_name={11}&address1={12}&city={13}&state={14}&zip={15}",
                new object[]
                    {
                        serverUrl, Globals.Settings.Store.CurrencyCode, business, vOrder.OrderID, itemName, amount,
                        shipping, notifyUrl, returnUrl, cancelUrl, firstname, lastname, address, city, state, zip
                    });
            return url.ToString();
        }

        public static string GetProductImagesDirectory()
        {
            return Helpers.GetPhyscialPath(@"Images\Store");
        }

        public static string GetProductImageURL(string vImageName)
        {
            if (vImageName.Contains(@"Images\Store"))
            {
                return Path.Combine(Helpers.WebRoot, vImageName);
            }
            return Path.Combine(Helpers.WebRoot, Path.Combine(@"Images\Store", vImageName));
        }

        public static ShoppingCart GetShoppingCart(string vUserName)
        {
            if (!string.IsNullOrEmpty(vUserName))
            {
                return (ShoppingCart) Helpers.GetUserProfile(vUserName).GetPropertyValue("ShoppingCart");
            }
            return null;
        }

        public static ShoppingCart GetShoppingCart(ProfileBase vProfile)
        {
            return (ShoppingCart) vProfile.GetPropertyValue("ShoppingCart");
        }



        /// <summary>
        /// 获取分类logo目录
        /// </summary>
        /// <returns></returns>
        public static string GetCategoryLogoDirectory()
        {
            return Helpers.GetPhyscialPath(Helpers.Settings.Store.CategoryLogoDirectory);
        }

        /// <summary>
        /// 获取产品缩略图目录
        /// </summary>
        /// <returns></returns>
        public static string GetProductThumbDirectory()
        {
            return Helpers.GetPhyscialPath(Helpers.Settings.Store.ProductThumbDirectory.Replace('/', '\\'));
        }


        /// <summary>
        /// 获取组图照片目录
        /// </summary>
        /// <returns></returns>
        public static string GetPhotosDirectory()
        {
            return Helpers.GetPhyscialPath(Helpers.Settings.Store.PhotosDirectory.Replace('/', '\\'));
        }

        /// <summary>
        /// 获取组图缩略图目录
        /// </summary>
        /// <returns></returns>
        public static string GetPhotosThumbDirectory()
        {
            return Helpers.GetPhyscialPath(Helpers.Settings.Store.PhotosThumbDirectory.Replace('/', '\\'));
        }



        /// <summary>
        /// 获取分类logo
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetCategoryLogoUrl(string fileName)
        {
            if (fileName.Length > 0)
                return string.Format("/{0}{1}", Helpers.Settings.Store.CategoryLogoDirectory, fileName);
            else
                return "/Images/no-image-product-category-logo.png";
        }




        /// <summary>
        /// 删除分类logo
        /// </summary>
        /// <param name="logoUrl"></param>
        public static void DeleteCategoryLogo(string fileName)
        {
            string bigUrl = Path.Combine(GetCategoryLogoDirectory(), fileName);
            if (File.Exists(bigUrl))
                File.Delete(bigUrl);     //删除文件，别忘了检查一下有没有这个文件 
        }


        /// <summary>
        /// 获取产品缩略图路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetProductThumbUrl(string fileName)
        {
            if (fileName.Length > 0)
                return string.Format("/{0}{1}", Helpers.Settings.Store.ProductThumbDirectory, fileName);
            else
                return "/Images/no-image-product-thumb.png";
        }


        /// <summary>
        /// 删除产品缩略图
        /// </summary>
        /// <param name="logoUrl"></param>
        public static void DeleteProductThumb(string fileName)
        {
            string bigUrl = Path.Combine(GetProductThumbDirectory(), fileName);
            if (File.Exists(bigUrl))
                File.Delete(bigUrl);     //删除文件，别忘了检查一下有没有这个文件 
        }


        /// <summary>
        /// 获取组图路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetPhotosUrl(string fileName)
        {
            if (fileName.Length > 0)
                return string.Format("/{0}{1}", Helpers.Settings.Store.PhotosDirectory, fileName);
            else
                return "/Images/no-image-product-photo-display.png";
        }


        /// <summary>
        /// 获取组图缩略图路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetPhotosThumbUrl(string fileName)
        {
            if (fileName.Length > 0)
                return string.Format("/{0}{1}", Helpers.Settings.Store.PhotosThumbDirectory, fileName);
            else
                return "/Images/no-image-product-photo-thumb.png";
        }

        /// <summary>
        /// 删除组图
        /// </summary>
        /// <param name="logoUrl"></param>
        public static void DeletePhoto(string fileName)
        {
            string bigUrl = Path.Combine(GetPhotosDirectory(), fileName);
            if (File.Exists(bigUrl))
                File.Delete(bigUrl);     //删除文件，别忘了检查一下有没有这个文件 
        }

        /// <summary>
        /// 删除缩略图
        /// </summary>
        /// <param name="logoUrl"></param>
        public static void DeletePhotoThumb(string fileName)
        {
            string bigUrl = Path.Combine(GetPhotosThumbDirectory(), fileName);
            if (File.Exists(bigUrl))
                File.Delete(bigUrl);     //删除文件，别忘了检查一下有没有这个文件 
        }


    }
}