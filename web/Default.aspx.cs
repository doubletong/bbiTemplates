using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BBICMS.BLL.Articles;
using BBICMS.UI;


public partial class _Default : BasePage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
          //  BindHomeArticles();
            
        }
    }
    
    //protected void BindHomeArticles()
    //{
        
    //    using (ArticleRepository lArticleCtx = new ArticleRepository()) {
            
    //        lvArticles.DataSource = lArticleCtx.GetHomePageArticles();
                
    //        lvArticles.DataBind();
            
            
    //    }
    //}
    
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        base.MoveHiddenFields = true;
    }

    protected void DataPager_PreRender(object sender, EventArgs e)
    {
        // DataPager pager = (DataPager)Page.FindControl("pagerBottom");
        // pager.Controls.Clear();

        int count = pdPager.TotalRowCount;
        int pageSize = pdPager.PageSize;
        int pagesCount = count / pageSize + (count % pageSize == 0 ? 0 : 1);
        int pageSelected = pdPager.StartRowIndex / pageSize + 1;

        if (pageSelected > 1)
        {
            // first page
            HyperLink img = new HyperLink();

            img.Text = "首页";
            img.NavigateUrl = "/";
            pdPager.Controls.Add(img);
            // gap
            Literal space = new Literal();
            space.Text = " ";
            pdPager.Controls.Add(space);
        }


        for (int i = 1; i <= pagesCount; ++i)
        {
            if (pageSelected != i)
            {
                HyperLink link = new HyperLink();
                link.NavigateUrl = "/Page_" + i.ToString() + "/";
                link.Text = i.ToString();
                pdPager.Controls.Add(link);


            }
            else
            {
                Literal lit = new Literal();
                lit.Text = "<strong>" + i.ToString() + "</strong>";
                pdPager.Controls.Add(lit);
            }

            Literal spaceb = new Literal();
            spaceb.Text = " ";
            pdPager.Controls.Add(spaceb);

        }
        if (pageSelected < pagesCount)
        {

            // last page
            HyperLink imgb = new HyperLink();

            imgb.Text = "尾页";
            imgb.NavigateUrl = "/Page_" + pagesCount + "/";
            pdPager.Controls.Add(imgb);
        }

    }
    
}