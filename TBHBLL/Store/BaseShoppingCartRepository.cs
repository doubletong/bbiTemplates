using System;
using BBICMS.BLL;

namespace BBICMS.Store
{
   
    public class BaseShoppingCartRepository : BaseRepository
    {
        private ShoppingEntities _Shoppingctx;
        private bool disposedValue;

        public BaseShoppingCartRepository()
        {
            this.disposedValue = false;
            this.ConnectionString = BBICMS.Globals.Settings.DefaultConnectionStringName;
            this.CacheKey = "ShoppingCart";
        }

        public BaseShoppingCartRepository(string sConnectionString)
        {
            this.disposedValue = false;
            this.ConnectionString = sConnectionString;
            this.CacheKey = "ShoppingCart";
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if ((!this.disposedValue && disposing) && null != this.Shoppingctx)
            {
                this.Shoppingctx.Dispose();
            }
            this.disposedValue = true;
        }

        public ShoppingEntities Shoppingctx
        {
            get
            {
                if (null ==this._Shoppingctx)
                {
                    this._Shoppingctx = new ShoppingEntities(this.GetActualConnectionString());
                }
                return this._Shoppingctx;
            }
            set
            {
                this._Shoppingctx = value;
            }
        }
    }
}

