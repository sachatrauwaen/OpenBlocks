using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Web.Razor;
using System.IO;
using DotNetNuke.Entities.Portals;
using DotNetNuke.UI.Modules;
using DotNetNuke.Framework;
using System.Reflection;
using Satrabel.DataSource;
using Satrabel.Providers.TemplateEngine;

namespace Satrabel.Token
{
    public class WidgetTokenProvider : TokenProvider
    {
        public static string ExecuteWidget(Dictionary<string, string> parameters)
        {
            var ModelParameters = new Dictionary<string, string>(parameters);
            string path = "";
            if (parameters.ContainsKey("templatepath"))
            {
                path = parameters["templatepath"];
                ModelParameters.Remove("templatepath");
            }
            string filename = "";
            if (parameters.ContainsKey("templatefile"))
            {
                filename = parameters["templatefile"];
                ModelParameters.Remove("templatefile");
            //if (string.IsNullOrEmpty(filename)) return "";
            }
                
            try
            {
                string ModelType = "";
                if (parameters.ContainsKey("datatype"))
                {
                    ModelType = parameters["datatype"];
                    ModelParameters.Remove("datatype");
                }
                string ModelProvider = "";
                if (parameters.ContainsKey("dataprovider"))
                {
                    ModelProvider = parameters["dataprovider"];
                    ModelParameters.Remove("dataprovider");
                }
                string ModelConstructor = "";
                if (parameters.ContainsKey("datasource"))
                {
                    ModelConstructor = parameters["datasource"];
                    ModelParameters.Remove("datasource");
                }

                DataSourceProvider provider = null;
                if (!string.IsNullOrEmpty(ModelType))
                {
                    Object obj = Activator.CreateInstance(Type.GetType(ModelType));
                    provider = obj as DataSourceProvider;
                    
                }
                else if (!string.IsNullOrEmpty(ModelProvider))
                {
                    provider = DataSourceProvider.Instance(ModelProvider);
  
                }
                object Model = null;
                if (provider != null)
                {
                    Model = provider.Invoke(ModelConstructor, ModelParameters);
                }
                if (filename != "")
                {
                    return TemplateProvider.FindProviderAndExecute(path, filename, Model);
                }
                else
                {
                    return ObjectDumper.Dump(Model, 4);   
                    
                }
                /*
                var engine = new RazorEngine(dir + filename, new ModuleInstanceContext(), null);
                var writer = new StringWriter();
                var model = Reflection.GetProperty(engine.Webpage.GetType(), "Model", engine.Webpage);
                var member = engine.Webpage.GetType().GetMember("Model")[0];
                Type t = ((PropertyInfo)member).PropertyType;


                model = Reflection.CreateInstance(t);
                engine.Render(writer);
                return writer.ToString();
                 */
            }
            catch (Exception ex)
            {
                return "Error : " + path + filename + " " + ex.Message;
            }            
        }

        public override string Execute(Dictionary<string, string> parameters) {
            return ExecuteWidget(parameters);
        }
    }
}