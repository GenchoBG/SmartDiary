using System;
using System.Linq;
using AutoMapper;
using IntelliMood.Web.Infrastructure.Mapper;

namespace IntelliMood.Web.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var types = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains(nameof(IntelliMood)))
                .SelectMany(a => a.GetTypes())
                .ToList();

            var mappings = types
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            t
                                .GetInterfaces()
                                .Any(i => i.IsGenericType &&
                                          i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t
                        .GetInterfaces()
                        .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                        .GetGenericArguments()
                        .First()
                })
                .ToList();


            foreach (var mapping in mappings)
            {
                this.CreateMap(mapping.Source, mapping.Destination);
            }

            var customMappings = types
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            typeof(ICustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<ICustomMapping>()
                .ToList();

            foreach (var customMapping in customMappings)
            {
                customMapping.ConfigureMapping(this);
            }
        }
    }
}