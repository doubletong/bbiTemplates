using BBICMS.UI;

namespace Web.UI.Newsletter
{
    public class NewsLetterPage : BasePage
    {
        public int NewsLetterId
        {
            get { return PrimaryKeyId("NewsLetterId"); }
            set { ViewState["NewsLetterId"] = value; }
        }


        public bool CanEdit
        {
            get { return (User.Identity.IsAuthenticated & (User.IsInRole("Administrators") | User.IsInRole("Editors"))); }
        }
    }
}