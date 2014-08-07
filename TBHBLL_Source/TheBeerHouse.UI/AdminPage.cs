namespace TheBeerHouse.UI
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.UI.WebControls;
    using TheBeerHouse;
    using TheBeerHouse.BLL;
    using TheBeerHouse.BLL.Store;

    public class AdminPage : BasePage
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        public GalleryElement lGallerySettings;

        public AdminPage()
        {
            base.PreInit += new EventHandler(this.Page_PreInit);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
            this.lGallerySettings = Helpers.Settings.Gallery;
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
                    vDepartmentListControl.Items.Insert(0, new ListItem("All Departments", Conversions.ToString(0)));
                }
                vDepartmentListControl.SelectedValue = Conversions.ToString(this.DepartmentId);
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
                vOrderStatusListControl.Items.Insert(0, new ListItem("All Order Statuses", Conversions.ToString(0)));
                vOrderStatusListControl.SelectedValue = Conversions.ToString(this.OrderStatusId);
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
                    Literal VB$t_ref$S0 = ltlStatus;
                    VB$t_ref$S0.Text = VB$t_ref$S0.Text + kv.Value.Message + "<BR/>";
                }
            }
            else
            {
                ltlStatus.Text = string.Format("The {0} Has Not Been Updated.", vEntityName);
            }
        }

        public void IndicateUpdated(BaseRepository vRepository, string vEntityName, Literal ltlStatus, WebControl cmdDelete)
        {
            ltlStatus.Text = string.Format("The {0} Has Been Updated.", vEntityName);
            if (!Information.IsNothing(cmdDelete))
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
                return this.get_PrimaryKeyId("AlbumId");
            }
            set
            {
                this.set_PrimaryKeyId("AlbumId", value);
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
                return this.get_PrimaryKeyId("ArticleId");
            }
            set
            {
                this.set_PrimaryKeyId("ArticleId", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CategoryId
        {
            get
            {
                return this.get_PrimaryKeyId("CategoryId");
            }
            set
            {
                this.set_PrimaryKeyId("CategoryId", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CommentId
        {
            get
            {
                return this.get_PrimaryKeyId("CommentId");
            }
            set
            {
                this.set_PrimaryKeyId("CommentId", value);
            }
        }

        public int DepartmentId
        {
            get
            {
                return this.get_PrimaryKeyId("departmentId");
            }
            set
            {
                this.set_PrimaryKeyId("departmentId", value);
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
                return this.get_PrimaryKeyId("EventId");
            }
            set
            {
                this.set_PrimaryKeyId("EventId", value);
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
                return this.get_PrimaryKeyId("ForumId");
            }
            set
            {
                this.set_PrimaryKeyId("ForumId", value);
            }
        }

        public int NewsLetterId
        {
            get
            {
                return this.get_PrimaryKeyId("NewsLetterId");
            }
            set
            {
                this.set_PrimaryKeyId("NewsLetterId", value);
            }
        }

        public int OrderId
        {
            get
            {
                return this.get_PrimaryKeyId("OrderId");
            }
            set
            {
                this.set_PrimaryKeyId("OrderId", value);
            }
        }

        public int OrderStatusId
        {
            get
            {
                return this.get_PrimaryKeyId("OrderStatusId");
            }
            set
            {
                this.set_PrimaryKeyId("OrderStatusId", value);
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
                return this.get_PrimaryKeyId("PhotoId");
            }
            set
            {
                this.set_PrimaryKeyId("PhotoId", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int PictureId
        {
            get
            {
                return this.get_PrimaryKeyId("PictureId");
            }
            set
            {
                this.set_PrimaryKeyId("PictureId", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int PollId
        {
            get
            {
                return this.get_PrimaryKeyId("PollId");
            }
            set
            {
                this.set_PrimaryKeyId("PollId", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int PollOptionId
        {
            get
            {
                return this.get_PrimaryKeyId("PollOptionId");
            }
            set
            {
                this.set_PrimaryKeyId("PollOptionId", value);
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int ProductId
        {
            get
            {
                return this.get_PrimaryKeyId("ProductId");
            }
            set
            {
                this.set_PrimaryKeyId("ProductId", value);
            }
        }

        public int ShippingMethodId
        {
            get
            {
                return this.get_PrimaryKeyId("ShippingMethodId");
            }
            set
            {
                this.set_PrimaryKeyId("ShippingMethodId", value);
            }
        }
    }
}

