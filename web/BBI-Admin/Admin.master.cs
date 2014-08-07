using System.Web.UI.HtmlControls;
using BBICMS;


partial class Admin_Admin : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {            
                
            BindNavItems();
            
        }
    }
    
    public HtmlGenericControl pageBodyTag {
        get { return this.pageBody; }
    }
    
    protected void BindNavItems()
    {
     


    }
    
}