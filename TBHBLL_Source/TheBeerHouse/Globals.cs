namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using System;
    using System.Web;
    using System.Web.Profile;

    public sealed class Globals
    {
        private static ProfileBase _tBHProfile;
        public static readonly TheBeerHouseSection Settings = ((TheBeerHouseSection) WebConfigurationManager.GetSection("theBeerHouse"));
        public static string ThemesSelectorID = string.Empty;

        public static ProfileBase TBHProfile
        {
            get
            {
                if (Information.IsNothing(_tBHProfile))
                {
                    _tBHProfile = HttpContext.Current.Profile;
                }
                return _tBHProfile;
            }
        }
    }
}

