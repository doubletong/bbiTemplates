namespace TheBeerHouse
{
    using System;

    public class ForumHelper
    {
        public static int GetNoofPostForUser(string vUserName)
        {
            return Helpers.GetProfileSubGroupIntegerProperty(Helpers.GetUserProfile(vUserName), "Forum", "Posts");
        }

        public static string GetPosterAvatar(string vUserName)
        {
            return Helpers.GetProfileSubGroupStringProperty(Helpers.GetUserProfile(vUserName), "Forum", "AvatarUrl");
        }

        public static string GetPosterSignature(string vUserName)
        {
            return Helpers.GetProfileSubGroupStringProperty(Helpers.GetUserProfile(vUserName), "Forum", "Signature");
        }
    }
}

