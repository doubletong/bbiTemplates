using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BBICMS.UI;

partial class AccessDenied : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblInsufficientPermissions.Visible = User.Identity.IsAuthenticated;
        lblLoginRequired.Visible = !(User.Identity.IsAuthenticated) &&
                                   string.IsNullOrEmpty(Request.QueryString["loginfailure"]);
        lblInvalidCredentials.Visible =
            !(Request.QueryString["loginfailure"] == null) &&
            Request.QueryString["loginfailure"] == "1";

        if (OpenID.IsOpenIdRequest)
        {
            OpenIdData data = OpenID.Authenticate();
            if (data.IsSuccess)
            {
                MembershipUser mu = Membership.GetUser(data.Identity);

                if ((mu == null))
                {
                    mu = Membership.GetUser(Membership.GetUserNameByEmail(data.Parameters["email"]));
                }

                if ((mu == null))
                {
                    Response.Redirect(
                        string.Format("Register.aspx?fullname={0}&email={1}&dob={2}&gender={3}&postcode&{4}&country{5}",
                                      data.Parameters["fullname"], data.Parameters["email"],
                                      data.Parameters["dob"], data.Parameters["gender"],
                                      data.Parameters["postcode"], data.Parameters["country"]));
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(mu.UserName, adLogin.RememberMeSet);
                }
            }
        }

        if (!IsPostBack)
        {
            Page.Form.DefaultFocus = adLogin.FindControl("username").ClientID;

            Page.Form.DefaultButton = adLogin.FindControl("Submit").UniqueID;
        }
    }

    
}