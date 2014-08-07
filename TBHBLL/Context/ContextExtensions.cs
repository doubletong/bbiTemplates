using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;

internal static class ContextExtensions
{
    public static IEnumerable<ObjectStateEntry> GetObjectStateEntries(ObjectStateManager osm)
    {
        IEnumerable<ObjectStateEntry> typeEntries = from entry
                    in osm.GetObjectStateEntries(EntityState.Added | EntityState.Deleted
                            | EntityState.Modified | EntityState.Unchanged)
                    select entry;
        return typeEntries;
    }
}