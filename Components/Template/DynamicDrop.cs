using System;
using System.Collections.Generic;
using System.Dynamic;

using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using DotLiquid;

namespace Satrabel.DotLiquid
{
    public class DynamicDrop : Drop
    {
        private readonly dynamic model;

        public DynamicDrop(dynamic model)
        {
            this.model = GetValidModelType(model);
        }

        public override object BeforeMethod(string propertyName)
        {
            if (model == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                return null;
            }

            Type modelType = this.model.GetType();
            object value = null;
            if (modelType.Equals(typeof(Dictionary<string, object>)))
            {
                value = GetExpandoObjectValue(propertyName);
            }
            else
            {
                value = GetPropertyValue(propertyName);
            }
            return value;
        }

        private object GetExpandoObjectValue(string propertyName)
        {
            return (!this.model.ContainsKey(propertyName)) ?
                null :
                this.model[propertyName];
        }

        
        private object GetPropertyValue(string propertyName)
        {
            var property = this.model.GetType().GetProperty(propertyName);

            return (property == null) ?
                null :
                property.GetValue(this.model, null);
        }

        private static dynamic GetValidModelType(dynamic model)
        {
            if (model == null)
            {
                return null;
            }

            return model.GetType().Equals(typeof(ExpandoObject))
                ? new Dictionary<string, object>(model,StringComparer.InvariantCultureIgnoreCase)
                : model;
        }
    }
}