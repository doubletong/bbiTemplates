namespace TheBeerHouse
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Profile;
    using TheBeerHouse.BLL.Store;

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
            string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath;
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl + "/";
            }
            string notifyUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/PayPalIPN.ashx");
            string returnUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/OrderCompleted.aspx?OrderID=" + vOrder.OrderID.ToString());
            string cancelUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/OrderCancelled.aspx");
            string business = HttpUtility.UrlEncode(Globals.Settings.Store.BusinessEmail);
            string itemName = HttpUtility.UrlEncode("Order #" + vOrder.OrderID.ToString());
            StringBuilder url = new StringBuilder();
            url.AppendFormat("{0}?cmd=_xclick&upload=1&rm=2&no_shipping=1&no_note=1&currency_code={1}&business={2}&item_number={3}&custom={3}&item_name={4}&amount={5}&shipping={6}&notify_url={7}&return={8}&cancel_return={9}&first_name={10}&last_name={11}&address1={12}&city={13}&state={14}&zip={15}", new object[] { serverUrl, Globals.Settings.Store.CurrencyCode, business, vOrder.OrderID, itemName, amount, shipping, notifyUrl, returnUrl, cancelUrl, firstname, lastname, address, city, state, zip });
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
    }
}

