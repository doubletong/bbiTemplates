using System;
using BBICMS.BLL.Store;
using BBICMS.Store;
using BBICMS.UI;

partial class Admin_AddEditOrderStatus : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (OrderStatusId > 0)
            {
                BindOrderStatuses();
            }
            else
            {
                ClearItems();
            }
        }
    }

    protected void ClearItems()
    {
        ltlOrderStatusID.Text = string.Empty;
        ltlAddedDate.Text = string.Empty;
        txtTitle.Text = string.Empty;
        ltlUpdatedDate.Text = string.Empty;


        ltlStatus.Text = "Create a New OrderStatuses.";
    }

    protected void BindOrderStatuses()
    {
        using (OrderStatusesRepository lOrderStatusesrpt = new OrderStatusesRepository())
        {
            OrderStatus lOrderStatuses = lOrderStatusesrpt.GetOrderStatusById(OrderStatusId);

            if ((lOrderStatuses != null))
            {
                ltlOrderStatusID.Text = lOrderStatuses.OrderStatusID.ToString();
                ltlAddedDate.Text = lOrderStatuses.AddedDate.ToShortDateString();
                txtTitle.Text = lOrderStatuses.Title;
                ltlUpdatedDate.Text = lOrderStatuses.UpdatedDate.ToShortDateString();

                ltlActive.Text = lOrderStatuses.Active.ToString();
                ltlAddedBy.Text = lOrderStatuses.AddedBy;
                ltlUpdatedBy.Text = lOrderStatuses.UpdatedBy;

                ltlStatus.Text = "Edit The Order Status";
            }
            else
            {
                OrderStatusId = 0;
                ltlStatus.Text = "Create a New Order Status";
            }
        }
    }

    protected void ManageOrderStatusess()
    {
        Response.Redirect("ManageOrderStatusess.aspx");
    }

    protected void UpdateOrderStatuses()
    {
        using (OrderStatusesRepository lOrderStatusesrpt = new OrderStatusesRepository())
        {
            OrderStatus lOrderStatuses = new OrderStatus();

            if (OrderStatusId > 0)
            {
                lOrderStatuses = lOrderStatusesrpt.GetOrderStatusById(OrderStatusId);
            }
            else
            {
                lOrderStatuses = new OrderStatus();
            }

            lOrderStatuses.Title = txtTitle.Text;
            lOrderStatuses.UpdatedDate = DateTime.Now;
            lOrderStatuses.UpdatedBy = UserName;

            if (lOrderStatuses.OrderStatusID > 0)
            {
                if ((lOrderStatusesrpt.AddOrderStatus(lOrderStatuses) != null))
                {
                    IndicateUpdated(lOrderStatusesrpt, "Order Status", ltlStatus, cmdDelete);
                }
                else
                {
                    IndicateNotUpdated(lOrderStatusesrpt, "Order Status", ltlStatus);
                }
            }
            else
            {
                lOrderStatuses.Active = true;
                lOrderStatuses.AddedBy = UserName;
                lOrderStatuses.AddedDate = DateTime.Now;
                if ((lOrderStatusesrpt.AddOrderStatus(lOrderStatuses) != null))
                {
                    IndicateUpdated(lOrderStatusesrpt, "Order Status", ltlStatus, cmdDelete);
                }
                else
                {
                    IndicateNotUpdated(lOrderStatusesrpt, "Order Status", ltlStatus);
                }
            }
        }
    }

    protected void GoToOrderStatusesList()
    {
        Response.Redirect("ManageOrderStatusess.aspx");
    }

    protected void DeleteOrderStatuses()
    {
        using (OrderStatusesRepository lOrderStatusesrpt = new OrderStatusesRepository())
        {
            lOrderStatusesrpt.DeleteOrderStatus(lOrderStatusesrpt.GetOrderStatusById(OrderStatusId));
        }
        GoToOrderStatusesList();
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        DeleteOrderStatuses();
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        UpdateOrderStatuses();
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        GoToOrderStatusesList();
    }
}