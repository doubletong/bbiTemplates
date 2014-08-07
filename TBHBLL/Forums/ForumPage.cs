using BBICMS;

using System.Web.UI.WebControls;
using BBICMS.UI;

namespace Web.UI.Forums
{

    public class ForumPage : BasePage
    {

        public int ForumId
        {
            get { return PrimaryKeyId("ForumId"); }
            set { ViewState["ForumId"] = value; }
        }

        public int PostId
        {
            get { return PrimaryKeyId("PostId"); }
            set { ViewState["PostId"] = value; }
        }

        public int ThreadId
        {
            get { return PrimaryKeyId("ThreadId"); }
            set { ViewState["ThreadId"] = value; }
        }

        public int QuotePostID
        {
            get { return PrimaryKeyId("QuotePostID"); }
            set { ViewState["QuotePostID"] = value; }
        }

        public bool isModerator
        {
            get { return (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || 
                this.User.IsInRole("Moderators")); }
        }

        public bool IsModeratorOrPoster(string vAddedBy)
        {
             return this.User.Identity.IsAuthenticated &&
                (this.User.Identity.Name.ToLower().Equals(vAddedBy.ToLower()) || 
                (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || 
                this.User.IsInRole("Moderators"))); 
        }

        #region " Profile Properties "

        public int GetNoofPostForUser(string vUserName)
        {
            return ForumHelper.GetNoofPostForUser(vUserName);
        }

        public string GetPosterAvatar(string vUserName)
        {
            return ForumHelper.GetPosterAvatar(vUserName);
        }

        public string GetPosterSignature(string vUserName)
        {
            return ForumHelper.GetPosterSignature(vUserName);
        }
        #endregion

        protected void BindForums(ListControl vListControl, string vInstruction)
        {
            ForumHelper.BindForums(vListControl, vInstruction, ForumId);
        }

    }
}