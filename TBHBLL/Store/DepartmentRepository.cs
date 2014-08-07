using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BBICMS.Store
{
    public class DepartmentRepository : BaseShoppingCartRepository
    {
        #region " BLL/DAL Methods "

        public List<Department> GetDepartments()
        {
            return (from lDepartment in Shoppingctx.Departments
                    select lDepartment).ToList();
        }

        public Department GetDepartmentById(int DepartmentId)
        {
            return (from lDepartment in Shoppingctx.Departments.Include("Products")
                    where lDepartment.DepartmentID == DepartmentId
                    select lDepartment).FirstOrDefault();
        }

        public int GetDepartmentCount()
        {
            return (from lDepartment in Shoppingctx.Departments
                    select lDepartment).Count();
        }

        public Department AddDepartment(int vDepartmentID, DateTime vAddedDate, string vTitle, int vImportance,
                                        string vDescription, string vImageUrl, DateTime vUpdatedDate)
        {
            Department lDepartment = default(Department);

            if (vDepartmentID > 0)
            {
                lDepartment = GetDepartmentById(vDepartmentID);

                lDepartment.DepartmentID = vDepartmentID;
                lDepartment.AddedDate = vAddedDate;
                lDepartment.Title = vTitle;
                lDepartment.Importance = vImportance;
                lDepartment.Description = vDescription;
                lDepartment.ImageUrl = vImageUrl;
                lDepartment.UpdatedDate = vUpdatedDate;

                lDepartment.UpdatedBy = Helpers.CurrentUserName;
            }
            else
            {
                lDepartment = Department.CreateDepartment(vDepartmentID, vAddedDate,
                                                          Helpers.CurrentUserName, vTitle, vImportance, vUpdatedDate,
                                                          true);

                lDepartment.Title = vTitle;
                lDepartment.Description = vDescription;

                lDepartment.ImageUrl = vImageUrl;
            }


            return AddDepartment(lDepartment);
        }

        public Department AddDepartment(Department vDepartment)
        {
            try
            {
                if (vDepartment.EntityState == EntityState.Detached)
                {
                    Shoppingctx.AddToDepartments(vDepartment);
                }
                base.PurgeCacheItems(CacheKey);

                return Shoppingctx.SaveChanges() > 0 ? vDepartment : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vDepartment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteDepartment(Department vDepartment)
        {
            return ChangeDeletedState(vDepartment, false);
        }

        public bool UnDeleteDepartment(Department vDepartment)
        {
            return ChangeDeletedState(vDepartment, true);
        }

        private bool ChangeDeletedState(Department vDepartment, bool vState)
        {
            vDepartment.Active = vState;
            vDepartment.UpdatedDate = DateTime.Now;
            vDepartment.UpdatedBy = CurrentUserName;

            try
            {
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vDepartment.DepartmentID.ToString(), ex);

                return false;
            }
        }


        public bool RemoveDepartment(int vDepartmentId)
        {
            return RemoveDepartment(GetDepartmentById(vDepartmentId));
        }
        public bool RemoveDepartment(Department vDepartment)
        {

            try
            {
                Shoppingctx.DeleteObject(vDepartment);
                Shoppingctx.SaveChanges();
                base.PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(vDepartment.DepartmentID.ToString(), ex);

                return false;

            }
        }

        #endregion
    }
}