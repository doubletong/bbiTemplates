namespace TheBeerHouse
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    public class AdminMenuItems
    {
        private NameValueCollection _AdminPagesPaneIndex;

        public static ArrayList FetchArticleMenuItems()
        {
            ArrayList ArticleAdminItems = new ArrayList();
            ArticleAdminItems.Add(new AdminMenuItem("Articles", "ManageArticles.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("New Article", "AddEditArticle.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("Categories", "ManageCategories.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("New Category", "AddEditCategory.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("Comments", "ManageComments.aspx"));
            ArticleAdminItems.Add(new AdminMenuItem("New Comment", "AddEditComment.aspx"));
            return ArticleAdminItems;
        }

        public static ArrayList FetchEventsMenuItems()
        {
            ArrayList userItems = new ArrayList();
            userItems.Add(new AdminMenuItem("Manage Events", "ManageEvents.aspx"));
            userItems.Add(new AdminMenuItem("New Event", "AddEditEvent.aspx"));
            userItems.Add(new AdminMenuItem("Manage RSVPs", "ManageEventRSVPs.aspx"));
            userItems.Add(new AdminMenuItem("New RSVP", "AddEditEventRSVP.aspx"));
            return userItems;
        }

        public static ArrayList FetchForumMenuItems()
        {
            ArrayList ForumItems = new ArrayList();
            ForumItems.Add(new AdminMenuItem("Forums", "ManageForums.aspx"));
            ForumItems.Add(new AdminMenuItem("New Forums", "AddEditForums.aspx"));
            ForumItems.Add(new AdminMenuItem("Manage Unapproved Posts", "ManageUnapprovedPosts.aspx"));
            return ForumItems;
        }

        public static ArrayList FetchGalleryMenuItems()
        {
            ArrayList GalleryItems = new ArrayList();
            GalleryItems.Add(new AdminMenuItem("Albums", "ManageAlbums.aspx"));
            GalleryItems.Add(new AdminMenuItem("New Album", "AddEditAlbum.aspx"));
            GalleryItems.Add(new AdminMenuItem("New Photo", "AddEditPhoto.aspx"));
            return GalleryItems;
        }

        public static ArrayList FetchNewsletterMenuItems()
        {
            ArrayList ForumItems = new ArrayList();
            ForumItems.Add(new AdminMenuItem("Newsletters", "ManageNewsletters.aspx"));
            ForumItems.Add(new AdminMenuItem("New Newsletter", "AddEditNewsletter.aspx"));
            return ForumItems;
        }

        public static ArrayList FetchPollMenuItems()
        {
            ArrayList userItems = new ArrayList();
            userItems.Add(new AdminMenuItem("Polls", "ManagePolls.aspx"));
            userItems.Add(new AdminMenuItem("New Poll", "AddEditPoll.aspx"));
            return userItems;
        }

        public static ArrayList FetchSecurityMenuItems()
        {
            ArrayList userItems = new ArrayList();
            userItems.Add(new AdminMenuItem("Users", "ManageUsers.aspx"));
            userItems.Add(new AdminMenuItem("New User", "AddEditUser.aspx"));
            userItems.Add(new AdminMenuItem("Roles", "ManageRoles.aspx"));
            userItems.Add(new AdminMenuItem("New Role", "AddEditRole.aspx"));
            return userItems;
        }

        public static ArrayList FetchShoppingCartMenuItems()
        {
            ArrayList userItems = new ArrayList();
            userItems.Add(new AdminMenuItem("Manage Products", "ManageProducts.aspx"));
            userItems.Add(new AdminMenuItem("New Product", "AddEditProduct.aspx"));
            userItems.Add(new AdminMenuItem("Manage Departments", "Managedepartments.aspx"));
            userItems.Add(new AdminMenuItem("New Department", "AddEditDepartment.aspx"));
            userItems.Add(new AdminMenuItem("Manage Orders", "Manageorders.aspx"));
            userItems.Add(new AdminMenuItem("Manage Order Statuses", "manageorderstatuses.aspx"));
            return userItems;
        }

        public NameValueCollection AdminPagesPaneIndex
        {
            get
            {
                return this._AdminPagesPaneIndex;
            }
        }
    }
}

