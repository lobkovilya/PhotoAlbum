using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace PhotoAlbum.BLL.Contracts.Mapping
{
    public class AutoMapperConfig
    {
        public static void Configure(params Assembly[] assemblies)
        {
            Mapper.Initialize(cfg =>
            {
                foreach (Assembly assembly in assemblies.Distinct())
                {
                    IEnumerable<Type> profileTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Profile)));
                    foreach (Type type in profileTypes)
                    {
                        cfg.AddProfile((Profile)Activator.CreateInstance(type));
                    }
                }
            });
        }
    }
}