namespace BBICMS
{
    
    public class UIUtility
    {

        public static void SelectListControlItem(ref System.Web.UI.WebControls.ListControl dl, object mValue)
        {
            dl.ClearSelection();
            if ((mValue != null))
            {
                if ((dl.Items.FindByValue(mValue.ToString()) == null) == false)
                {
                    dl.Items.FindByValue(mValue.ToString()).Selected = true;
                    return;
                }
                else if ((dl.Items.FindByText(mValue.ToString()) == null) == false)
                {
                    dl.Items.FindByText(mValue.ToString()).Selected = true;
                    return;
                }
            }
        }

    }
}

