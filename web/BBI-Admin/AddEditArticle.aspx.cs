using System;
using System.Security;
using BBICMS.Articles;
using BBICMS;
using BBICMS.BLL.Articles;
using BBICMS.UI;

partial class Admin_AddEditArticle : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated &&
                (User.IsInRole("Administrators") | User.IsInRole("Editors") | User.IsInRole("Contributors") |
                 User.IsInRole("Posters")))
            {
                txtBody.BasePath = BaseUrl + "FCKeditor/";

                if (ArticleId > 0)
                {
                    if (User.IsInRole("Administrators") | User.IsInRole("Editors"))
                    {
                        BindArticle();
                    }
                    else
                    {
                        throw new SecurityException("You are not allowed to edit existent articles!");
                    }
                }
                else
                {
                    ClearItems();
                }
            }
            else
            {
                throw new SecurityException("You are not allowed to edit existent or insert new articles!");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    protected void ClearItems()
    {
        ltlArticleID.Text = string.Empty;
        ltlAddedDate.Text = string.Empty;
        ltlAddedBy.Text = string.Empty;

        txtTitle.Text = string.Empty;
        txtAbstract.Text = string.Empty;
        txtBody.Value = string.Empty;
        ddlState.ClearSelection();
        ddlCountry.ClearSelection();
        txtCity.Text = string.Empty;
        txtReleaseDate.Text = string.Empty;
        txtExpireDate.Text = string.Empty;
        bool bApprove = (User.IsInRole("Administrators") | User.IsInRole("Editors"));
        cbApproved.Checked = bApprove;
        cbApproved.Enabled = bApprove;
        cbListed.Checked = true;
        cbCommentsEnabled.Checked = true;
        cbOnlyForMembers.Checked = false;
        ArticleHelper.BindCategoriesToListControl(ddlCategories, 0);

        ltlViewCount.Text = "0";
        ltlVotes.Text = "0";
        ltlRating.Text = "0";

        ltlUpdatedDate.Text = string.Empty;
        ltlUpdateBy.Text = string.Empty;


        ltlStatus.Text = "Create a New Article";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    protected void BindArticle()
    {
        using (ArticleRepository Articlerpt = new ArticleRepository())
        {
            Article lArticle = Articlerpt.GetArticleById(ArticleId, false, false);

            if ((lArticle != null))
            {
                ltlArticleID.Text = lArticle.ArticleID.ToString();
                ltlAddedDate.Text = lArticle.AddedDate.ToShortDateString();
                ltlAddedBy.Text = lArticle.AddedBy;

                txtTitle.Text = lArticle.Title;
                txtAbstract.Text = lArticle.Abstract;
                txtKeywords.Text = lArticle.Keywords;
                txtBody.Value = lArticle.Body;
                ddlState.SelectedValue = lArticle.State;
                ddlCountry.SelectedValue = lArticle.Country;
                txtCity.Text = lArticle.City;
                txtReleaseDate.Text = lArticle.ReleaseDate.ToString();
                txtExpireDate.Text = lArticle.ExpireDate.ToString();
                cbApproved.Checked = lArticle.Approved;
                cbListed.Checked = lArticle.Listed;
                cbCommentsEnabled.Checked = lArticle.CommentsEnabled;
                cbOnlyForMembers.Checked = lArticle.OnlyForMembers;

                ArticleHelper.BindCategoriesToListControl(ddlCategories, lArticle.CategoryId);

                ltlViewCount.Text = lArticle.ViewCount.ToString();
                ltlVotes.Text = lArticle.Votes.ToString();
                ltlRating.Text = lArticle.AverageRating.ToString();
                ltlUpdatedDate.Text = lArticle.UpdatedDate.ToString();
                ltlUpdateBy.Text = lArticle.UpdatedBy;

                ltlStatus.Text = "Edit The Article.";
            }
            else
            {
                ArticleId = 0;
                ltlStatus.Text = "Create a New Article.";
            }
        }
    }

    protected void UpdateArticle()
    {
        using (ArticleRepository Articlerpt = new ArticleRepository())
        {
            Article lArticle;

            if (ArticleId > 0)
            {
                lArticle = Articlerpt.GetArticleById(ArticleId);
            }
            else
            {
                lArticle = new Article();
            }

            lArticle.Title = Helpers.ProperCase(txtTitle.Text);
            lArticle.Abstract = txtAbstract.Text;
            lArticle.Keywords = txtKeywords.Text;
            lArticle.Body = txtBody.Value;
            lArticle.State = ddlState.SelectedValue;
            lArticle.Country = ddlCountry.SelectedValue;
            lArticle.City = txtCity.Text;
            lArticle.ReleaseDate = DateTime.Parse( txtReleaseDate.Text);
            lArticle.ExpireDate = DateTime.Parse( txtExpireDate.Text);
            lArticle.Approved = cbApproved.Checked;
            lArticle.Listed = cbListed.Checked;
            lArticle.CommentsEnabled = cbCommentsEnabled.Checked;
            lArticle.OnlyForMembers = cbOnlyForMembers.Checked;

            //Dim lCategoryId As Integer = CInt(ddlCategories.SelectedValue)
            //'http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=3985835&SiteID=1

            if ((lArticle.Category == null) | lArticle.Category.CategoryID != int.Parse(ddlCategories.SelectedValue))
            {
                //And lArticle.CategoryId > 0 Then


                lArticle.CategoryId = int.Parse(ddlCategories.SelectedValue);
            }

            lArticle.UpdatedBy = UserName;
            lArticle.UpdatedDate = DateTime.Now;

            if (lArticle.ArticleID == 0)
            {
                lArticle.Active = true;
                lArticle.AddedBy = UserName;
                lArticle.AddedDate = DateTime.Now;
            }

            lArticle = Articlerpt.AddArticle(lArticle);

            if ((lArticle != null))
            {
                IndicateUpdated(Articlerpt, "Article", ltlStatus, cmdDelete);

                string sURL = SEOFriendlyURL(Settings.Articles.URLIndicator + "/" + lArticle.Title, ".aspx");
                string sRealURL = string.Format("ShowArticle.aspx?Articleid={0}", lArticle.ArticleID);

                CommitSiteMap(lArticle.Title, sURL, sRealURL,
                              lArticle.Abstract.Substring(0,
                                                          lArticle.Abstract.Length >= 200
                                                              ? 199
                                                              : lArticle.Abstract.Length - 1),
                              string.IsNullOrEmpty(lArticle.Keywords) == false ? lArticle.Keywords : string.Empty,
                              "Article");
            }
            else
            {
                IndicateNotUpdated(Articlerpt, "Article", ltlStatus);
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UpdateArticle();
    }
}