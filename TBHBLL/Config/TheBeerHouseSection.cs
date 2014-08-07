using System;
using System.Configuration;
using System.Web.Configuration;
using BBICMS;


public class BBICMSSection : ConfigurationSection
{

    [ConfigurationProperty("defaultConnectionStringName", DefaultValue = "LocalSqlServer")]
    public string DefaultConnectionStringName
    {
        get { return (string)this["defaultConnectionStringName"]; }
        set { this["DefaultConnectionStringName"] = value; }
    }

    [ConfigurationProperty("devSiteName", DefaultValue = "BeerHouse35")]
    public string devSiteName
    {
        get { return (string)this["devSiteName"]; }
        set { this["devSiteName"] = value; }
    }

    [ConfigurationProperty("siteDomainName", DefaultValue = "localhost")]
    public string SiteDomainName
    {
        get { return (string)this["siteDomainName"]; }
        set { this["siteDomainName"] = value; }
    }

    [ConfigurationProperty("siteName", DefaultValue = "The Beer House")]
    public string SiteName
    {
        get { return (string)this["siteName"]; }
        set { this["siteName"] = value; }
    }

    [ConfigurationProperty("pageSize", DefaultValue = "10")]
    public int PageSize
    {
        get { return (int)this["pageSize"]; }
        set { this["pageSize"] = value; }
    }

    [ConfigurationProperty("enableCaching", DefaultValue = "true")]
    public bool EnableCaching
    {
        get { return (bool)this["enableCaching"]; }
        set { this["enableCaching"] = value; }
    }

    [ConfigurationProperty("defaultCacheDuration", DefaultValue = "600")]
    public int DefaultCacheDuration
    {
        get { return (int)this["defaultCacheDuration"]; }
        set { this["defaultCacheDuration"] = value; }
    }

    [ConfigurationProperty("contactForm", IsRequired = true)]
    public ContactFormElement ContactForm
    {
        get { return (ContactFormElement)this["contactForm"]; }
    }

    [ConfigurationProperty("articles", IsRequired = true)]
    public ArticlesElement Articles
    {
        get { return (ArticlesElement)this["articles"]; }
    }

    [ConfigurationProperty("polls", IsRequired = true)]
    public PollsElement Polls
    {
        get { return (PollsElement)this["polls"]; }
    }

    [ConfigurationProperty("newsletters", IsRequired = true)]
    public NewslettersElement Newsletters
    {
        get { return (NewslettersElement)this["newsletters"]; }
    }

    [ConfigurationProperty("forums", IsRequired = true)]
    public ForumsElement Forums
    {
        get { return (ForumsElement)this["forums"]; }
    }

    [ConfigurationProperty("store", IsRequired = true)]
    public StoreElement Store
    {
        get { return (StoreElement)this["store"]; }
    }

    [ConfigurationProperty("gallery", IsRequired = true)]
    public GalleryElement Gallery
    {
        get { return (GalleryElement)this["gallery"]; }
    }

    [ConfigurationProperty("calendar", IsRequired = true)]
    public CalendarElement Calendar
    {
        get { return (CalendarElement)this["calendar"]; }
    }

}

public class ContactFormElement : ConfigurationElement
{

    [ConfigurationProperty("mailSubject", DefaultValue = "Mail from BBICMS: {0}")]
    public string MailSubject
    {
        get { return (string)this["mailSubject"]; }
        set { this["mailSubject"] = value; }
    }

    [ConfigurationProperty("mailTo", IsRequired = true)]
    public string MailTo
    {
        get { return (string)this["mailTo"]; }
        set { this["mailTo"] = value; }
    }

    [ConfigurationProperty("mailCC")]
    public string MailCC
    {
        get { return (string)this["mailCC"]; }
        set { this["mailCC"] = value; }
    }

}

public class ArticlesElement : ConfigurationElement
{

    [ConfigurationProperty("connectionStringName")]
    public string ConnectionStringName
    {
        get { return (string)this["connectionStringName"]; }
        set { this["ConnectionStringName"] = value; }
    }

    public string ConnectionString
    {
        get
        {
            string connStringName = null;
            if (string.IsNullOrEmpty(this.ConnectionStringName))
            {
                connStringName = Globals.Settings.DefaultConnectionStringName;
            }
            else
            {
                connStringName = this.ConnectionStringName;
            }
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
        }
    }

