using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BBICMS.Newsletters;
using BBICMS;
using BBICMS.UI;

partial class Admin_ManageNewsLetters : AdminPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = string.Format(this.Title, Helpers.Settings.SiteName);
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            BindArchivedNewsLetters();
            
        }
    }
    
    protected void BindArchivedNewsLetters()
    {
        
        using (NewslettersRepository lNewsLetterrpt = new NewslettersRepository()) {
            
            List<Newsletter> lNewsLetters = lNewsLetterrpt.GetNewsletters();
            lvNewsLetters.DataSource = lNewsLetters;
            lvNewsLetters.DataBind();

            DataPager pagerBottom = (DataPager)lvNewsLetters.FindControl("pagerBottom");

            if ((pagerBottom != null))
            {
                if (lNewsLetters.Count <= pagerBottom.PageSize)
                {
                    pagerBottom.Visible = false;
                }
                else
                {
                    pagerBottom.Visible = true;
                }

            }
        }
            
        
    }

    protected void lvNewsLetters_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindArchivedNewsLetters();
    }
    
}