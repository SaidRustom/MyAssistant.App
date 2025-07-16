using System.Reflection;
using AutoMapper;
using MyAssistant.Domain.Base;
using MyAssistant.Shared;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            ApplyMappingsFromAssembly(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Automatically map objects using the interface IMapWith<T>
        /// </summary>
        private void ApplyMappingsFromAssembly(ICollection<Assembly> assembly)
        {
            foreach (var assemblyName in assembly)
            {
                var types = assemblyName.GetExportedTypes();

                foreach (var type in types)
                {
                    // find all types implementing IMapWith<T> interface
                    var mapInterfaces = type.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>));
                    foreach (var mapInterface in mapInterfaces)
                    {
                        var argumentType = mapInterface.GetGenericArguments()[0];
                        // Register the map in both directions
                        CreateMap(argumentType, type).ReverseMap();
                    }

                    // ALSO: map all classes inheriting from LookupBase<T> to LookupDto
                    var baseType = type;
                    while (baseType != null && baseType != typeof(object))
                    {
                        if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(LookupBase<>))
                        {
                            CreateMap(type, typeof(LookupDto)); 
                            break;
                        }
                        baseType = baseType.BaseType;
                    }
                }
            }
        }
    }
}
