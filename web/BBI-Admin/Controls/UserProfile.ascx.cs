using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using BBICMS.Newsletters;
using BBICMS;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;

public partial class Admin_Controls_UserProfile : System.Web.UI.UserControl
{
    #region " Properties "

    public string FirstName
    {
        get
        {
            if (string.IsNullOrEmpty(FullName) == false)
            {
                if (FullName.Split(' ').Length > 0)
                {
                    return FullName.Split(' ')[0];
                }
                return FullName;
            }
            return string.Empty;
        }
    }

    public string LastName
    {
        get
        {
            if (string.IsNullOrEmpty(FullName) == false)
            {
                if (FullName.Split(' ').Length > 0)
                {
                    return FullName.Split(' ')[FullName.Split(' ').Length - 1];
                }
                return FullName;
            }
            return string.Empty;
        }
    }

    public string FullName
    {
        get { return Request["fullname"]; }
    }

    public string EMail
    {
        get { return this.Request.QueryString["Email"]; }
    }

    public string DOB
    {
        get { return Request["dob"]; }
    }

    public string Gender
    {
        get { return Request["gender"]; }
    }

    public string PostCode
    {
        get { return Request["postcode"]; }
    }

    public string Country
    {
        get { return Request["country"]; }
    }

    #endregion

    private string _userName = string.Empty;
    public string Username
    {
        get
        {
            if (string.IsNullOrEmpty(_userName))
            {
                return Helpers.CurrentUser.Identity.Name;
            }
            return _userName;
        }
        set { _userName = value; }
    }



    private ProfileBase GetProfile()
    {
        ProfileBase profile = Helpers.GetUserProfile(_userName);
        if ((profile == null))
        {
            profile = ProfileBase.Create(_userName, false);
        }
        //If Me.Username.Length > 0 Then
        return profile;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!this.IsPostBack)
        {
           

            // if the UserName property contains an empty string, retrieve the profile
            // for the current user, otherwise for the specified user

            ddlSubscriptions.SelectedValue = Profile.Preferences.Newsletter.ToString();
            ddlLanguages.SelectedValue = Profile.Preferences.Culture;
            txtFullName.Text = Profile.FullName;
         

            if (string.IsNullOrEmpty(Gender))
            {
                ddlGenders.SelectedValue = Gender;
            }
            else
            {
                ddlGenders.SelectedValue = Profile.Gender;
            }

            if (!(Profile.BirthDate == DateTime.MinValue))
            {
                txtBirthDate.Text = Profile.BirthDate.ToShortDateString();
            }
            ddlOccupations.SelectedValue = Profile.Occupation;
            txtWebsite.Text = Profile.Website;
            txtStreet.Text = Profile.Address.Street;
            txtCity.Text = Profile.Address.City;
            txtPostalCode.Text = Profile.Address.PostalCode;
            ddlState.SelectedValue = Profile.Address.State;

            if (string.IsNullOrEmpty(Country))
            {
                ddlCountry.SelectedValue = Country;
            }
            else
            {
                ddlCountry.SelectedValue = Profile.Address.Country;
            }

            txtPhone.Text = Profile.Contacts.Phone;
            txtFax.Text = Profile.Contacts.Fax;
            txtAvatarUrl.Text = GetAvatarURL();

            txtSignature.Text = Profile.Forum.Signature;

        }
    }


    public void SaveProfile()
    {
        // if the UserName property contains an emtpy string, save the current user's profile,
        // othwerwise save the profile for the specified user

        Profile.Preferences.Newsletter = (SubscriptionType)int.Parse(ddlSubscriptions.SelectedValue);
        Profile.Preferences.Culture = ddlLanguages.SelectedValue;
        Profile.FullName = txtFullName.Text;
     
        Profile.Gender = ddlGenders.SelectedValue;
        if (txtBirthDate.Text.Trim().Length > 0)
        {
            Profile.BirthDate = DateTime.Parse(txtBirthDate.Text);
        }
        Profile.Occupation = ddlOccupations.SelectedValue;
        Profile.Website = txtWebsite.Text;
        Profile.Address.Street = txtStreet.Text;
        Profile.Address.City = txtCity.Text;
        Profile.Address.PostalCode = txtPostalCode.Text;
        Profile.Address.State = ddlState.SelectedValue;
        Profile.Address.Country = ddlCountry.SelectedValue;
        Profile.Contacts.Phone = txtPhone.Text;
        Profile.Contacts.Fax = txtFax.Text;
        Profile.Forum.AvatarUrl = txtAvatarUrl.Text;
        Profile.Forum.Signature = txtSignature.Text;

        Profile.Save();
    }

    public string GetAvatarURL()
    {

        if (string.IsNullOrEmpty(Profile.Forum.AvatarUrl))
        {

            MembershipUser mu = Membership.GetUser(_userName);
            if ((mu != null) && !string.IsNullOrEmpty(mu.Email))
            {
                return string.Format("http://www.gravatar.com/avatar/{0}.jpg?d=wavatar&s=32", GetGravatarHash(Membership.GetUser().Email));
            }
            else
            {
                return string.Format("{0}/images/user.gif", Helpers.WebRoot);

            }
        }
        else
        {
            return Profile.Forum.AvatarUrl;
        }
    }

    public string GetGravatarHash(string sEmail)
    {

        MD5 md5Hasher = MD5.Create();

        //Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(sEmail));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        int i = 0;
        for (i = 0; i <= data.Length - 1; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.

        return sBuilder.ToString();
    }
}