    [Obsolete("No longer a factor with the Entity Framework.")]
    public string ProviderType
    {
        get { return (string)this["providerType"]; }
        set { this["providerType"] = value; }
    }

    [ConfigurationProperty("ratingLockInterval", DefaultValue = "15")]
    public int RatingLockInterval
    {
        get { return (int)this["ratingLockInterval"]; }
        set { this["ratingLockInterval"] = value; }
    }

    [ConfigurationProperty("pageSize", DefaultValue = "10")]
    public int PageSize
    {
        get { return (int)this["pageSize"]; }
        set { this["pageSize"] = value; }
    }

    [ConfigurationProperty("rssItems", DefaultValue = "5")]
    public int RssItems
    {
        get { return (int)this["rssItems"]; }
        set { this["rssItems"] = value; }
    }

    [ConfigurationProperty("enableCaching", DefaultValue = "true")]
    public bool EnableCaching
    {
        get { return (bool)this["enableCaching"]; }
        set { this["enableCaching"] = value; }
    }

    [ConfigurationProperty("cacheDuration")]
    public int CacheDuration
    {
        get
        {
            int duration = (int)this["cacheDuration"];
            if (duration > 0)
            {
                return duration;
            }
            else
            {
                return Globals.Settings.DefaultCacheDuration;
            }
        }
        set { this["cacheDuration"] = value; }
    }

    [ConfigurationProperty("urlIndicator")]
    public string URLIndicator
    {
        get
        {
            string lurlIndicator = this["urlIndicator"].ToString();
            if (string.IsNullOrEmpty(lurlIndicator))
            {
                lurlIndicator = "Article";
            }
            return lurlIndicator;
        }
        set { this["urlIndicator"] = value; }
    }

    [ConfigurationProperty("categoryUrlIndicator")]
    public string CategoryUrlIndicator
    {
        get
        {
            string lurlIndicator = this["categoryUrlIndicator"].ToString();
            if (string.IsNullOrEmpty(lurlIndicator))
            {
                lurlIndicator = "Category";
            }
            return lurlIndicator;
        }
        set { this["categoryUrlIndicator"] = value; }
    }

    [ConfigurationProperty("akismetKey")]
    public string AkismetKey
    {
        get
        {
            string lakismetKey = this["akismetKey"].ToString();
            if (string.IsNullOrEmpty(lakismetKey))
            {
                lakismetKey = "";
            }
            return lakismetKey;
        }
        set { this["akismetKey"] = value; }
    }

    [ConfigurationProperty("enableAkismet", DefaultValue = "false")]
    public bool EnableAkismet
    {
        get { return (bool)this["enableAkismet"]; }
        set { this["enableAkismet"] = value; }
    }

    [ConfigurationProperty("reportAkismet", DefaultValue = "false")]
    public bool ReportAkismet
    {
        get { return (bool)this["reportAkismet"]; }
        set { this["reportAkismet"] = value; }
    }


    [ConfigurationProperty("enableTwitter", DefaultValue = "false")]
    public bool EnableTwitter
    {
        get { return (bool)this["enableTwitter"]; }
        set { this["enableTwitter"] = value; }
    }

    [ConfigurationProperty("twitterUrserName")]
    public string TwitterUrserName
    {
        get
        {
            string lurlIndicator = this["twitterUrserName"].ToString();
            if (string.IsNullOrEmpty(lurlIndicator))
            {
                lurlIndicator = "TwitterUserName";
            }
            return lurlIndicator;
        }
        set { this["twitterUrserName"] = value; }
    }


    [ConfigurationProperty("twitterPassword")]
    public string TwitterPassword
    {
        get
        {
            string lurlIndicator = this["twitterPassword"].ToString();
            if (string.IsNullOrEmpty(lurlIndicator))
            {
                lurlIndicator = "TwitterPassword";
            }
            return lurlIndicator;
        }
        set { this["twitterPassword"] = value; }
    }

}

public class PollsElement : ConfigurationElement
{

    [ConfigurationProperty("connectionStringName")]
    public string ConnectionStringName
    {
        get { return (string)this["connectionStringName"]; }
        set { this["connectionStringName"] = value; }
    }

    public string ConnectionString
    {
        get
        {
            string connStringName = null;
            if (string.IsNullOrEmpty(this.ConnectionStringName))
            {
                connStringName = Globals.Settings.DefaultConnectionStringName;
            }
            else
            {
                connStringName = this.ConnectionStringName;
            }
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
        }
    }

