namespace TheBeerHouse.BLL.Store
{
    using Microsoft.VisualBasic;
    using System;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    public class BaseShoppingCartRepository : BaseRepository
    {
        private ShoppingEntities _Shoppingctx;
        private bool disposedValue;

        public BaseShoppingCartRepository()
        {
            this.disposedValue = false;
            this.ConnectionString = TheBeerHouse.Globals.Settings.DefaultConnectionStringName;
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
            if ((!this.disposedValue && disposing) && !Information.IsNothing(this.Shoppingctx))
            {
                this.Shoppingctx.Dispose();
            }
            this.disposedValue = true;
        }

        public ShoppingEntities Shoppingctx
        {
            get
            {
                if (Information.IsNothing(this._Shoppingctx))
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

