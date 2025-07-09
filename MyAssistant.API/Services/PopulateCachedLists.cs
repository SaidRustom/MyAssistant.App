using MediatR;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Base;
using MyAssistant.Persistence;
using System.Reflection;

namespace MyAssistant.API.Services
{
    /// <summary>
    /// Responsible for populating and caching lookup lists from the database.
    /// 
    /// This class uses reflection to discover all relevant lookup list types, queries their data from the 
    /// Entity Framework DbContext, and stores the results into each list's static cache property. 
    /// This enables efficient reuse of lookup data across the application without repeated database fetches.
    /// </summary>
    public class PopulateCachedListsCommand : IRequest { }

    public class PopulateCachedListsService(MyAssistantDbContext context) : IRequestHandler<PopulateCachedListsCommand>
    {
        protected readonly MyAssistantDbContext _context = context;

        public async Task Handle(PopulateCachedListsCommand cmd, CancellationToken token)
        {
            // Get the runtime type of the DbContext instance (_context)
            var contextType = _context.GetType();

            // Find all non-abstract types with name containing 'TypeList' in 'Lookups' namespace
            var lookupListTypes = typeof(LookupBaseList<>).Assembly
                .GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    t.Name.Contains("TypeList") &&
                    t.Namespace.Contains("Lookups")
                );

            // Iterate through each TypeList-based lookup list type found
            foreach (var listType in lookupListTypes)
            {
                // listType should inherit from LookupBaseList<T>, so get generic T
                var baseType = listType.BaseType!;
                var lookupType = baseType.GetGenericArguments()[0];

                // Find a DbSet<T> property in the context for the required lookup type
                var dbSetProp = contextType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(p =>
                        p.PropertyType.IsGenericType &&
                        p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                        p.PropertyType.GetGenericArguments()[0] == lookupType
                    );
                if (dbSetProp == null) continue; // No DbSet for this type found, skip

                // Get the actual DbSet<T> property value from the context
                var dbSetValue = dbSetProp.GetValue(_context);

                // Use reflection to get and build a strongly-typed ToListAsync<T>(IQueryable<T>, CancellationToken) method
                var toListAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .First(m => m.Name == "ToListAsync" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(lookupType);

                // Start ToListAsync on the DbSet<T>; note, it uses CancellationToken.None
                // (You might want to pass the 'token' parameter here instead!)
                var toListTask = (Task)toListAsyncMethod.Invoke(null, new object[] { dbSetValue, CancellationToken.None })!;
                await toListTask.ConfigureAwait(false);

                // Get the result (List<T>) ToListAsync
                var resultProp = toListTask.GetType().GetProperty("Result");
                var data = resultProp.GetValue(toListTask);

                // Create a new instance of the listType
                var newList = Activator.CreateInstance(listType);

                // Call AddRange(List<T>) to populate the new List instance with data
                var addRangeMethod = listType.GetMethod("AddRange", new[] { typeof(IEnumerable<>).MakeGenericType(lookupType) })!;
                addRangeMethod.Invoke(newList, new[] { data });

                // Get the static CachedList property (could be defined on listType or its base)
                var cachedListProp = listType.GetProperty("CachedList", BindingFlags.Public | BindingFlags.Static)
                    ?? baseType.GetProperty("CachedList", BindingFlags.Public | BindingFlags.Static);

                // Store the populated new list in the CachedList property (overwrites previously cached data)
                cachedListProp.SetValue(null, newList);
            }
        }
    }
}
