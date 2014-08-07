namespace TheBeerHouse.BLL.Store
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using TheBeerHouse.BLL;

    /// <summary>
    /// The Entity Repository Class. Contains methods that utilize the Entity Framework to 
    /// interact with the database.
    /// </summary>
    /// <remarks></remarks>
    public class DepartmentRepository : BaseShoppingCartRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vDepartment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AddDepartment(Department vDepartment)
        {
            bool AddDepartment;
            try
            {
                if (vDepartment.EntityState == EntityState.Detached)
                {
                    this.Shoppingctx.AddToDepartments(vDepartment);
                }
                base.PurgeCacheItems(this.CacheKey);
                AddDepartment = this.Shoppingctx.SaveChanges() > 0;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                AddDepartment = false;
                ProjectData.ClearProjectError();
                return AddDepartment;
                ProjectData.ClearProjectError();
            }
            return AddDepartment;
        }

        private bool ChangeDeletedState(Department vDepartment, bool vState)
        {
            bool ChangeDeletedState;
            vDepartment.Active = vState;
            vDepartment.UpdatedDate = DateAndTime.Now;
            vDepartment.UpdatedBy = BaseRepository.CurrentUserName;
            try
            {
                this.Shoppingctx.SaveChanges();
                base.PurgeCacheItems(this.CacheKey);
                ChangeDeletedState = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                this.ActiveExceptions.Add(Conversions.ToString(vDepartment.DepartmentID), ex);
                ChangeDeletedState = false;
                ProjectData.ClearProjectError();
                return ChangeDeletedState;
                ProjectData.ClearProjectError();
            }
            return ChangeDeletedState;
        }

        /// <summary>
        /// </summary>
        /// <param name="vDepartment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteDepartment(Department vDepartment)
        {
            return this.ChangeDeletedState(vDepartment, false);
        }

        /// <summary>
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Department GetDepartmentById(int DepartmentId)
        {
            ParameterExpression VB$t_ref$S0;
            _Closure$__38 $VB$Closure_ClosureVariable_1E_C = new _Closure$__38();
            $VB$Closure_ClosureVariable_1E_C.$VB$Local_DepartmentId = DepartmentId;
            return this.Shoppingctx.Departments.Where<Department>(Expression.Lambda<Func<Department, bool>>(Expression.Equal(Expression.Property(VB$t_ref$S0 = Expression.Parameter(typeof(Department), "lai"), (MethodInfo) methodof(Department.get_DepartmentID)), Expression.Field(Expression.Constant($VB$Closure_ClosureVariable_1E_C, typeof(_Closure$__38)), fieldof(_Closure$__38.$VB$Local_DepartmentId)), true, null), new ParameterExpression[] { VB$t_ref$S0 })).FirstOrDefault<Department>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetDepartmentCount()
        {
            ParameterExpression VB$t_ref$S0;
            return this.Shoppingctx.Departments.Select<Department, Department>(Expression.Lambda<Func<Department, Department>>(VB$t_ref$S0 = Expression.Parameter(typeof(Department), "lai"), new ParameterExpression[] { VB$t_ref$S0 })).Count<Department>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Department> GetDepartments()
        {
            ParameterExpression VB$t_ref$S0;
            return base.Shoppingctx.Departments.Select<Department, Department>(Expression.Lambda<Func<Department, Department>>(VB$t_ref$S0 = Expression.Parameter(typeof(Department), "lDepartment"), new ParameterExpression[] { VB$t_ref$S0 })).ToList<Department>();
        }

        public bool UnDeleteDepartment(Department vDepartment)
        {
            return this.ChangeDeletedState(vDepartment, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="vDepartment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool UpdateDepartment(Department vDepartment)
        {
            return this.AddDepartment(vDepartment);
        }

        [CompilerGenerated]
        internal class _Closure$__38
        {
            public int $VB$Local_DepartmentId;

            [DebuggerNonUserCode]
            public _Closure$__38()
            {
            }

            [DebuggerNonUserCode]
            public _Closure$__38(DepartmentRepository._Closure$__38 other)
            {
                if (other != null)
                {
                    this.$VB$Local_DepartmentId = other.$VB$Local_DepartmentId;
                }
            }
        }
    }
}

