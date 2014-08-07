using System;
using System.IO;
using BBICMS;
using BBICMS.BLL.Store;
using BBICMS.Store;
using BBICMS.UI;

partial class Admin_AddEditDepartment : AdminPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = string.Format(this.Title, Helpers.Settings.SiteName);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (DepartmentId > 0)
            {
                BindDepartment();
            }
            else
            {
                ClearItems();
            }
        }
    }

    protected void ClearItems()
    {
        // txtDepartmentID.Text = String.Empty
        ltlAddedDate.Text = string.Empty;
        txtTitle.Text = string.Empty;
        txtImportance.Text = string.Empty;
        txtDescription.Text = string.Empty;
        iThumbnail.Visible = false;
        ltlUpdatedDate.Text = string.Empty;

        plnote.Visible = false;
        ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "创建商品分类";
    }

    protected void BindDepartment()
    {
        plnote.Visible = true;
        using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
        {
            Department lDepartment = lDepartmentrpt.GetDepartmentById(DepartmentId);

            if ((lDepartment != null))
            {
                ltlDepartmentID.Text = lDepartment.DepartmentID.ToString();
                ltlAddedDate.Text = lDepartment.AddedDate.ToShortDateString();
                txtTitle.Text = lDepartment.Title;
                txtImportance.Text = lDepartment.Importance.ToString();
                txtDescription.Text = lDepartment.Description;

                if (string.IsNullOrEmpty(lDepartment.ImageUrl) == false)
                {
                    iThumbnail.ImageUrl = lDepartment.ImageUrl;
                    iThumbnail.Visible = true;
                }
                else
                {
                    iThumbnail.Visible = false;
                }

                ActiveCheckBox.Checked = lDepartment.Active;
                ltlAddedBy.Text = lDepartment.AddedBy;
                ltlAddedDate.Text = lDepartment.AddedDate.ToShortDateString();
                ltlUpdatedDate.Text = lDepartment.UpdatedDate.ToShortDateString();
                ltlUpdatedBy.Text = lDepartment.UpdatedBy;

                ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "修改商品分类";
            }
            else
            {
                DepartmentId = 0;
                ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "创建商品分类";
            }
        }
    }

    protected void ManageDepartments()
    {
        Response.Redirect("ManageDepartment.aspx");
    }

    protected void UpdateDepartment()
    {
        using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
        {
            Department lDepartment = new Department();

            if (DepartmentId > 0)
            {
                lDepartment = lDepartmentrpt.GetDepartmentById(DepartmentId);
            }
            else
            {
                lDepartment = new Department();
            }

            lDepartment.DepartmentID = DepartmentId;
          
            lDepartment.Title = txtTitle.Text;
            lDepartment.Importance = int.Parse(txtImportance.Text);
            lDepartment.Description = txtDescription.Text;
          

            if (fPicture.HasFile)
            {
                string lFilePath = Path.Combine(Helpers.GetPhyscialPath("Store\\Departments"),
                                                Helpers.FormatSpacesForURL(lDepartment.Title) +
                                                Path.GetExtension(fPicture.FileName));

                if (File.Exists(lFilePath))
                {
                    File.Delete(lFilePath);
                }

                fPicture.SaveAs(lFilePath);


                lDepartment.ImageUrl = Path.Combine("~/Store/Departments",
                                                    Helpers.FormatSpacesForURL(lDepartment.Title) +
                                                    Path.GetExtension(fPicture.FileName));
            }

            lDepartment.UpdatedBy = UserName;
            lDepartment.UpdatedDate = DateTime.Now;

            if (lDepartment.DepartmentID > 0)
            {
                if ((lDepartmentrpt.AddDepartment(lDepartment) != null))
                {
                    IndicateUpdated(lDepartmentrpt, "Department", ltlStatus, cmdDelete);
                }
                else
                {
                    IndicateNotUpdated(lDepartmentrpt, "Department", ltlStatus);
                }
            }
            else
            {
                lDepartment.Active = ActiveCheckBox.Checked;
                lDepartment.AddedBy = UserName;
                lDepartment.AddedDate = DateTime.Now;
                if ((lDepartmentrpt.AddDepartment(lDepartment) != null))
                {
                    IndicateUpdated(lDepartmentrpt, "Department", ltlStatus, cmdDelete);
                }
                else
                {
                    IndicateNotUpdated(lDepartmentrpt, "Department", ltlStatus);
                }
            }
        }
    }

 

    protected void DeleteDepartment()
    {
        using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
        {
            lDepartmentrpt.DeleteDepartment(lDepartmentrpt.GetDepartmentById(DepartmentId));
        }
        Response.Redirect("ManageDepartment.aspx");
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        DeleteDepartment();
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        UpdateDepartment();
    }

   
}