    [Obsolete("No longer a factor with the Entity Framework.")]
    public string ProviderType
    {
        get { return (string)this["providerType"]; }
        set { this["providerType"] = value; }
    }

    [ConfigurationProperty("votingLockInterval", DefaultValue = "15")]
    public int VotingLockInterval
    {
        get { return (int)this["votingLockInterval"]; }
        set { this["votingLockInterval"] = value; }
    }

    [ConfigurationProperty("votingLockByCookie", DefaultValue = "true")]
    public bool VotingLockByCookie
    {
        get { return (bool)this["votingLockByCookie"]; }
        set { this["votingLockByCookie"] = value; }
    }

    [ConfigurationProperty("votingLockByIP", DefaultValue = "true")]
    public bool VotingLockByIP
    {
        get { return (bool)this["votingLockByIP"]; }
        set { this["votingLockByIP"] = value; }
    }

    [ConfigurationProperty("archiveIsPublic", DefaultValue = "false")]
    public bool ArchiveIsPublic
    {
        get { return (bool)this["archiveIsPublic"]; }
        set { this["archiveIsPublic"] = value; }
    }

    [ConfigurationProperty("enableCaching", DefaultValue = "true")]
    public bool EnableCaching
    {
        get { return (bool)this["enableCaching"]; }
        set { this["enableCaching"] = value; }
    }

    [ConfigurationProperty("cacheDuration")]
    public int CacheDuration
    {
        get
        {
            int duration = (int)this["cacheDuration"];
            if (duration <= 0)
            {
                duration = Globals.Settings.DefaultCacheDuration;
            }
            return duration;
        }
        set { this["cacheDuration"] = value; }
    }

    [ConfigurationProperty("urlIndicator")]
    public string URLIndicator
    {
        get
        {
            string lurlIndicator = this["urlIndicator"].ToString();
            if (string.IsNullOrEmpty(lurlIndicator))
            {
                lurlIndicator = "Poll";
            }
            return lurlIndicator;
        }
        set { this["urlIndicator"] = value; }
    }

}

public class NewslettersElement : ConfigurationElement
{

    [ConfigurationProperty("connectionStringName")]
    public string ConnectionStringName
    {
        get { return (string)this["connectionStringName"]; }
        set { this["connectionStringName"] = value; }
    }

    public string ConnectionString
    {
        get
        {
            string connStringName = null;
            if (string.IsNullOrEmpty(this.ConnectionStringName))
            {
                connStringName = Globals.Settings.DefaultConnectionStringName;
            }
            else
            {
                connStringName = this.ConnectionStringName;
            }
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
        }
    }

    [Obsolete("No longer a factor with the Entity Framework.")]
    public string ProviderType
    {
        get { return (string)this["providerType"]; }
        set { this["providerType"] = value; }
    }

    [ConfigurationProperty("fromEmail", IsRequired = true)]
    public string FromEmail
    {
        get { return (string)this["fromEmail"]; }
        set { this["fromEmail"] = value; }
    }

    [ConfigurationProperty("fromDisplayName", IsRequired = true)]
    public string FromDisplayName
    {
        get { return (string)this["fromDisplayName"]; }
        set { this["fromDisplayName"] = value; }
    }

    [ConfigurationProperty("hideFromArchiveInterval", DefaultValue = "15")]
    public int HideFromArchiveInterval
    {
        get { return (int)this["hideFromArchiveInterval"]; }
        set { this["hideFromArchiveInterval"] = value; }
    }

    [ConfigurationProperty("archiveIsPublic", DefaultValue = "false")]
    public bool ArchiveIsPublic
    {
        get { return (bool)this["archiveIsPublic"]; }
        set { this["archiveIsPublic"] = value; }
    }

    [ConfigurationProperty("enableCaching", DefaultValue = "true")]
    public bool EnableCaching
    {
        get { return (bool)this["enableCaching"]; }
        set { this["enableCaching"] = value; }
    }

    [ConfigurationProperty("cacheDuration")]
    public int CacheDuration
    {
        get
        {
            int duration = (int)this["cacheDuration"];
            if (duration > 0)
            {
                return duration;
            }
            else
            {
                return Globals.Settings.DefaultCacheDuration;
            }
        }
        set { this["cacheDuration"] = value; }
    }

