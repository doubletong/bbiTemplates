using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using BBICMS.Articles;
using BBICMS;
using BBICMS.BLL.Articles;
using BBICMS.UI.Articles;

public partial class ShowArticle : ArticlePage
{
    
    #region " Properties "
    
    private Article _articleDetails;
    public Article ArticleDetails {
        get { return _articleDetails; }
        set { _articleDetails = value; }
    }
    
    public string GetTitle {
        get {
            if ((ArticleDetails == null)) {
                return "No Article Loaded";
            }
            return ArticleDetails.Title;
        }
    }
    
    public string GetArticleDate {
        get {
            if ((ArticleDetails == null)) {
                return "No Article Loaded";
            }
            return ArticleDetails.ArticleReleaseDate;
        }
    }
    
    public string GetBody {
        get {
            if ((ArticleDetails == null)) {
                return "No Article Loaded";
            }
            return ArticleDetails.Body;
        }
    }
    
    public string GetAverageRating {
        get {
            if ((ArticleDetails == null)) {
                return "0";
            }
                
            return ArticleDetails.AverageRating.ToString("0");
        }
    }
    
    #endregion
    
    #region " Comments "
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            BindArticle();
            BindComments();
            
        }
    }
    
    protected void BindComments()
    {
        
        using (CommentRepository Commentrpt = new CommentRepository()) {
            
            List<Comment> lComments = Commentrpt.GetApprovedComments(ArticleId);
            lvComments.DataSource = lComments;
            lvComments.DataBind();
            
            DataPager pagerBottom = (DataPager)lvComments.FindControl("pagerBottom");
            
            if ((pagerBottom != null) && lComments.Count <= pagerBottom.PageSize) {
                pagerBottom.Visible = false;
            }
            
        }
    }
    
    protected void lvComments_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindComments();
    }
    
    #endregion
    
    
    protected void ShowArticle_PreLoad(object sender, System.EventArgs e)
    {
        if (!IsPostBack) {
            BindArticle();
        }
    }
    
    
    protected void BindArticle()
    {
        
        using (ArticleRepository Articlerpt = new ArticleRepository()) {
            
            Article lArticle = Articlerpt.GetArticleById(ArticleId);
            ArticleRating.Tag = ArticleId.ToString( );
            Title = lArticle.Title;
            lblLocation.Visible = (lArticle.Location.Length > 0);
            lblLocation.Text = string.Format(lblLocation.Text, lArticle.Location);
            lblViews.Text = string.Format(lblViews.Text, lArticle.ViewCount);
            lblCategory.Text = lArticle.CategoryTitle;
            
            // Dim profile As ProfileBase = Helpers.GetUserProfile(lArticle.AddedBy)
            
            lblAddedBy.Text = Helpers.GetProfileRealName(Profile);
            ArticleRating.CurrentRating = GetUserRating();
            
            ArticleDetails = lArticle;
            
            lArticle.ViewCount += 1;
            Articlerpt.AddArticle(lArticle);
            
            if (this.User.Identity.IsAuthenticated && (this.User.IsInRole("Administrators") | this.User.IsInRole("Editors"))) {
                
                hlnkEdit.NavigateUrl = string.Format("~/Admin/AddEditArticle.aspx?ArticleId={0}", ArticleId);
                hlnkEdit.Visible = true;
                
                lbtnApprove.Visible = lArticle.Approved ? false : true;
                    
                lbtnDelete.Visible = true;
                
            }
            
        }
    }
    
    protected void ArticleRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        
        int userRating = GetUserRating();
        
        if (userRating == 0) {
            using (ArticleRepository Articlerpt = new ArticleRepository()) {
                Articlerpt.RateArticle(int.Parse( e.Tag), int.Parse( e.Value));
                
                HttpCookie cookie = new HttpCookie("Rating_Article" + ArticleId.ToString(), userRating.ToString());
                cookie.Expires = DateTime.Now.AddDays(Helpers.Settings.Articles.RatingLockInterval);
                    
                this.Response.Cookies.Add(cookie);
            }
            
        }
    }
    
    protected int GetUserRating()
    {
        int rating = 0;
        HttpCookie cookie = this.Request.Cookies["Rating_Article" + ArticleId];
        if ((cookie != null)) {
            rating = int.Parse(cookie.Value);
        }
        return rating;
    }
    
    protected void lbtnDelete_Click(object sender, System.EventArgs e)
    {
        using (ArticleRepository lArticlerpt = new ArticleRepository()) {
            
            lArticlerpt.DeleteArticle(ArticleId);
                
            Response.Redirect("~/ShowCategories.aspx");
        }
    }
    
    protected void lbtnApprove_Click(object sender, System.EventArgs e)
    {
        using (ArticleRepository lArticlerpt = new ArticleRepository()) {
            
            lArticlerpt.ApproveArticle(ArticleId);
            lbtnApprove.Visible = false;
        }
    }
    
}