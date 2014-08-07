using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CRMaster : MasterPage
{
    public SiteMapPath SiteMapPath
    {
        get { return (SiteMapPath) FindControl("TBHSiteMapPath"); }
    }
}