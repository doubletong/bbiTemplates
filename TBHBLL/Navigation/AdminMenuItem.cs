using System.Collections;
using System.Collections.Specialized;

namespace BBICMS
{
    public class AdminMenuItem
    {
        public AdminMenuItem(string vName, string vURL)
        {
            MenuName = vName;
            URL = vURL;
        }

        public AdminMenuItem(string vName, string vImageURL, string vURL)
        {
            MenuName = vName;
            ImageURL = vImageURL;
            URL = vURL;
        }

        public string ImageURL { get; set; }

        public string MenuName { get; set; }

        public string URL { get; set; }
    }


    public class AdminMenuItems
    {
        //private NameValueCollection _AdminPagesPaneIndex;

        //public NameValueCollection AdminPagesPaneIndex
        //{
        //    get { return _AdminPagesPaneIndex; }
        //}


        public static ArrayList FetchArticleMenuItems()
        {
            var ArticleAdminItems = new ArrayList();
            ArticleAdminItems.Add(new AdminMenuItem("Articles", "ManageArticles.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("New Article", "AddEditArticle.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("Categories", "ManageCategories.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("New Category", "AddEditCategory.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("Comments", "ManageComments.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("New Comment", "AddEditComment.aspx"));


            return ArticleAdminItems;
        }


        public static ArrayList FetchSecurityMenuItems()
        {
            var userItems = new ArrayList();
            userItems.Add(new AdminMenuItem("Users", "ManageUsers.aspx"));
            userItems.Add(new AdminMenuItem("New User", "AddEditUser.aspx"));
            userItems.Add(new AdminMenuItem("Roles", "ManageRoles.aspx"));
            userItems.Add(new AdminMenuItem("New Role", "AddEditRole.aspx"));


            return userItems;
        }

        public static ArrayList FetchShoppingCartMenuItems()
        {
            var userItems = new ArrayList();
            userItems.Add(new AdminMenuItem("Manage Products", "ManageProducts.aspx"));
            userItems.Add(new AdminMenuItem("New Product", "AddEditProduct.aspx"));
            userItems.Add(new AdminMenuItem("Manage Departments", "Managedepartments.aspx"));
            userItems.Add(new AdminMenuItem("New Department", "AddEditDepartment.aspx"));
            userItems.Add(new AdminMenuItem("Manage Orders", "Manageorders.aspx"));
            userItems.Add(new AdminMenuItem("Manage Order Statuses", "manageorderstatuses.aspx"));


            return userItems;
        }


        public static ArrayList FetchForumMenuItems()
        {
            var ForumItems = new ArrayList();
            ForumItems.Add(new AdminMenuItem("Forums", "ManageForums.aspx"));
            ForumItems.Add(new AdminMenuItem("New Forums", "AddEditForums.aspx"));
            ForumItems.Add(new AdminMenuItem("Manage Unapproved Posts", "ManageUnapprovedPosts.aspx"));


            return ForumItems;
        }

        public static ArrayList FetchNewsletterMenuItems()
        {
            var ForumItems = new ArrayList();
            ForumItems.Add(new AdminMenuItem("Newsletters", "ManageNewsletters.aspx"));
            ForumItems.Add(new AdminMenuItem("New Newsletter", "AddEditNewsletter.aspx"));


            return ForumItems;
        }

        public static ArrayList FetchGalleryMenuItems()
        {
            var GalleryItems = new ArrayList();
            GalleryItems.Add(new AdminMenuItem("Albums", "ManageAlbums.aspx"));
            GalleryItems.Add(new AdminMenuItem("New Album", "AddEditAlbum.aspx"));
            GalleryItems.Add(new AdminMenuItem("New Photo", "AddEditPhoto.aspx"));


            return GalleryItems;
        }

        public static ArrayList FetchPollMenuItems()
        {
            var userItems = new ArrayList();
            userItems.Add(new AdminMenuItem("Polls", "ManagePolls.aspx"));
            userItems.Add(new AdminMenuItem("New Poll", "AddEditPoll.aspx"));


            return userItems;
        }

        public static ArrayList FetchEventsMenuItems()
        {
            var userItems = new ArrayList();
            userItems.Add(new AdminMenuItem("Manage Events", "ManageEvents.aspx"));
            userItems.Add(new AdminMenuItem("New Event", "AddEditEvent.aspx"));
            userItems.Add(new AdminMenuItem("Manage RSVPs", "ManageEventRSVPs.aspx"));
            userItems.Add(new AdminMenuItem("New RSVP", "AddEditEventRSVP.aspx"));


            return userItems;
        }
    }
}