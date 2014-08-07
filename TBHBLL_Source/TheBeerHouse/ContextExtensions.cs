namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [StandardModule]
    internal sealed class ContextExtensions
    {
        [CompilerGenerated, DebuggerStepThrough]
        private static ObjectStateEntry _Lambda$__3(ObjectStateEntry entry)
        {
            return entry;
        }

        public static IEnumerable<ObjectStateEntry> GetObjectStateEntries(this ObjectStateManager osm)
        {
            return osm.GetObjectStateEntries(EntityState.Modified | EntityState.Deleted | EntityState.Added | EntityState.Unchanged).Select<ObjectStateEntry, ObjectStateEntry>(new Func<ObjectStateEntry, ObjectStateEntry>(ContextExtensions._Lambda$__3));
        }
    }
}

