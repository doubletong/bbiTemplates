using BBICMS.UI;

public partial class Register : BasePage
{
    
    #region " Properties "
    
    public string FullName {
        get { return Request["fullname"]; }
    }
    
    public string EMail {
        get { return this.Request.QueryString["Email"]; }
    }
    
    public string DOB {
        get { return Request["dob"]; }
    }
    
    public string Gender {
        get { return Request["gender"]; }
    }
    
    public string PostCode {
        get { return Request["postcode"]; }
    }
    public string Country {
        get { return Request["country"]; }
    }
    
    
    #endregion
    
    protected void CreateUserWizard1_FinishButtonClick(object sender, System.Web.UI.WebControls.WizardNavigationEventArgs e)
    {
        UserProfile1.SaveProfile();
    }
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!this.IsPostBack && !string.IsNullOrEmpty(this.Request.QueryString["Email"])) {
            CreateUserWizard1.DataBind();
        }
    }
}