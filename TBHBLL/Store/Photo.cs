using System;
using System.Data;

using BLL;


namespace BBICMS.Store
{

    public partial class Photo : IBaseEntity
    {

        public string ProTitle
        {
            get
            {
                if ((Product != null))
                {
                    return Product.Title;
                }
                return "无品名";
            }
        }


        //public int ProductId
        //{
        //    get
        //    {
        //        if ((this.ProductReference.EntityKey != null))
        //        {
        //            return int.Parse(this.ProductReference.EntityKey.EntityKeyValues[0].Value.ToString());
        //        }
        //        return 0;
        //    }
        //    set
        //    {
        //        if ((this.ProductReference.EntityKey != null))
        //        {
        //            this.ProductReference = null;
        //        }
        //        this.ProductReference.EntityKey = new EntityKey("StoresEntities.Products", "ProductID", value);
        //    }
        //}


       

        private string _setName = "Photos";
        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Thumbnail) == false & string.IsNullOrEmpty(this.OriginalPic) == false)
                {
                    return true;
                }
                return false;
            }
        }

        bool bIsDirty = false;
        public bool IsDirty
        {
            get { return bIsDirty; }
            set { bIsDirty = value; }
        }


        public bool CanAdd
        {
            get { return true; }
        }

        public bool CanDelete
        {
            get { return true; }
        }

        public bool CanEdit
        {
            get { return true; }
        }

        public bool CanRead
        {
            get { return true; }
        }


        partial void OnAddedByChanging(string value)
        {
            if (string.IsNullOrEmpty(this.AddedBy) == false)
            {
                throw new ArgumentException("发布人一旦设定就无法修改！");
            }
        }

    }


}

