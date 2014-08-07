using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Profile;
using System.Web.UI.HtmlControls;
using BBICMS;
using BBICMS.UI;

partial class Admin_ManageUsers : AdminPage
{
    private readonly MembershipUserCollection allUsers = Membership.GetAllUsers();


    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = string.Format(this.Title, Helpers.Settings.SiteName);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAlphabet();
        }
    }

    protected void BindAlphabet()
    {
        char[] alphabet = {
                              'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                              'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                              'U', 'V', 'W', 'X', 'Y', 'Z'
                          };

        lvAlphabet.DataSource = alphabet;
        lvAlphabet.DataBind();

        lblTotUsers.Text = allUsers.Count.ToString();
        lblOnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();
    }

    protected void BindUsers()
    {
        MembershipUserCollection users = null;

        StringBuilder lSearchText = new StringBuilder();
        if (string.IsNullOrEmpty(lvUsers.Attributes["SearchText"]) == false)
        {
            lSearchText.Append(lvUsers.Attributes["SearchText"]);
        }

        bool searchByEmail = false;
        if (string.IsNullOrEmpty(lvUsers.Attributes["SearchByEmail"]) == false)
        {
            searchByEmail = bool.Parse(lvUsers.Attributes["SearchByEmail"]);
        }

        if (lSearchText.Length > 0)
        {
            if ((searchByEmail))
            {
                users = Membership.FindUsersByEmail(lSearchText.ToString());
            }
            else
            {
                users = Membership.FindUsersByName(lSearchText.ToString());
            }
        }
        else
        {
            users = allUsers;
        }

        lvUsers.DataSource = users;

        lvUsers.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bool searchByEmail = (ddlSearchTypes.SelectedValue == "E-mail");
        lvUsers.Attributes.Add("SearchText", "%" + txtSearchText.Text + "%");
        lvUsers.Attributes.Add("SearchByEmail", searchByEmail.ToString());

        BindUsers();
    }

   

    protected void lvAlphabet_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        lvUsers.Attributes.Add("SearchByEmail", false.ToString());
        if ((e.CommandArgument.ToString().Length == 1))
        {
            lvUsers.Attributes.Add("SearchText", e.CommandArgument + "%");
            BindUsers();
        }
        else
        {
            lvUsers.Attributes.Add("SearchText", "");
            BindUsers();
        }
    }

  
    protected void lvUsers_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        string userName = lvUsers.DataKeys[e.ItemIndex].Value.ToString();
        ProfileManager.DeleteProfile(userName);
        Membership.DeleteUser(userName);
        BindUsers();
        //  lblTotUsers.Text = allUsers.Count.ToString();
    }
    protected void DeleteUser(string UserName)
    {
        if (Membership.DeleteUser(UserName))
        {
            BindUsers();
            ltlstatus.Text = "The user was deleted.";
        }
        else
        {
            ltlstatus.Text = "The user was not deleted.";
        }
    }

    protected void lvUsers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;

        if (lvdi.ItemType == ListViewItemType.DataItem)
        {
            //LinkButton btnDelete = (LinkButton)lvdi.FindControl("btnDelete");
            //MembershipUser mu = (MembershipUser)lvdi.DataItem;

            //if ((mu != null) & (btnDelete != null))
            //{
            //    btnDelete.CommandArgument = mu.UserName;
            //    btnDelete.CommandName = "DeleteUser";
            //    btnDelete.Attributes.Add("onclick",
            //                             "return confirm('Warning: This will delete the User from the database.')");
            //}
        }
    }

    protected void lvUsers_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            LinkButton btn = e.Item.FindControl("btnDelete") as LinkButton;
            btn.OnClientClick = "if (confirm('你确定要删除这个帐号吗？') == false) return false;";
        }
    }

    protected void lvUsers_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        BindUsers();
    }
}