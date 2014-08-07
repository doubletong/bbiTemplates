namespace BBICMS.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class RoleAdminPage : AdminPage
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        [DebuggerNonUserCode]
        public RoleAdminPage()
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        protected int RoleId
        {
            get
            {
                return this.PrimaryKeyId("RoleId");
            }
            set
            {
                ViewState["RoledId"] =  value;
            }
        }

        protected string RoleName
        {
            get
            {
                return this.PrimaryKeyIdAsString( "RoleName");
            }
            set
            {
                ViewState["RoleName"] = value;
            }
        }
    }
}

