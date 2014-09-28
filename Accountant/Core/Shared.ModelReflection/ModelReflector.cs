using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Shared.Attributes;
using NewModel.Shared.FunctionsCombinator;
using NewModel.Shared.Utils;

namespace NewModel.Shared.ModelReflection
{
    public sealed class ModelReflector
    {
        readonly ModelEntityReflector mReflector =
            new ModelEntityReflector(
                new TypeNameFormatter(), 
                new DependencyResolver());

        public IEnumerable<ModelEntry> Reflect(params Type[] types)
        {
            var hashSet = types.Flatten(t => t.GetProperties().Where(
                p => p.IsDefined(typeof(NavigationAttribute), false)
                  || p.IsDefined(typeof(CompoundAttribute), false))
                .Select(p => p.PropertyType.ItemType() ?? p.PropertyType));
            return hashSet.OrderBy(t => t.Name).Select(mReflector.Reflect);
        }
    }
}
