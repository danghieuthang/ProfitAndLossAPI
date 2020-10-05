using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using ProfitAndLoss.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute(
            
            ) : base() { }

       

        
    }
    public class InjectPropertySelector : DefaultPropertySelector
    {
        public InjectPropertySelector(bool preserveSetValues) : base(preserveSetValues)
        { }

        public override bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            var attr = propertyInfo.GetCustomAttribute<InjectAttribute>(inherit: true);
            return attr != null && propertyInfo.CanWrite
                    && (!PreserveSetValues
                    || (propertyInfo.CanRead && propertyInfo.GetValue(instance, null) == null));
        }
    }
}
