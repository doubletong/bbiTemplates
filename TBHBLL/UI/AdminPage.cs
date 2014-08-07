using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using BBICMS.BLL;
using BBICMS.Store;

namespace BBICMS.UI
{

    public class AdminPage : BasePage
    {
        public GalleryElement lGallerySettings;
        public StoreElement lStoreSettings;

        public AdminPage()
        {
            base.PreInit += new EventHandler(this.Page_PreInit);
            this.lGallerySettings = Helpers.Settings.Gallery;
            this.lStoreSettings = Helpers.Settings.Store;
        }

        public void BindDepartmentsToListControl(ListControl vDepartmentListControl, bool bAddInstruction)
        {
            using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
            {
                vDepartmentListControl.DataValueField = "DepartmentId";
                vDepartmentListControl.DataTextField = "Title";
                vDepartmentListControl.DataSource = lDepartmentrpt.GetDepartments();
                vDepartmentListControl.DataBind();
                if (bAddInstruction)
                {
                    vDepartmentListControl.Items.Insert(0, new ListItem("All Departments", "0"));
                }
                vDepartmentListControl.SelectedValue = this.DepartmentId.ToString();
            }
        }

        public void BindOrderStatusesToListControl(ListControl vOrderStatusListControl, bool bAddInstruction)
        {

            using (OrderStatusesRepository lOrderStatusrpt = new OrderStatusesRepository())
            {

                vOrderStatusListControl.DataValueField = "OrderStatusId";
                vOrderStatusListControl.DataTextField = "Title";

                vOrderStatusListControl.DataSource = lOrderStatusrpt.GetOrderStatuses();
                vOrderStatusListControl.DataBind();

                vOrderStatusListControl.Items.Insert(0, new ListItem("All Order Statuses", "0"));

                vOrderStatusListControl.SelectedValue = OrderStatusId.ToString();

            }
        }

        protected string GetItemImage(FileUpload vFileUpload, TextBox txtImageURL)
        {
            if (vFileUpload.HasFile)
            {
                vFileUpload.SaveAs(Path.Combine(Path.Combine(this.Request.PhysicalApplicationPath, "images"), vFileUpload.FileName));
                return Path.Combine("~/images", vFileUpload.FileName);
            }
            if (!string.IsNullOrEmpty(txtImageURL.Text))
            {
                return txtImageURL.Text;
            }
            return "~/Images/Beers.gif";
        }

        public void IndicateNotUpdated(BaseRepository vRepository, string vEntityName, Literal ltlStatus)
        {

            ltlStatus.Text = string.Empty;
            if (vRepository.ActiveExceptions.Count > 0)
            {
                foreach (KeyValuePair<string, Exception> kv in vRepository.ActiveExceptions)
                {
                    ltlStatus.Text += ((Exception)kv.Value).Message + "<BR/>";
                }
            }
            else
            {
                ltlStatus.Text = string.Format("The {0} Has Not Been Updated", vEntityName);

            }
        }

        public void IndicateUpdated(BaseRepository vRepository, string vEntityName, Literal ltlStatus, WebControl cmdDelete)
        {
            ltlStatus.Text = string.Format("The {0} Has Been Updated", vEntityName);
            if ((cmdDelete != null))
            {
                cmdDelete.Visible = true;

            }
        }

        private void Page_PreInit(object sender, EventArgs e)
        {
            this.MoveHiddenFields = false;
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int AlbumId
        {
            get
            {
                return this.PrimaryKeyId("AlbumId");
            }
            set
            {
                this.ViewState["AlbumId"] = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int ArticleId
        {
            get
            {
                return this.PrimaryKeyId("ArticleId");
            }
            set
            {
                this.ViewState["ArticleId"] = value;
            }
        }

        public int CategoryId
        {
            get
            {
                return this.PrimaryKeyId("CategoryId");
            }
            set
            {
                this.ViewState["CategoryId"] = value;
            }
        }

        public int CommentId
        {
            get
            {
                return this.PrimaryKeyId("CommentId");
            }
            set
            {
                this.ViewState["CommentId"] = value;
            }
        }

        public int DepartmentId
        {
            get
            {
                return this.PrimaryKeyId("departmentId");
            }
            set
            {
                this.ViewState["departmentId"] = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int EventId
        {
            get
            {
                return this.PrimaryKeyId("EventId");
            }
            set
            {
                this.ViewState["EventId"] = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int ForumId
        {
            get
            {
                return this.PrimaryKeyId("ForumId");
            }
            set
            {
                this.ViewState["ForumId"] = value;
            }
        }

        public int NewsLetterId
        {
            get
            {
                return this.PrimaryKeyId("NewsLetterId");
            }
            set
            {
                this.ViewState["NewsLetterId"] = value;
            }
        }

        public int OrderId
        {
            get
            {
                return this.PrimaryKeyId("OrderId");
            }
            set
            {
                this.ViewState["OrderId"] = value;
            }
        }

        public int OrderStatusId
        {
            get
            {
                return this.PrimaryKeyId("OrderStatusId");
            }
            set
            {
                this.ViewState["OrderStatusId"] = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int PhotoId
        {
            get
            {
                return this.PrimaryKeyId("PhotoId");
            }
            set
            {
                this.ViewState["PhotoId"] = value;
            }
        }

        public int PictureId
        {
            get
            {
                return this.PrimaryKeyId("PictureId");
            }
            set
            {
                this.ViewState["PictureId"] = value;
            }
        }

        public int PollId
        {
            get
            {
                return this.PrimaryKeyId("PollId");
            }
            set
            {
                this.ViewState["PollId"] = value;
            }
        }

        public int PollOptionId
        {
            get
            {
                return this.PrimaryKeyId("PollOptionId");
            }
            set
            {
                this.ViewState["PollOptionId"] = value;
            }
        }

        public int ProductId
        {
            get
            {
                return this.PrimaryKeyId("ProductId");
            }
            set
            {
                this.ViewState["ProductId"] = value;
            }
        }

        public int ShippingMethodId
        {
            get
            {
                return this.PrimaryKeyId("ShippingMethodId");
            }
            set
            {
                this.ViewState["ShippingMethodId"] = value;
            }
        }

        public int ThreadId
        {
            get
            {
                return this.PrimaryKeyId("ThreadId");
            }
            set
            {
                this.ViewState["ThreadID"] = value;
            }
        }

    }

}