    [ConfigurationProperty("urlIndicator")]
    public string URLIndicator
    {
        get
        {
            string lurlIndicator = this["urlIndicator"].ToString();
            if (string.IsNullOrEmpty(lurlIndicator))
            {
                lurlIndicator = "Newsletter";
            }
            return lurlIndicator;
        }
        set { this["urlIndicator"] = value; }
    }

}

public class ForumsElement : ConfigurationElement
{

    [ConfigurationProperty("connectionStringName")]
    public string ConnectionStringName
    {
        get { return (string)this["connectionStringName"]; }
        set { this["connectionStringName"] = value; }
    }

    public string ConnectionString
    {
        get
        {
            string connStringName = null;
            if (string.IsNullOrEmpty(this.ConnectionStringName))
            {
                connStringName = Globals.Settings.DefaultConnectionStringName;
            }
            else
            {
                connStringName = this.ConnectionStringName;
            }
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
        }
    }

    [Obsolete("No longer a factor with the Entity Framework.")]
    public string Providertype
    {
        get { return (string)this["providerType"]; }
        set { this["providerType"] = value; }
    }

    [ConfigurationProperty("threadsPageSize", DefaultValue = "25")]
    public int ThreadsPageSize
    {
        get { return (int)this["threadsPageSize"]; }
        set { this["threadsPageSize"] = value; }
    }

    [ConfigurationProperty("postsPageSize", DefaultValue = "10")]
    public int PostsPageSize
    {
        get { return (int)this["postsPageSize"]; }
        set { this["postsPageSize"] = value; }
    }

    [ConfigurationProperty("rssItems", DefaultValue = "5")]
    public int RssItems
    {
        get { return (int)this["rssItems"]; }
        set { this["rssItems"] = value; }
    }

    [ConfigurationProperty("hotThreadPosts", DefaultValue = "25")]
    public int HotThreadPosts
    {
        get { return (int)this["hotThreadPosts"]; }
        set { this["hotThreadPosts"] = value; }
    }

    [ConfigurationProperty("bronzePosterPosts", DefaultValue = "100")]
    public int BronzePosterPosts
    {
        get { return (int)this["bronzePosterPosts"]; }
        set { this["bronzePosterPosts"] = value; }
    }

    [ConfigurationProperty("bronzePosterDescription", DefaultValue = "Bronze Poster")]
    public string BronzePosterDescription
    {
        get { return (string)this["bronzePosterDescription"]; }
        set { this["bronzePosterDescription"] = value; }
    }

    [ConfigurationProperty("silverPosterPosts", DefaultValue = "500")]
    public int SilverPosterPosts
    {
        get { return (int)this["silverPosterPosts"]; }
        set { this["silverPosterPosts"] = value; }
    }

    [ConfigurationProperty("silverPosterDescription", DefaultValue = "Silver Poster")]
    public string SilverPosterDescription
    {
        get { return (string)this["silverPosterDescription"]; }
        set { this["silverPosterDescription"] = value; }
    }

    [ConfigurationProperty("goldPosterPosts", DefaultValue = "1000")]
    public int GoldPosterPosts
    {
        get { return (int)this["goldPosterPosts"]; }
        set { this["goldPosterPosts"] = value; }
    }

    [ConfigurationProperty("goldPosterDescription", DefaultValue = "Gold Poster")]
    public string GoldPosterDescription
    {
        get { return (string)this["goldPosterDescription"]; }
        set { this["goldPosterDescription"] = value; }
    }

    [ConfigurationProperty("enableCaching", DefaultValue = "true")]
    public bool EnableCaching
    {
        get { return (bool)this["enableCaching"]; }
        set { this["enableCaching"] = value; }
    }

    [ConfigurationProperty("cacheDuration")]
    public int CacheDuration
    {
        get
        {
            int duration = (int)this["cacheDuration"];
            if (duration > 0)
            {
                return duration;
            }
            else
            {
                return Globals.Settings.DefaultCacheDuration;
            }
        }
        set { this["cacheDuration"] = value; }
    }

    [ConfigurationProperty("urlIndicator")]
    public string URLIndicator
    {
        get
        {
            string lurlIndicator = this["urlIndicator"].ToString();
            if (string.IsNullOrEmpty(lurlIndicator))
            {
                lurlIndicator = "Forum";
            }
            return lurlIndicator;
        }
        set { this["urlIndicator"] = value; }
    }


}

