using System;
using System.Security;
using BBICMS.Articles;
using BBICMS.BLL.Articles;
using BBICMS.UI;

partial class Admin_AddEditCategory : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated &&
                (User.IsInRole("Administrators") | User.IsInRole("Editors") | User.IsInRole("Contributors") |
                 User.IsInRole("Posters")))
            {
                if (CategoryId > 0)
                {
                    BindCategory();
                }
                else
                {
                    ClearItems();
                }
            }
            else
            {
                throw new SecurityException("You are not allowed to edit existing categories!");
            }
        }
    }

    protected void ClearItems()
    {
        ltlCategoryID.Text = string.Empty;
        ltlAddedDate.Text = string.Empty;
        txtTitle.Text = string.Empty;
        txtImportance.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtImageUrl.Text = string.Empty;
        ltlUpdatedDate.Text = string.Empty;
        iLogo.Visible = false;


        ltlStatus.Text = "Create a New Category.";
    }

    protected void BindCategory()
    {
        using (CategoryRepository Categoryrpt = new CategoryRepository())
        {
            Category lCategory = Categoryrpt.GetCategoryById(CategoryId);

            if ((lCategory != null))
            {
                ltlCategoryID.Text = lCategory.CategoryID.ToString();
                ltlAddedDate.Text = lCategory.AddedDate.ToString();
                ltlAddedBy.Text = lCategory.AddedBy;
                txtTitle.Text = lCategory.Title;
                txtImportance.Text = lCategory.Importance.ToString();
                txtDescription.Text = lCategory.Description;

                if (string.IsNullOrEmpty(lCategory.ImageUrl) == false)
                {
                    txtImageUrl.Text = lCategory.ImageUrl;
                    iLogo.ImageUrl = lCategory.ImageUrl;
                }
                else
                {
                    iLogo.Visible = false;
                }

                cbActive.Checked = lCategory.Active;
                ltlUpdatedDate.Text = lCategory.UpdatedDate.ToShortDateString();
                ltlUpdatedBy.Text = lCategory.UpdatedBy;

                ltlStatus.Text = string.Format("Edit The Category - {0}", lCategory.Title);
            }
            else
            {
                CategoryId = 0;
                ltlStatus.Text = "Create a New Category";
                cmdUpdate.Text = "Insert";
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageCategories.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UpdateCategory();
    }

    protected void UpdateCategory()
    {
        using (CategoryRepository Categoryrpt = new CategoryRepository())
        {
            Category lCategory = new Category();

            if (CategoryId > 0)
            {
                lCategory = Categoryrpt.GetCategoryById(CategoryId);
            }
            else
            {
                lCategory = new Category();
            }

            lCategory.Title = txtTitle.Text;
            lCategory.Importance = int.Parse(txtImportance.Text);
            lCategory.Description = txtDescription.Text;

            lCategory.ImageUrl = GetItemImage(fuImageURL, txtImageUrl);
            lCategory.Active = cbActive.Checked;

            lCategory.UpdatedBy = UserName;
            lCategory.UpdatedDate = DateTime.Now;

            if (lCategory.CategoryID == 0)
            {
                lCategory.Active = true;
                lCategory.AddedBy = UserName;
                lCategory.AddedDate = DateTime.Now;
            }

            if ((Categoryrpt.AddCategory(lCategory) != null))
            {
                IndicateUpdated(Categoryrpt, "Category", ltlStatus, cmdDelete);

                cmdUpdate.Text = "Update";
                IndicateUpdated(Categoryrpt, "Category", ltlStatus, cmdDelete);
                string sURL = SEOFriendlyURL(Settings.Articles.CategoryUrlIndicator + "/" + lCategory.Title, ".aspx");
                string sRealURL = string.Format("BrowseArticles.aspx?Categoryid={0}", lCategory.CategoryID);


                CommitSiteMap(lCategory.Title, sURL, sRealURL,
                              lCategory.Description.Substring(0,
                                                              lCategory.Description.Length >= 200
                                                                  ? 199
                                                                  : lCategory.Description.Length - 1),
                              string.IsNullOrEmpty(lCategory.Title) == false ? lCategory.Title : string.Empty,
                              "Category");
            }
            else
            {
                IndicateNotUpdated(Categoryrpt, "Category", ltlStatus);
            }
        }
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        using (CategoryRepository Categoryrpt = new CategoryRepository())
        {
            if (Categoryrpt.DeleteCategory(CategoryId))
            {
                ltlStatus.Text = "The Category was Deleted.";
            }
            else
            {
                ltlStatus.Text = "Sorry the Category was not Deleted.";
            }
        }
    }
}