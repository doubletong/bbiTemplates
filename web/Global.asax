<%@ Application Language="C#" %>
<%@ Import Namespace="BBICMS.Store"%>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    public void Profile_MigrateAnonymous(object sender, ProfileMigrateEventArgs e)
    {
        // get a reference to the previously anonymous user's profile
        ProfileBase anonProfile = ProfileBase.Create(e.AnonymousID);
        // if set, copy its Theme to the current user's profile
        if (!string.IsNullOrEmpty(anonProfile.GetProfileGroup("Preferences").GetPropertyValue("Theme").ToString()))
        {
            Profile.Preferences.Theme = anonProfile.GetProfileGroup("Preferences").GetPropertyValue("Theme").ToString();
        }

        ShoppingCart lShoppingCart = (ShoppingCart)anonProfile.GetPropertyValue("ShoppingCart");
        if (null != lShoppingCart && lShoppingCart.Items.Count > 0)
        {
            this.Profile.ShoppingCart = lShoppingCart;
        }


        // delete the anonymous profile
        ProfileManager.DeleteProfile(e.AnonymousID);
        AnonymousIdentificationModule.ClearAnonymousIdentifier();
    }
       
</script>
