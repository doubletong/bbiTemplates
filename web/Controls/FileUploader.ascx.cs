using System;
using System.IO;
using System.Security;
using BBICMS.UI;

partial class Controls_FileUploader : System.Web.UI.UserControl
{
    
    protected void btnUpload_Click(object sender, System.EventArgs e)
    {
        if (!filUpload.HasFile) {
            try {
                // if not already present, create a directory
                // names /uploads/{CurrentUserName}
                string dirUrl = ((BasePage)this.Page).BaseUrl + "Uploads/" + this.Page.User.Identity.Name;
                string dirPath = Server.MapPath(dirUrl);
                if (!Directory.Exists(dirPath)) {
                    Directory.CreateDirectory(dirPath);
                }
                // save the file under the users's personal folder
                string fileUrl = dirUrl + "/" + Path.GetFileName(filUpload.PostedFile.FileName);
                filUpload.PostedFile.SaveAs(Server.MapPath(fileUrl));
                
                lblFeedbackOK.Visible = true;
                lblFeedbackOK.Text = "File successfully uploaded: " + fileUrl;
            }
            catch (Exception ex) {
                lblFeedbackKO.Visible = true;
                lblFeedbackKO.Text = ex.Message;
            }
        }
    }
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // this control can only work for authenticated users
        if (!this.Page.User.Identity.IsAuthenticated) {
            throw new SecurityException("Anonymous users cannot upload files.");

            lblFeedbackKO.Visible = false;
            lblFeedbackOK.Visible = false;
        }
    }
    
}