#if !(UNITY_WSA && ENABLE_DOTNET)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zenject
{
  [NoReflectionBaking]
  public class ConventionBindInfo
  {
    private readonly List<Func<Type, bool>> _typeFilters = new();
    private readonly List<Func<Assembly, bool>> _assemblyFilters = new();

#if ZEN_MULTITHREADING
        readonly object _locker = new object();
#endif
    private static readonly Dictionary<Assembly, Type[]> _assemblyTypeCache = new();

    public void AddAssemblyFilter(Func<Assembly, bool> predicate)
    {
      _assemblyFilters.Add(predicate);
    }

    public void AddTypeFilter(Func<Type, bool> predicate)
    {
      _typeFilters.Add(predicate);
    }

    private IEnumerable<Assembly> GetAllAssemblies()
    {
      // This seems fast enough that it's not worth caching
      // We also want to allow dynamically loading assemblies
      return AppDomain.CurrentDomain.GetAssemblies();
    }

    private bool ShouldIncludeAssembly(Assembly assembly)
    {
      return _assemblyFilters.All(predicate => predicate(assembly));
    }

    private bool ShouldIncludeType(Type type)
    {
      return _typeFilters.All(predicate => predicate(type));
    }

    private Type[] GetTypes(Assembly assembly)
    {
      Type[] types;

#if ZEN_MULTITHREADING
            lock (_locker)
#endif
      {
        // This is much faster than calling assembly.GetTypes() every time
        if (!_assemblyTypeCache.TryGetValue(assembly, out types))
        {
          types = assembly.GetTypes();
          _assemblyTypeCache[assembly] = types;
        }
      }

      return types;
    }

    public List<Type> ResolveTypes()
    {
      return GetAllAssemblies()
        .Where(ShouldIncludeAssembly)
        .SelectMany(assembly => GetTypes(assembly))
        .Where(ShouldIncludeType).ToList();
    }
  }
}

#endif