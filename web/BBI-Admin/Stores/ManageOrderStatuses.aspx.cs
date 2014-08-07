using System.Collections.Generic;
using System.Web.UI.WebControls;
using BBICMS.BLL.Store;
using BBICMS.Store;
using BBICMS.UI;

partial class Admin_ManageOrderStatuses : AdminPage
{
    
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            BindOrderStatuses();
            
        }
    }
    
    protected void BindOrderStatuses()
    {
        using (OrderStatusesRepository lOrderStatusesrpt = new OrderStatusesRepository()) {
            
            
            List<OrderStatus> lOrderStatusess = lOrderStatusesrpt.GetOrderStatuses();
            lvOrderStatuses.DataSource = lOrderStatusess;
            lvOrderStatuses.DataBind();
            
            DataPager pagerBottom = (DataPager)lvOrderStatuses.FindControl("pagerBottom");
            
            if ((pagerBottom != null)) {
                if (lOrderStatusess.Count <= pagerBottom.PageSize) {
                    pagerBottom.Visible = false;
                }
                else {
                    pagerBottom.Visible = true;
                }
            }
        }
    }
    
    protected void lvOrderStatuses_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
    {
        
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;
        
        if (lvdi.ItemType == ListViewItemType.DataItem) {
            
            ImageButton btnDelete = (ImageButton)lvdi.FindControl("btnDelete");
            OrderStatus lOrderStatus = (OrderStatus)lvdi.DataItem;
            
            if ((lOrderStatus != null) & (btnDelete != null)) {
                
                if (lOrderStatus.OrderStatusID <= 5) {
                    btnDelete.Visible = false;
                    
                }
                
            }
        }
    }
    
    protected void lvOrderStatuses_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindOrderStatuses();
    }
    
}