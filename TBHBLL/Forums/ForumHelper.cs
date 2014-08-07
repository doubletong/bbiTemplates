using System.Web.UI.WebControls;
using BBICMS.Forums;

namespace BBICMS
{
    public class ForumHelper
    {
        public static int GetNoofPostForUser(string vUserName)
        {
            return Helpers.GetProfileSubGroupIntegerProperty(Helpers.GetUserProfile(vUserName), "Forum", "Posts");
        }

        public static string GetPosterAvatar(string vUserName)
        {
            return Helpers.GetProfileSubGroupStringProperty(Helpers.GetUserProfile(vUserName), "Forum", "AvatarUrl");
        }

        public static string GetPosterSignature(string vUserName)
        {
            return Helpers.GetProfileSubGroupStringProperty(Helpers.GetUserProfile(vUserName), "Forum", "Signature");
        }

        public static void BindForums(ListControl vListControl, string vInstruction, int vForumId)
        {

            using (ForumsRepository lforumRpt = new ForumsRepository())
            {

                vListControl.DataSource = lforumRpt.GetActiveForums();
                vListControl.DataBind();

                vListControl.Items.Insert(0, new ListItem(vInstruction, "0"));

                if (vForumId > 0)
                {
                    vListControl.SelectedValue = vForumId.ToString();
                }

            }
        }
    }
}