public class StoreElement : ConfigurationElement
{

    [ConfigurationProperty("connectionStringName")]
    public string ConnectionStringName
    {
        get { return this["connectionStringName"].ToString(); }
        set { this["connectionStringName"] = value; }
    }

    public string ConnectionString
    {
        get
        {
            string connStringName = this.ConnectionStringName;
            if (string.IsNullOrEmpty(this.ConnectionStringName))
            {
                connStringName = Globals.Settings.DefaultConnectionStringName;
            }
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
        }
    }

    [Obsolete("No longer a factor with the Entity Framework.")]
    public string ProviderType
    {
        get { return (string)this["providerType"]; }
        set { this["providerType"] = value; }
    }

    [ConfigurationProperty("ratingLockInterval", DefaultValue = "15")]
    public int RatingLockInterval
    {
        get { return (int)this["ratingLockInterval"]; }
        set { this["ratingLockInterval"] = value; }
    }

    [ConfigurationProperty("pageSize", DefaultValue = "10")]
    public int PageSize
    {
        get { return (int)this["pageSize"]; }
        set { this["pageSize"] = value; }
    }

    [ConfigurationProperty("rssItems", DefaultValue = "5")]
    public int RssItems
    {
        get { return (int)this["rssItems"]; }
        set { this["rssItems"] = value; }
    }

    [ConfigurationProperty("defaultOrderListInterval", DefaultValue = "7")]
    public int DefaultOrderListInterval
    {
        get { return (int)this["defaultOrderListInterval"]; }
        set { this["defaultOrderListInterval"] = value; }
    }

    [ConfigurationProperty("sandboxMode", DefaultValue = "false")]
    public bool SandboxMode
    {
        get { return (bool)this["sandboxMode"]; }
        set { this["sandboxMode"] = value; }
    }

    [ConfigurationProperty("businessEmail", IsRequired = true)]
    public string BusinessEmail
    {
        get { return (string)this["businessEmail"]; }
        set { this["businessEmail"] = value; }
    }

    [ConfigurationProperty("currencyCode", DefaultValue = "元")]
    public string CurrencyCode
    {
        get { return (string)this["currencyCode"]; }
        set { this["currencyCode"] = value; }
    }

    [ConfigurationProperty("lowAvailability", DefaultValue = "10")]
    public int LowAvailability
    {
        get { return (int)this["lowAvailability"]; }
        set { this["lowAvailability"] = value; }
    }

    [ConfigurationProperty("enableCaching", DefaultValue = "true")]
    public bool EnableCaching
    {
        get { return (bool)this["enableCaching"]; }
        set { this["enableCaching"] = value; }
    }

    [ConfigurationProperty("cacheDuration")]
    public int CacheDuration
    {
        get
        {
            int duration = (int)this["cacheDuration"];
            if (duration > 0)
            {
                return duration;
            }
            else
            {
                return Globals.Settings.DefaultCacheDuration;
            }
        }
        set { this["cacheDuration"] = value; }
    }

 

    

    /// <summary>
    /// 组图目录
    /// </summary>
    [ConfigurationProperty("photosDirectory", DefaultValue = "Content/Uploads/Products/Photos/")]
    public string PhotosDirectory
    {
        get { return (string)this["photosDirectory"]; }
        set { this["photosDirectory"] = value; }
    }
    /// <summary>
    /// 组图缩略图目录
    /// </summary>
    [ConfigurationProperty("photosThumbDirectory", DefaultValue = "Content/Uploads/Products/Photos/ThumbNails/")]
    public string PhotosThumbDirectory
    {
        get { return (string)this["photosThumbDirectory"]; }
        set { this["photosThumbDirectory"] = value; }
    }

    [ConfigurationProperty("originalsDirectory", DefaultValue = "Originals")]
    public string OriginalsDirectory
    {
        get { return (string)this["originalsDirectory"]; }
        set { this["originalsDirectory"] = value; }
    }

    /// <summary>
    /// 产品分类LOGO目录
    /// </summary>
    [ConfigurationProperty("categoryLogoDirectory", DefaultValue = "Content/Uploads/Products/CategoryLogos/")]
    public string CategoryLogoDirectory
    {
        get { return (string)this["categoryLogoDirectory"]; }
        set { this["categoryLogoDirectory"] = value; }
    }



