    using System.Web.UI.WebControls;
    using BBICMS.BLL.Articles;

namespace BBICMS.UI.Articles
{
    

    public class ArticlePage : BasePage
    {
        private ArticleRepository _articlerpt;
        private CategoryRepository _categoryrpt;
        private CommentRepository _Commentrpt;

        public void BindCategoriesToListControl(ListControl lCtrl, int selectedId)
        {
            ArticleHelper.BindCategoriesToListControl(lCtrl, selectedId);
        }

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

        public ArticleRepository Articlerpt
        {
            get
            {
                if (null == this._articlerpt)
                {
                    this._articlerpt = new ArticleRepository();
                }
                return this._articlerpt;
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

        public CategoryRepository Categoryrpt
        {
            get
            {
                if (null == this._categoryrpt)
                {
                    this._categoryrpt = new CategoryRepository();
                }
                return this._categoryrpt;
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
                return this.PrimaryKeyId("CommentId");
            }
            set
            {
                this.ViewState["CommentId"] = value;
            }
        }

        public CommentRepository Commentrpt
        {
            get
            {
                if (null == this._Commentrpt)
                {
                    this._Commentrpt = new CommentRepository();
                }
                return this._Commentrpt;
            }
        }
    }
}

