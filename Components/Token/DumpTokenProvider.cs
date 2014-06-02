
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Satrabel.OpenBlocks.DataSource;
using DotNetNuke.Modules.Html;
using DotNetNuke.Common.Utilities;

using Satrabel.OpenBlocks.TemplateEngine;
using System.Collections;
using System.Reflection;

namespace Satrabel.OpenBlocks.Token
{

    public class DumpTokenProvider : TokenProvider
    {

        private readonly List<int> _hashListOfFoundElements;

        public override string Execute(Dictionary<string, string> parameters)
        {
            string path = parameters["templatepath"];
            if (string.IsNullOrEmpty(path)) return "";
            string filename = parameters["templatefile"];
            if (string.IsNullOrEmpty(filename)) return "";

            try
            {
                string ModelProvider = parameters["dataprovider"];
                string ModelConstructor = parameters["datasource"];

                var ModelParameters = new Dictionary<string, string>(parameters);
                ModelParameters.Remove("templatepath");
                ModelParameters.Remove("templatefile");
                ModelParameters.Remove("dataprovider");
                ModelParameters.Remove("datasource");

                var Model = DataSourceProvider.Instance(ModelProvider).Invoke(ModelConstructor, ModelParameters);
                return TemplateProvider.FindProviderAndExecute(path, filename, Dump(Model));

            }
            catch (Exception ex)
            {
                return "Error : " + path + filename + " " + ex.Message;
            }
        }


        public static PropertyModel Dump(object Model)
        {
            return new PropertyModel();

        }


        private void DumpElement(object element, PropertyModel dump)
        {
            if (element == null || element is ValueType || element is string)
            {
                FormatValue(element, dump);
            }
            else
            {
                var objectType = element.GetType();
                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    dump.Type = objectType.FullName;
                    _hashListOfFoundElements.Add(element.GetHashCode());
                }

                var enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            PropertyModel elem = new PropertyModel();
                            dump.Properties.Add(elem);
                            DumpElement(item, elem);
                        }
                        else
                        {
                            if (!AlreadyTouched(item))
                                DumpElement(item, dump);
                            else
                                dump.Value = string.Format("{{{0}}} <-- bidirectional reference found", item.GetType().FullName);
                        }
                    }
                }
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;

                        if (fieldInfo == null && propertyInfo == null)
                            continue;

                        if (propertyInfo != null && propertyInfo.GetIndexParameters().Length > 0)
                            continue;

                        if (propertyInfo != null && propertyInfo.DeclaringType.FullName == "DotLiquid.DropBase")
                            continue;

                        var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                        object value = fieldInfo != null
                                           ? fieldInfo.GetValue(element)
                                           : propertyInfo.GetValue(element, null);

                        if (type.IsValueType || type == typeof(string) || value == null)
                        {
                            dump.Name = memberInfo.Name;
                            FormatValue(value, dump);
                        }
                        else
                        {
                            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
                            dump.Name = memberInfo.Name;
                            dump.Value = isEnumerable ? "..." : "{ }";

                            var alreadyTouched = !isEnumerable && AlreadyTouched(value);
                            if (!alreadyTouched)
                                DumpElement(value, dump);
                            else
                                dump.Value = string.Format("{{{0}}} <-- bidirectional reference found", value.GetType().FullName);
                        }
                    }
                }

                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {

                }
            }

        }

        private bool AlreadyTouched(object value)
        {
            var hash = value.GetHashCode();
            for (var i = 0; i < _hashListOfFoundElements.Count; i++)
            {
                if (_hashListOfFoundElements[i] == hash)
                    return true;
            }
            return false;
        }

        private void FormatValue(object o, PropertyModel dump)
        {
            if (o == null)
                dump.Value = "null";

            if (o is DateTime)
                dump.Value = ((DateTime)o).ToShortDateString();

            if (o is string)
                dump.Value = string.Format("\"{0}\"", o);

            if (o is ValueType)
                dump.Value = o.ToString();

            if (o is IEnumerable)
                dump.Value = "...";

            dump.Value = "{ }";
        }
    }
    /*
    public class xDumpModel : Drop
    {
        public List<PropertyModel> Properties { get; set; }       
    }
     * */

    public class PropertyModel 
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Simple { get; set; }
        public bool List { get; set; }

        public List<PropertyModel> Properties { get; set; }

        public PropertyModel Property { get; set; } 
    }

    
}