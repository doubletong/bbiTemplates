
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class TBHMain : System.Web.UI.MasterPage
{
    public HtmlGenericControl pageBodyTag
    {
        get { return this.pageBody; }
    }


    private bool _enablePersonalization = false;

    public bool EnablePersonalization
    {
        get { return _enablePersonalization; }
        set { _enablePersonalization = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!this.Page.User.Identity.IsAuthenticated)
        {
            (LoginView1.FindControl("lnkLogin") as HyperLink).NavigateUrl = "~/AccessDenied.aspx?ReturnUrl=" + Server.UrlEncode(HttpContext.Current.Request.RawUrl);



            // 新浪开始
            //OAuth oauth = new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"], ConfigurationManager.AppSettings["CallbackUrl"]);
            //string weiboAuthUrl = oauth.GetAuthorizeURL(ResponseType.Code, null, DisplayType.Default);
            //(LoginView1.FindControl("hlAuthUrl") as HyperLink).NavigateUrl = weiboAuthUrl;

            // QQ开始
            //var context = new QzoneContext();
            //string state = Guid.NewGuid().ToString().Replace("-", "");
            //string scope = "get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
            //var authenticationUrl = context.GetAuthorizationUrl(state, scope);
            //request token, request token secret 需要保存起来
            //在demo演示中，直接保存在全局变量中.真实情况需要网站自己处理
            //Session["requeststate"] = state;
            //(LoginView1.FindControl("hlQQAuthUrl") as HyperLink).NavigateUrl = authenticationUrl;
        }
        else
        {
            ProfileCommon profile = this.Profile;
            if (this.Page.User.Identity.Name.Length > 0)
                profile = this.Profile.GetProfile(this.Page.User.Identity.Name);

            if (!string.IsNullOrEmpty(profile.FullName))
            {
                (LoginView1.FindControl("ltlUserName") as Literal).Text = profile.FullName;
            }
            else
            {
                (LoginView1.FindControl("ltlUserName") as Literal).Text = this.Page.User.Identity.Name;
            }



            //if (!Page.User.IsInRole("系统管理员"))
            //{

            //    int mscount = GenaiSky.MessageHelper.GetNoReadMessageCount(Page.User.Identity.Name);
            //    if (mscount > 0)
            //    {
            //        (LoginView1.FindControl("lblMsgCount") as Label).Text = mscount.ToString();
            //    }
            //    else
            //    {
            //        (LoginView1.FindControl("lblMsgCount") as Label).Visible = false;
            //    }
            //}
        }


    }

    protected void RedirectToCreateUser(OpenIdData data)
    {
        Response.Redirect(
            string.Format("Register.aspx?fullname={0}&email={1}&dob={2}&gender={3}&postcode&{4}&country{5}",
                          data.Parameters["fullname"], data.Parameters["email"], data.Parameters["dob"],
                          data.Parameters["gender"], data.Parameters["postcode"], data.Parameters["country"]));
    }


    protected void btnLoginOpenId_Click(object sender, System.EventArgs e)
    {
        //bool success = OpenID.Login(txtOpenId.Text, "email,fullname", "country,language,nickname,dob,gender,postcode");

        //if (!success)
        //{
        //    Response.Write("The OpenID is not valid");
        //}
    }
}