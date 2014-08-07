using System;
using System.Web.UI;
using System.ComponentModel;

namespace BBICMS.Web.UI
{

    [ToolboxData("<{0}:AvailabilityImage runat=server></{0}:AvailabilityImage>")]
    public class AvailabilityImage : System.Web.UI.WebControls.Image
    {

        private string _RedAlt = "Currently not available";
        [Category("The Beer House"), DefaultValue("Currently not available")]
        public string RedAlt
        {
            get { return _RedAlt; }
            set { _RedAlt = value; }
        }

        private string _YellowAlt = "Few units available";
        [Category("The Beer House"), DefaultValue("Few units available")]
        public string YellowAlt
        {
            get { return _YellowAlt; }
            set { _YellowAlt = value; }
        }

        private string _GreenAlt = "Available";
        [Category("The Beer House"), DefaultValue("Available")]
        public string GreenAlt
        {
            get { return _GreenAlt; }
            set { _GreenAlt = value; }
        }

        private string _RedImage = "~/images/lightred.gif";
        [Category("The Beer House"), DefaultValue("~/images/lightred.gif")]
        public string RedImage
        {
            get { return _RedImage; }
            set { _RedImage = value; }
        }

        private string _YellowImage = "~/images/lightyellow.gif";
        [Category("The Beer House"), DefaultValue("~/images/lightyellow.gif")]
        public string YellowImage
        {
            get { return _YellowImage; }
            set { _YellowImage = value; }
        }

        private string _GreenImage = "~/images/lightgreen.gif";
        [Category("The Beer House"), DefaultValue("~/images/lightgreen.gif")]
        public string GreenImage
        {
            get { return _GreenImage; }
            set { _GreenImage = value; }
        }

        private int _lowAvailability = 5;
        [Category("The Beer House"), DefaultValue("5")]
        public int LowAvailability
        {
            get { return _lowAvailability; }
            set { _lowAvailability = value; }
        }


        private int _value = 0;
        [Category("The Beer House"), DefaultValue("0")]
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                SetProperties();
            }
        }


        private void SetProperties()
        {
            if (_value <= 0)
            {
                base.ImageUrl = RedImage;
                base.AlternateText = RedAlt;
            }
            else if (_value <= LowAvailability)
            {
                base.ImageUrl = YellowImage;
                base.AlternateText = YellowAlt;
            }
            else
            {
                base.ImageUrl = GreenImage;
                base.AlternateText = GreenAlt;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            SetProperties();
            base.OnInit(e);
        }

    }
}