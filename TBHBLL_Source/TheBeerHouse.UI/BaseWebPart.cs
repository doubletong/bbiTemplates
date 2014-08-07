namespace TheBeerHouse.UI
{
    using Microsoft.VisualBasic;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls.WebParts;
    using TheBeerHouse.BLL.Articles;

    public class BaseWebPart : UserControl, IWebPart
    {
        private IArticleRepository _articleRepository;
        private string _catalogIconImageUrl = string.Empty;
        private string _description = string.Empty;
        protected string _subTitle = string.Empty;
        protected string _title = string.Empty;
        private string _titleIconImageUrl = string.Empty;
        private string _titleUrl = string.Empty;

        public IArticleRepository ArticleRepository
        {
            get
            {
                if (Information.IsNothing(this._articleRepository))
                {
                    this._articleRepository = (IArticleRepository) new TheBeerHouse.BLL.Articles.ArticleRepository();
                }
                return this._articleRepository;
            }
            set
            {
                this._articleRepository = value;
            }
        }

        public string CatalogIconImageUrl
        {
            get
            {
                return this._catalogIconImageUrl;
            }
            set
            {
                this._catalogIconImageUrl = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string SubTitle
        {
            get
            {
                return this._subTitle;
            }
        }

        public string System.Web.UI.WebControls.WebParts.IWebPart.CatalogIconImageUrl
        {
            get
            {
                return this._catalogIconImageUrl;
            }
            set
            {
                this._catalogIconImageUrl = value;
            }
        }

        public string System.Web.UI.WebControls.WebParts.IWebPart.Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string System.Web.UI.WebControls.WebParts.IWebPart.Subtitle
        {
            get
            {
                return this._subTitle;
            }
        }

        public string System.Web.UI.WebControls.WebParts.IWebPart.Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public string System.Web.UI.WebControls.WebParts.IWebPart.TitleIconImageUrl
        {
            get
            {
                return this._titleIconImageUrl;
            }
            set
            {
                this._titleIconImageUrl = value;
            }
        }

        public string System.Web.UI.WebControls.WebParts.IWebPart.TitleUrl
        {
            get
            {
                return this._titleUrl;
            }
            set
            {
                this._titleUrl = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public string TitleIconImageUrl
        {
            get
            {
                return this._titleIconImageUrl;
            }
            set
            {
                this._titleIconImageUrl = value;
            }
        }

        public string TitleUrl
        {
            get
            {
                return this._titleUrl;
            }
            set
            {
                this._titleUrl = value;
            }
        }
    }
}

