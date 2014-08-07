namespace TheBeerHouse
{
    using Microsoft.VisualBasic;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class UIUtility
    {
        /// <summary>
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="mValue"></param>
        /// <remarks></remarks>
        public static void SelectListControlItem(ref ListControl dl, object mValue)
        {
            dl.ClearSelection();
            if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(mValue)))
            {
                if (!Information.IsNothing(dl.Items.FindByValue(mValue.ToString())))
                {
                    dl.Items.FindByValue(mValue.ToString()).Selected = true;
                }
                else if (!Information.IsNothing(dl.Items.FindByText(mValue.ToString())))
                {
                    dl.Items.FindByText(mValue.ToString()).Selected = true;
                }
            }
        }
    }
}

