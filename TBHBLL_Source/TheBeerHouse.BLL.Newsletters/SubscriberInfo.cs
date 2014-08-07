namespace TheBeerHouse.BLL.Newsletters
{
    using System;

    public class SubscriberInfo
    {
        public string Email;
        public string FirstName;
        public string LastName;
        public TheBeerHouse.BLL.Newsletters.SubscriptionType SubscriptionType;
        public string UserName;

        public SubscriberInfo(string userName, string email, string firstName, string lastName, TheBeerHouse.BLL.Newsletters.SubscriptionType subscriptionType)
        {
            this.UserName = userName;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SubscriptionType = subscriptionType;
        }
    }
}

