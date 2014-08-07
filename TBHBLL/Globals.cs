using System.Web;
using System.Web.Configuration;
using System.Web.Profile;

namespace BBICMS
{
    public sealed class Globals
    {
        public static readonly BBICMSSection Settings =
            ((BBICMSSection) WebConfigurationManager.GetSection("theBeerHouse"));

        private static ProfileBase _tBHProfile;

        public static string ThemesSelectorID = string.Empty;

        public static ProfileBase TBHProfile
        {
            get
            {
                if (null == _tBHProfile)
                {
                    _tBHProfile = HttpContext.Current.Profile;
                }
                return _tBHProfile;
            }
        }
    }
}