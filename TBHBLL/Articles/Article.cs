using TwitterLib;
using System.IO;
using BBICMS;
using System;
using BLL;
using System.Data;

namespace BBICMS.Articles
{

    public partial class Article : IBaseEntity
    {

        public void PostTweet()
        {

            TwitterNet lTwitterNet = new TwitterNet(Helpers.Settings.Articles.TwitterUrserName, TwitterNet.ToSecureString("pack62"));
            UrlShorteningService vTinyURL = new UrlShorteningService(ShorteningService.TinyUrl);
            string lTweet = string.Format("{0} - {1}", this.Title, vTinyURL.ShrinkUrls(Helpers.SEOFriendlyURL(Path.Combine(Helpers.Settings.Articles.URLIndicator, this.Title), ".aspx")));


            var id = lTwitterNet.AddTweet(lTweet);
        }

        #region " Immutable Properties "

        /// <summary>
        /// This method repurposes the ReleaseDate property.
        /// The ReleaseData is a Nullable(of T) and therefore
        /// cannot be used directly as a DateTime object, and
        /// the ToShortDateString method is needed to turn the date
        /// into a string. 
        /// </summary>
        /// <value>ShortDateString value of the ReleaseDate</value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string ArticleReleaseDate
        {
            get
            {

                if (ReleaseDate.GetValueOrDefault() != null)
                {
                    DateTime lRelaseDate = ReleaseDate.GetValueOrDefault();
                    return lRelaseDate.ToShortDateString();
                }


                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double AverageRating
        {
            get
            {
                if (this.Votes >= 1)
                {
                    return (double)this.TotalRating / (double)this.Votes;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CategoryTitle
        {
            get
            {
                if (this.Category != null)
                {
                    return this.Category.Title;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Location
        {
            get
            {
                string _location = string.Empty;

                if ((this.City != null))
                {
                    _location = this.City.Split(';')[0];
                }

                if (string.IsNullOrEmpty(this.State) == false)
                {
                    if (_location.Length > 0)
                    {
                        _location += ", ";
                    }
                    _location += this.State.Split(';')[0];
                }

                if (string.IsNullOrEmpty(this.Country) == false)
                {
                    if (_location.Length > 0)
                    {
                        _location += ", ";
                    }
                    _location += this.Country;
                }

                return _location;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Published
        {
            get { return (this.Approved & this.ReleaseDate <= DateTime.Now & this.ExpireDate > DateTime.Now); }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        partial void OnAddedByChanging(string value)
        {
            if (string.IsNullOrEmpty(this.AddedBy) == false)
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }

        partial void OnAddedDateChanged()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        partial void OnAddedDateChanging(System.DateTime value)
        {
            if (DateTime.MinValue == this.AddedDate & value <= DateTime.MinValue)
            {
                throw new ArgumentException("The Added Date cannot be empty.");
            }
        }

        /// <summary>
        /// http://architectmuse.blogspot.com/2008/09/having-navigation-properties-and.html
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CategoryId
        {
            get
            {
                if ((this.CategoryReference.EntityKey != null))
                {
                    return (int)this.CategoryReference.EntityKey.EntityKeyValues[0].Value;
                }
                return 0;
            }
            set
            {
                if ((this.CategoryReference.EntityKey != null))
                {
                    this.CategoryReference = null;
                }
                this.CategoryReference.EntityKey = new EntityKey("ArticlesEntities.Categories", "CategoryID", value);
            }
        }


        private string _setName = "Articles";
        /// <summary>
        /// Returns the name of the Data Set the Entity belongs to. Needs to be set
        /// in the derived class.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Title) == false & string.IsNullOrEmpty(this.Abstract) == false & string.IsNullOrEmpty(this.Body) == false & this.ReleaseDate < this.ExpireDate)
                {
                    return true;
                }
                return false;
            }
        }

        bool bIsDirty = false;
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsDirty
        {
            get { return bIsDirty; }
            set { bIsDirty = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        partial void OnArticleIDChanging(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("The ArticleId cannot be less than 0.");
            }

            if (value != ArticleID)
            {
                IsDirty = true;

            }
        }

        #region " Authorizations "

        public bool CanAdd
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }


        public bool CanDelete
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanEdit
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanRead
        {
            get { return true; }
        }

        #endregion

    }
}