    /// <summary>
    /// 产品缩略图目录
    /// </summary>
    [ConfigurationProperty("productThumbDirectory", DefaultValue = "Content/Uploads/Products/Thumbnails/")]
    public string ProductThumbDirectory
    {
        get { return (string)this["productThumbDirectory"]; }
        set { this["productThumbDirectory"] = value; }
    }


    /// <summary>
    /// 产品缩略图宽度
    /// </summary>
    [ConfigurationProperty("thumbWidth", DefaultValue = "160")]
    public int ThumbWidth
    {
        get { return (int)this["thumbWidth"]; }
        set { this["thumbWidth"] = value; }
    }

    /// <summary>
    /// 产品缩略图高度
    /// </summary>
    [ConfigurationProperty("thumbHeight", DefaultValue = "120")]
    public int ThumbHeight
    {
        get { return (int)this["thumbHeight"]; }
        set { this["thumbHeight"] = value; }
    }


    /// <summary>
    /// 组图缩略图宽度
    /// </summary>
    [ConfigurationProperty("photoThumbW", DefaultValue = "80")]
    public int PhotoThumbW
    {
        get { return (int)this["photoThumbW"]; }
        set { this["photoThumbW"] = value; }
    }

    /// <summary>
    /// 组图缩略图高度
    /// </summary>
    [ConfigurationProperty("photoThumbH", DefaultValue = "80")]
    public int PhotoThumbH
    {
        get { return (int)this["photoThumbH"]; }
        set { this["photoThumbH"] = value; }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    [ConfigurationProperty("displayHeight", DefaultValue = "800")]
    public int DisplayHeight
    {
        get { return (int)this["displayHeight"]; }
        set { this["displayHeight"] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    [ConfigurationProperty("displayWidth", DefaultValue = "600")]
    public int DisplayWidth
    {
        get { return (int)this["displayWidth"]; }
        set { this["displayWidth"] = value; }
    }




}

public class GalleryElement : ConfigurationElement
{

    [ConfigurationProperty("photosDirectory", DefaultValue = "Photos")]
    public string PhotosDirectory
    {
        get { return (string)this["photosDirectory"]; }
        set { this["photosDirectory"] = value; }
    }

    [ConfigurationProperty("originalsDirectory", DefaultValue = "Originals")]
    public string OriginalsDirectory
    {
        get { return (string)this["originalsDirectory"]; }
        set { this["originalsDirectory"] = value; }
    }

    [ConfigurationProperty("albumThumbNailsDirectory", DefaultValue = "ThumbNails")]
    public string AlbumThumbNailsDirectory
    {
        get { return (string)this["albumThumbNailsDirectory"]; }
        set { this["albumThumbNailsDirectory"] = value; }
    }

    [ConfigurationProperty("albumDisplayDirectory", DefaultValue = "Display")]
    public string AlbumDisplayDirectory
    {
        get { return (string)this["albumDisplayDirectory"]; }
        set { this["albumDisplayDirectory"] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    [ConfigurationProperty("thumbWidth", DefaultValue = "80")]
    public int ThumbWidth
    {
        get { return (int)this["thumbWidth"]; }
        set { this["thumbWidth"] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    [ConfigurationProperty("thumbHeight", DefaultValue = "80")]
    public int ThumbHeight
    {
        get { return (int)this["thumbHeight"]; }
        set { this["thumbHeight"] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    [ConfigurationProperty("displayHeight", DefaultValue = "500")]
    public int DisplayHeight
    {
        get { return (int)this["displayHeight"]; }
        set { this["displayHeight"] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    [ConfigurationProperty("displayWidth", DefaultValue = "400")]
    public int DisplayWidth
    {
        get { return (int)this["displayWidth"]; }
        set { this["displayWidth"] = value; }
    }

}

public class CalendarElement : ConfigurationElement
{

    [ConfigurationProperty("urlIndicator", DefaultValue = "Events")]
    public string URLIndicator
    {
        get
        {
            string lurlIndicator = this["urlIndicator"].ToString();
            if (string.IsNullOrEmpty(lurlIndicator))
            {
                lurlIndicator = "Events";
            }
            return lurlIndicator;
        }
        set { this["urlIndicator"] = value; }
    }

}