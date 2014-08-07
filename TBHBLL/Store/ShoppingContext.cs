using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using BLL;

namespace BBICMS.Store
{
    public partial class ShoppingEntities
    {
        /// <summary>
        /// Checks each object that is either new to the Context or has been updated 
        /// to verify each is valid. If one does not return true when the Entity's 
        /// IsValid method is called a BeerHouseDataException is thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This is an event handler that will get called when ever the SaveChanges method is called.</remarks>
        protected void ShoppingEntities_SavingChanges(object sender, EventArgs e)
        {
            List<ObjectStateEntry> typeEntries = (from entry in
                                                      ObjectStateManager.GetObjectStateEntries(EntityState.Added |
                                                                                               EntityState.Modified)
                                                  where entry.Entity is IBaseEntity
                                                  select entry).ToList();

            foreach (ObjectStateEntry ose in typeEntries)
            {
                var lBaseEntity = (IBaseEntity) ose.Entity;

                if (lBaseEntity.IsValid == false)
                {
                    throw new BeerHouseDataException(string.Format("{0} is Not Valid", lBaseEntity.SetName), "", "");
                }
            }
        }
    }
}