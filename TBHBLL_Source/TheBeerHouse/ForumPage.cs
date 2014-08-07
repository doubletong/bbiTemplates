namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.UI.WebControls;
    using TheBeerHouse.BLL.Forums;
    using TheBeerHouse.UI;

    public class ForumPage : BasePage
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        [DebuggerNonUserCode]
        public ForumPage()
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        protected void BindForums(ListControl vListControl, string vInstruction)
        {
            using (ForumsRepository lforumRpt = new ForumsRepository())
            {
                vListControl.DataSource = lforumRpt.GetActiveForums();
                vListControl.DataBind();
                vListControl.Items.Insert(0, new ListItem(vInstruction, "0"));
                if (this.ForumId > 0)
                {
                    vListControl.SelectedValue = Conversions.ToString(this.ForumId);
                }
            }
        }

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

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int ForumId
        {
            get
            {
                return this.get_PrimaryKeyId("ForumId");
            }
            set
            {
                this.set_PrimaryKeyId("ForumId", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool isModerator
        {
            get
            {
                return (((this.User.Identity.IsAuthenticated & this.User.IsInRole("Administrators")) | this.User.IsInRole("Editors")) | this.User.IsInRole("Moderators"));
            }
        }

        public bool this[string vAddedBy]
        {
            get
            {
                return (this.User.Identity.IsAuthenticated & (this.User.Identity.Name.ToLower().Equals(vAddedBy.ToLower()) | ((this.User.IsInRole("Administrators") | this.User.IsInRole("Editors")) | this.User.IsInRole("Moderators"))));
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int PostId
        {
            get
            {
                return this.get_PrimaryKeyId("PostId");
            }
            set
            {
                this.set_PrimaryKeyId("PostId", value);
            }
        }

        public int QuotePostID
        {
            get
            {
                return this.get_PrimaryKeyId("QuotePostID");
            }
            set
            {
                this.set_PrimaryKeyId("QuotePostID", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int ThreadId
        {
            get
            {
                return this.get_PrimaryKeyId("ThreadId");
            }
            set
            {
                this.set_PrimaryKeyId("ThreadId", value);
            }
        }
    }
}

