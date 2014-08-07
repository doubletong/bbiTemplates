using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BBICMS.Store
{
    public class ShippingMethodsRepository : BaseShoppingCartRepository
    {
        #region " BLL/DAL Methods "

        public List<ShippingMethod> GetShippingMethods()
        {
            return (from lShippingMethod in Shoppingctx.ShippingMethods
                    select lShippingMethod).ToList();
        }

        public ShippingMethod GetShippingMethodById(int ShippingMethodId)
        {
            return (from lShippingMethod in Shoppingctx.ShippingMethods
                    where lShippingMethod.ShippingMethodID == ShippingMethodId
                    select lShippingMethod).FirstOrDefault();
        }

        public int GetShippingMethodCount()
        {
            return (from lShippingMethod in Shoppingctx.ShippingMethods
                    select lShippingMethod).Count();
        }

        public ShippingMethod AddShippingMethod(int vShippingMethodID, DateTime vAddedDate, string vTitle,
                                                decimal vPrice, DateTime vUpdatedDate)
        {
            ShippingMethod lShippingMethod = default(ShippingMethod);

            if (vShippingMethodID > 0)
            {
                lShippingMethod = GetShippingMethodById(vShippingMethodID);

                lShippingMethod.ShippingMethodID = vShippingMethodID;
                lShippingMethod.AddedDate = vAddedDate;
                lShippingMethod.Title = vTitle;
                lShippingMethod.Price = vPrice;
                lShippingMethod.UpdatedDate = vUpdatedDate;

                lShippingMethod.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lShippingMethod = ShippingMethod.CreateShippingMethod(vShippingMethodID, vAddedDate,
                                                                      Helpers.CurrentUserName, vTitle, vPrice,
                                                                      vUpdatedDate, true);
            }


            return AddShippingMethod(lShippingMethod);
        }

        public ShippingMethod AddShippingMethod(ShippingMethod vShippingMethod)
        {
            try
            {
                if (vShippingMethod.EntityState == EntityState.Detached)
                {
                    Shoppingctx.AddToShippingMethods(vShippingMethod);
                }
                base.PurgeCacheItems(CacheKey);

                return Shoppingctx.SaveChanges() > 0 ? vShippingMethod : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteShippingMethod(ShippingMethod vShippingMethod)
        {
            return ChangeDeletedState(vShippingMethod, false);
        }

        public bool UnDeleteShippingMethod(ShippingMethod vShippingMethod)
        {
            return ChangeDeletedState(vShippingMethod, true);
        }

        private bool ChangeDeletedState(ShippingMethod vShippingMethod, bool vState)
        {
            vShippingMethod.Active = vState;
            vShippingMethod.UpdatedDate = DateTime.Now;
            vShippingMethod.UpdatedBy = CurrentUserName;

            try
            {
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vShippingMethod.ShippingMethodID.ToString(), ex);

                return false;
            }
        }

        #endregion
    }
}