
namespace BBICMS.Newsletters
{
    public partial class SubscriberInfo
    {
        public string Email;
        public string FullName;
      
        public SubscriptionType SubscriptionType;
        public string UserName;

        public SubscriberInfo(string userName, string email, string fullName, 
                              SubscriptionType subscriptionType)
        {
            UserName = userName;
            Email = email;
            FullName = fullName;
          
            SubscriptionType = subscriptionType;
        }
    }
}