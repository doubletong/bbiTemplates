namespace TheBeerHouse.Web.UI
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:StateDropDownList runat=server></{0}:StateDropDownList>")]
    public class StateDropDownList : DropDownList
    {
        protected void CreateDataSource()
        {
            this.Items.Clear();
            if (this.IncludeUS & this.IncludeCanada)
            {
                this.Items.Add(new ListItem("Choose a State/Province", "0"));
            }
            else if (this.IncludeUS & !this.IncludeCanada)
            {
                this.Items.Add(new ListItem("Choose a State", "0"));
            }
            else if (!this.IncludeUS & this.IncludeCanada)
            {
                this.Items.Add(new ListItem("Choose a Province", "0"));
            }
            if (this.IncludeUS)
            {
                this.Items.Add(new ListItem("Alabama", "AL"));
                this.Items.Add(new ListItem("Alaska", "AK"));
                this.Items.Add(new ListItem("Arizona", "AZ"));
                this.Items.Add(new ListItem("Arkansas", "AR"));
                this.Items.Add(new ListItem("California", "CA"));
                this.Items.Add(new ListItem("California", "CO"));
                this.Items.Add(new ListItem("Connecticut", "CT"));
                this.Items.Add(new ListItem("D.C.", "DC"));
                this.Items.Add(new ListItem("Delaware", "DE"));
                this.Items.Add(new ListItem("Florida", "FL"));
                this.Items.Add(new ListItem("Georgia", "GA"));
                this.Items.Add(new ListItem("Hawaii", "HI"));
                this.Items.Add(new ListItem("Idaho", "ID"));
                this.Items.Add(new ListItem("Illinois", "IL"));
                this.Items.Add(new ListItem("Indiana", "IN"));
                this.Items.Add(new ListItem("Iowa", "IA"));
                this.Items.Add(new ListItem("Kansas", "KS"));
                this.Items.Add(new ListItem("Kentucky", "KY"));
                this.Items.Add(new ListItem("Louisiana", "LA"));
                this.Items.Add(new ListItem("Maine", "ME"));
                this.Items.Add(new ListItem("Maryland", "MD"));
                this.Items.Add(new ListItem("Massachusetts", "MA"));
                this.Items.Add(new ListItem("Michigan", "MI"));
                this.Items.Add(new ListItem("Minnesota", "MN"));
                this.Items.Add(new ListItem("Mississippi", "MS"));
                this.Items.Add(new ListItem("Missouri", "MO"));
                this.Items.Add(new ListItem("Montana", "MT"));
                this.Items.Add(new ListItem("Nebraska", "NE"));
                this.Items.Add(new ListItem("Nevada", "NV"));
                this.Items.Add(new ListItem("New Hampshire", "NH"));
                this.Items.Add(new ListItem("New Jersey", "NJ"));
                this.Items.Add(new ListItem("New Mexico", "NM"));
                this.Items.Add(new ListItem("New York", "NY"));
                this.Items.Add(new ListItem("North Carolina", "NC"));
                this.Items.Add(new ListItem("North Dakota", "ND"));
                this.Items.Add(new ListItem("Ohio", "OH"));
                this.Items.Add(new ListItem("Oklahoma", "OK"));
                this.Items.Add(new ListItem("Oregon", "OR"));
                this.Items.Add(new ListItem("Pennsylvania", "PA"));
                this.Items.Add(new ListItem("Rhode Island", "RI"));
                this.Items.Add(new ListItem("South Carolina", "SC"));
                this.Items.Add(new ListItem("South Dakota", "SD"));
                this.Items.Add(new ListItem("Tennessee", "TN"));
                this.Items.Add(new ListItem("Texas", "TX"));
                this.Items.Add(new ListItem("Utah", "UT"));
                this.Items.Add(new ListItem("Vermont", "VT"));
                this.Items.Add(new ListItem("Virginia", "VA"));
                this.Items.Add(new ListItem("Washington", "WA"));
                this.Items.Add(new ListItem("West Virginia", "WV"));
                this.Items.Add(new ListItem("Wisconsin", "WI"));
                this.Items.Add(new ListItem("Wyoming", "WY"));
            }
            if (this.IncludeCanada)
            {
                this.Items.Add(new ListItem("British Columbia", "BC"));
                this.Items.Add(new ListItem("Manitoba", "MB"));
                this.Items.Add(new ListItem("New Brunswick", "NB"));
                this.Items.Add(new ListItem("Newfoundland and Labrador", "NL"));
                this.Items.Add(new ListItem("Northwest Territories", "NT"));
                this.Items.Add(new ListItem("Nova Scotia", "NS"));
                this.Items.Add(new ListItem("Nunavut", "NU"));
                this.Items.Add(new ListItem("Ontario", "ON"));
                this.Items.Add(new ListItem("Prince Edward Island", "PE"));
                this.Items.Add(new ListItem("Quebec", "QC"));
                this.Items.Add(new ListItem("Saskatchewan", "SK"));
                this.Items.Add(new ListItem("Yukon Territories", "YT"));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.DataTextField = "Text";
            this.DataValueField = "Value";
            this.CreateDataSource();
            base.OnInit(e);
        }

        [DefaultValue("True"), Category("Control Flags")]
        public bool IncludeCanada
        {
            get
            {
                if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(this.ViewState["Canada"])))
                {
                    return Conversions.ToBoolean(this.ViewState["Canada"]);
                }
                return true;
            }
            set
            {
                this.ViewState["Canada"] = value;
            }
        }

        [DefaultValue("True"), Category("Control Flags")]
        public bool IncludeUS
        {
            get
            {
                if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(this.ViewState["US"])))
                {
                    return Conversions.ToBoolean(this.ViewState["US"]);
                }
                return true;
            }
            set
            {
                this.ViewState["US"] = value;
            }
        }
    }
}

