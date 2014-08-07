namespace TheBeerHouse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:AvailabilityImage runat=server></{0}:AvailabilityImage>")]
    public class AvailabilityImage : Image
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private string _GreenAlt;
        private string _GreenImage;
        private int _lowAvailability;
        private string _RedAlt;
        private string _RedImage;
        private int _value;
        private string _YellowAlt;
        private string _YellowImage;

        public AvailabilityImage()
        {
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
            this._RedAlt = "Currently not available";
            this._YellowAlt = "Few units available";
            this._GreenAlt = "Available";
            this._RedImage = "~/images/lightred.gif";
            this._YellowImage = "~/images/lightyellow.gif";
            this._GreenImage = "~/images/lightgreen.gif";
            this._lowAvailability = 5;
            this._value = 0;
        }

        protected override void OnInit(EventArgs e)
        {
            this.SetProperties();
            base.OnInit(e);
        }

        private void SetProperties()
        {
            if (this._value <= 0)
            {
                base.ImageUrl = this.RedImage;
                base.AlternateText = this.RedAlt;
            }
            else if (this._value <= this.LowAvailability)
            {
                base.ImageUrl = this.YellowImage;
                base.AlternateText = this.YellowAlt;
            }
            else
            {
                base.ImageUrl = this.GreenImage;
                base.AlternateText = this.GreenAlt;
            }
        }

        [DefaultValue("Available"), Category("The Beer House")]
        public string GreenAlt
        {
            get
            {
                return this._GreenAlt;
            }
            set
            {
                this._GreenAlt = value;
            }
        }

        [Category("The Beer House"), DefaultValue("~/images/lightgreen.gif")]
        public string GreenImage
        {
            get
            {
                return this._GreenImage;
            }
            set
            {
                this._GreenImage = value;
            }
        }

        [DefaultValue("5"), Category("The Beer House")]
        public int LowAvailability
        {
            get
            {
                return this._lowAvailability;
            }
            set
            {
                this._lowAvailability = value;
            }
        }

        [Category("The Beer House"), DefaultValue("Currently not available")]
        public string RedAlt
        {
            get
            {
                return this._RedAlt;
            }
            set
            {
                this._RedAlt = value;
            }
        }

        [DefaultValue("~/images/lightred.gif"), Category("The Beer House")]
        public string RedImage
        {
            get
            {
                return this._RedImage;
            }
            set
            {
                this._RedImage = value;
            }
        }

        [DefaultValue("0"), Category("The Beer House")]
        public int Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                this.SetProperties();
            }
        }

        [DefaultValue("Few units available"), Category("The Beer House")]
        public string YellowAlt
        {
            get
            {
                return this._YellowAlt;
            }
            set
            {
                this._YellowAlt = value;
            }
        }

        [Category("The Beer House"), DefaultValue("~/images/lightyellow.gif")]
        public string YellowImage
        {
            get
            {
                return this._YellowImage;
            }
            set
            {
                this._YellowImage = value;
            }
        }
    }
}

