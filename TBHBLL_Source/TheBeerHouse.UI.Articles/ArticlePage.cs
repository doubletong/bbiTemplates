namespace TheBeerHouse.UI.Articles
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.UI.WebControls;
    using TheBeerHouse;
    using TheBeerHouse.BLL.Articles;
    using TheBeerHouse.UI;

    public class ArticlePage : BasePage
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ArticleRepository _articlerpt;
        private CategoryRepository _categoryrpt;
        private CommentRepository _Commentrpt;

        [DebuggerNonUserCode]
        public ArticlePage()
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="lCtrl"></param>
        /// <param name="selectedId"></param>
        /// <remarks></remarks>
        public void BindCategoriesToListControl(ListControl lCtrl, int selectedId)
        {
            ArticleHelper.BindCategoriesToListControl(lCtrl, selectedId);
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

        public ArticleRepository Articlerpt
        {
            get
            {
                if (Information.IsNothing(this._articlerpt))
                {
                    this._articlerpt = new ArticleRepository();
                }
                return this._articlerpt;
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

        public CategoryRepository Categoryrpt
        {
            get
            {
                if (Information.IsNothing(this._categoryrpt))
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
                return this.get_PrimaryKeyId("CommentId");
            }
            set
            {
                this.set_PrimaryKeyId("CommentId", value);
            }
        }

        public CommentRepository Commentrpt
        {
            get
            {
                if (Information.IsNothing(this._Commentrpt))
                {
                    this._Commentrpt = new CommentRepository();
                }
                return this._Commentrpt;
            }
        }
    }
}

