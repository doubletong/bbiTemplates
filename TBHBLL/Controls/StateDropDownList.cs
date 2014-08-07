using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BBICMS.Web.UI
{
    [ToolboxData("<{0}:StateDropDownList runat=server></{0}:StateDropDownList>")]
    public class StateDropDownList : DropDownList
    {
        [DefaultValue("True"), Category("Control Flags")]
        public bool IncludeCanada
        {
            get
            {
                if (null != ViewState["Canada"])
                {
                    return bool.Parse(ViewState["Canada"].ToString());
                }
                return true;
            }
            set { ViewState["Canada"] = value; }
        }

        [DefaultValue("True"), Category("Control Flags")]
        public bool IncludeUS
        {
            get
            {
                if (null != ViewState["US"])
                {
                    return bool.Parse(ViewState["US"].ToString());
                }
                return true;
            }
            set { ViewState["US"] = value; }
        }

        protected void CreateDataSource()
        {
            Items.Clear();
            if (IncludeUS & IncludeCanada)
            {
                Items.Add(new ListItem("Choose a State/Province", "0"));
            }
            else if (IncludeUS & !IncludeCanada)
            {
                Items.Add(new ListItem("Choose a State", "0"));
            }
            else if (!IncludeUS & IncludeCanada)
            {
                Items.Add(new ListItem("Choose a Province", "0"));
            }
            if (IncludeUS)
            {
                Items.Add(new ListItem("Alabama", "AL"));
                Items.Add(new ListItem("Alaska", "AK"));
                Items.Add(new ListItem("Arizona", "AZ"));
                Items.Add(new ListItem("Arkansas", "AR"));
                Items.Add(new ListItem("California", "CA"));
                Items.Add(new ListItem("California", "CO"));
                Items.Add(new ListItem("Connecticut", "CT"));
                Items.Add(new ListItem("D.C.", "DC"));
                Items.Add(new ListItem("Delaware", "DE"));
                Items.Add(new ListItem("Florida", "FL"));
                Items.Add(new ListItem("Georgia", "GA"));
                Items.Add(new ListItem("Hawaii", "HI"));
                Items.Add(new ListItem("Idaho", "ID"));
                Items.Add(new ListItem("Illinois", "IL"));
                Items.Add(new ListItem("Indiana", "IN"));
                Items.Add(new ListItem("Iowa", "IA"));
                Items.Add(new ListItem("Kansas", "KS"));
                Items.Add(new ListItem("Kentucky", "KY"));
                Items.Add(new ListItem("Louisiana", "LA"));
                Items.Add(new ListItem("Maine", "ME"));
                Items.Add(new ListItem("Maryland", "MD"));
                Items.Add(new ListItem("Massachusetts", "MA"));
                Items.Add(new ListItem("Michigan", "MI"));
                Items.Add(new ListItem("Minnesota", "MN"));
                Items.Add(new ListItem("Mississippi", "MS"));
                Items.Add(new ListItem("Missouri", "MO"));
                Items.Add(new ListItem("Montana", "MT"));
                Items.Add(new ListItem("Nebraska", "NE"));
                Items.Add(new ListItem("Nevada", "NV"));
                Items.Add(new ListItem("New Hampshire", "NH"));
                Items.Add(new ListItem("New Jersey", "NJ"));
                Items.Add(new ListItem("New Mexico", "NM"));
                Items.Add(new ListItem("New York", "NY"));
                Items.Add(new ListItem("North Carolina", "NC"));
                Items.Add(new ListItem("North Dakota", "ND"));
                Items.Add(new ListItem("Ohio", "OH"));
                Items.Add(new ListItem("Oklahoma", "OK"));
                Items.Add(new ListItem("Oregon", "OR"));
                Items.Add(new ListItem("Pennsylvania", "PA"));
                Items.Add(new ListItem("Rhode Island", "RI"));
                Items.Add(new ListItem("South Carolina", "SC"));
                Items.Add(new ListItem("South Dakota", "SD"));
                Items.Add(new ListItem("Tennessee", "TN"));
                Items.Add(new ListItem("Texas", "TX"));
                Items.Add(new ListItem("Utah", "UT"));
                Items.Add(new ListItem("Vermont", "VT"));
                Items.Add(new ListItem("Virginia", "VA"));
                Items.Add(new ListItem("Washington", "WA"));
                Items.Add(new ListItem("West Virginia", "WV"));
                Items.Add(new ListItem("Wisconsin", "WI"));
                Items.Add(new ListItem("Wyoming", "WY"));
            }
            if (IncludeCanada)
            {
                Items.Add(new ListItem("British Columbia", "BC"));
                Items.Add(new ListItem("Manitoba", "MB"));
                Items.Add(new ListItem("New Brunswick", "NB"));
                Items.Add(new ListItem("Newfoundland and Labrador", "NL"));
                Items.Add(new ListItem("Northwest Territories", "NT"));
                Items.Add(new ListItem("Nova Scotia", "NS"));
                Items.Add(new ListItem("Nunavut", "NU"));
                Items.Add(new ListItem("Ontario", "ON"));
                Items.Add(new ListItem("Prince Edward Island", "PE"));
                Items.Add(new ListItem("Quebec", "QC"));
                Items.Add(new ListItem("Saskatchewan", "SK"));
                Items.Add(new ListItem("Yukon Territories", "YT"));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            DataTextField = "Text";
            DataValueField = "Value";
            CreateDataSource();
            base.OnInit(e);
        }
    }
}