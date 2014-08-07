namespace TheBeerHouse.BLL.Store
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.EntityClient;
    using System.Data.Objects;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TheBeerHouse;
    using TheBeerHouse.BLL;

    /// <summary>
    /// There are no comments for ShoppingEntities in the schema.
    /// </summary>
    public class ShoppingEntities : ObjectContext
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private ObjectQuery<Department> _Departments;
        private ObjectQuery<OrderItem> _OrderItems;
        private ObjectQuery<Order> _Orders;
        private ObjectQuery<OrderStatus> _OrderStatuses;
        private ObjectQuery<Product> _Products;
        private ObjectQuery<ShippingMethod> _ShippingMethods;

        /// <summary>
        /// Initializes a new ShoppingEntities object using the connection string found in the 'ShoppingEntities' section of the application configuration file.
        /// </summary>
        public ShoppingEntities() : base("name=ShoppingEntities", "ShoppingEntities")
        {
            base.SavingChanges += new EventHandler(this.ShoppingEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new ShoppingEntities object.
        /// </summary>
        public ShoppingEntities(EntityConnection connection) : base(connection, "ShoppingEntities")
        {
            base.SavingChanges += new EventHandler(this.ShoppingEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        /// <summary>
        /// Initialize a new ShoppingEntities object.
        /// </summary>
        public ShoppingEntities(string connectionString) : base(connectionString, "ShoppingEntities")
        {
            base.SavingChanges += new EventHandler(this.ShoppingEntities_SavingChanges);
            List<WeakReference> VB$t_ref$L0 = __ENCList;
            lock (VB$t_ref$L0)
            {
                __ENCList.Add(new WeakReference(this));
            }
        }

        [DebuggerStepThrough, CompilerGenerated]
        private static bool _Lambda$__24(ObjectStateEntry entry)
        {
            return (entry.Entity is IBaseEntity);
        }

        /// <summary>
        /// There are no comments for Departments in the schema.
        /// </summary>
        public void AddToDepartments(Department department)
        {
            base.AddObject("Departments", department);
        }

        /// <summary>
        /// There are no comments for OrderItems in the schema.
        /// </summary>
        public void AddToOrderItems(OrderItem orderItem)
        {
            base.AddObject("OrderItems", orderItem);
        }

        /// <summary>
        /// There are no comments for Orders in the schema.
        /// </summary>
        public void AddToOrders(Order order)
        {
            base.AddObject("Orders", order);
        }

        /// <summary>
        /// There are no comments for OrderStatuses in the schema.
        /// </summary>
        public void AddToOrderStatuses(OrderStatus orderStatus)
        {
            base.AddObject("OrderStatuses", orderStatus);
        }

        /// <summary>
        /// There are no comments for Products in the schema.
        /// </summary>
        public void AddToProducts(Product product)
        {
            base.AddObject("Products", product);
        }

        /// <summary>
        /// There are no comments for ShippingMethods in the schema.
        /// </summary>
        public void AddToShippingMethods(ShippingMethod shippingMethod)
        {
            base.AddObject("ShippingMethods", shippingMethod);
        }

        /// <summary>
        /// Checks each object that is either new to the Context or has been updated 
        /// to verify each is valid. If one does not return true when the Entity's 
        /// IsValid method is called a BeerHouseDataException is thrown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This is an event handler that will get called when ever the SaveChanges method is called.</remarks>
        private void ShoppingEntities_SavingChanges(object sender, EventArgs e)
        {
            List<ObjectStateEntry>.Enumerator VB$t_struct$L0;
            List<ObjectStateEntry> typeEntries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added).Where<ObjectStateEntry>(new Func<ObjectStateEntry, bool>(ShoppingEntities._Lambda$__24)).ToList<ObjectStateEntry>();
            try
            {
                VB$t_struct$L0 = typeEntries.GetEnumerator();
                while (VB$t_struct$L0.MoveNext())
                {
                    ObjectStateEntry ose = VB$t_struct$L0.Current;
                    IBaseEntity lBaseEntity = (IBaseEntity) ose.Entity;
                    if (!lBaseEntity.IsValid)
                    {
                        throw new BeerHouseDataException(string.Format("{0} is Not Valid", lBaseEntity.SetName), "", "");
                    }
                }
            }
            finally
            {
                VB$t_struct$L0.Dispose();
            }
        }

        /// <summary>
        /// There are no comments for Departments in the schema.
        /// </summary>
        public ObjectQuery<Department> Departments
        {
            get
            {
                if (this._Departments == null)
                {
                    this._Departments = base.CreateQuery<Department>("[Departments]", new ObjectParameter[0]);
                }
                return this._Departments;
            }
        }

        /// <summary>
        /// There are no comments for OrderItems in the schema.
        /// </summary>
        public ObjectQuery<OrderItem> OrderItems
        {
            get
            {
                if (this._OrderItems == null)
                {
                    this._OrderItems = base.CreateQuery<OrderItem>("[OrderItems]", new ObjectParameter[0]);
                }
                return this._OrderItems;
            }
        }

        /// <summary>
        /// There are no comments for Orders in the schema.
        /// </summary>
        public ObjectQuery<Order> Orders
        {
            get
            {
                if (this._Orders == null)
                {
                    this._Orders = base.CreateQuery<Order>("[Orders]", new ObjectParameter[0]);
                }
                return this._Orders;
            }
        }

        /// <summary>
        /// There are no comments for OrderStatuses in the schema.
        /// </summary>
        public ObjectQuery<OrderStatus> OrderStatuses
        {
            get
            {
                if (this._OrderStatuses == null)
                {
                    this._OrderStatuses = base.CreateQuery<OrderStatus>("[OrderStatuses]", new ObjectParameter[0]);
                }
                return this._OrderStatuses;
            }
        }

        /// <summary>
        /// There are no comments for Products in the schema.
        /// </summary>
        public ObjectQuery<Product> Products
        {
            get
            {
                if (this._Products == null)
                {
                    this._Products = base.CreateQuery<Product>("[Products]", new ObjectParameter[0]);
                }
                return this._Products;
            }
        }

        /// <summary>
        /// There are no comments for ShippingMethods in the schema.
        /// </summary>
        public ObjectQuery<ShippingMethod> ShippingMethods
        {
            get
            {
                if (this._ShippingMethods == null)
                {
                    this._ShippingMethods = base.CreateQuery<ShippingMethod>("[ShippingMethods]", new ObjectParameter[0]);
                }
                return this._ShippingMethods;
            }
        }
    }
}

