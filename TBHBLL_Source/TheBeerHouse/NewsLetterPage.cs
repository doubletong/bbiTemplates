namespace TheBeerHouse
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TheBeerHouse.UI;

    public class NewsLetterPage : BasePage
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        [DebuggerNonUserCode]
        public NewsLetterPage()
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        public bool CanEdit
        {
            get
            {
                return (this.User.Identity.IsAuthenticated & (this.User.IsInRole("Administrators") | this.User.IsInRole("Editors")));
            }
        }

        public int NewsLetterId
        {
            get
            {
                return this.get_PrimaryKeyId("NewsLetterId");
            }
            set
            {
                this.set_PrimaryKeyId("NewsLetterId", value);
            }
        }
    }
}

