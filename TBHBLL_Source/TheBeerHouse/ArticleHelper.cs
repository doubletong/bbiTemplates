namespace TheBeerHouse
{
    using System;
    using System.Web.UI.WebControls;
    using TheBeerHouse.BLL.Articles;

    public class ArticleHelper
    {
        /// <summary>
        /// </summary>
        /// <param name="lCtrl"></param>
        /// <param name="selectedId"></param>
        /// <remarks></remarks>
        public static void BindCategoriesToListControl(ListControl lCtrl, int selectedId)
        {
            using (CategoryRepository Categoryrpt = new CategoryRepository())
            {
                lCtrl.DataTextField = "Title";
                lCtrl.DataValueField = "CategoryID";
                lCtrl.DataSource = Categoryrpt.GetActiveCategories();
                lCtrl.DataBind();
                UIUtility.SelectListControlItem(ref lCtrl, selectedId);
            }
        }
    }
}

