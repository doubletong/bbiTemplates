using BBICMS.UI;

partial class EditProfile : BasePage
{
    
    protected void btnUpdate_Click(object sender, System.EventArgs e)
    {
        UserProfile1.SaveProfile();
        lblFeedbackOK.Visible = true;
    }

    protected void ChangePasswordPushButton_Click(object sender, System.EventArgs e)
    {


    }

}