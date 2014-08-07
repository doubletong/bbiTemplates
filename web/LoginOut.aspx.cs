using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Principal;
using BBICMS.UI;

public partial class LoginOut : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //string[] cookies = Request.Cookies.AllKeys;
        //foreach (string cookie in cookies)
        //{
        //    //BulletedList1.Items.Add("Deleting " + cookie);
        //    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        //}

        FormsAuthentication.SignOut();
        Response.Redirect("~/");
    }
}