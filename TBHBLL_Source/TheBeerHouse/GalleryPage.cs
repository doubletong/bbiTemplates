namespace TheBeerHouse
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TheBeerHouse.UI;

    public class GalleryPage : BasePage
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        [DebuggerNonUserCode]
        public GalleryPage()
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }
    }
}

