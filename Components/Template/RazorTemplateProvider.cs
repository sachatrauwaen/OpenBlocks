using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Web.Razor;
using System.IO;
using DotNetNuke.Entities.Portals;
using DotNetNuke.UI.Modules;
using System.Web.Hosting;
using System.Text.RegularExpressions;
using Satrabel.OpenBlocks.DataSource;
using DotNetNuke.UI.Utilities;
using System.Reflection;

namespace Satrabel.OpenBlocks.TemplateEngine
{
    public class RazorTemplateProvider : TemplateProvider
    {
        public override string Execute(string virtualfile, object Model)
        {            
            try
            {
                if (!File.Exists(HostingEnvironment.MapPath(virtualfile))){
                    return String.Format("File {0} don't exist", virtualfile);
                }

                var engine = new RazorEngine(virtualfile, new ModuleInstanceContext(), null);
                var WebPage = engine.Webpage as TemplateWebPage;
                if ((WebPage != null))
                {
                    WebPage.Template = new TemplateHelper(WebPage.VirtualPath);
                }

                var writer = new StringWriter();
                
                //var model = Reflection.GetProperty(engine.Webpage.GetType(), "Model", engine.Webpage);                
                //var member = engine.Webpage.GetType().GetMember("Model")[0];
                //Type t = ((PropertyInfo)member).PropertyType;
                //model = Reflection.CreateInstance(t);
               
                if (Model == null)
                {
                    engine.Render(writer);
                }
                else
                {
                    Object obj = engine.Webpage;
                    PropertyInfo prop = obj.GetType().GetProperty("Model", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(obj, Model, null);
                    }
                    //Reflection.SetProperty(t.DeclaringType, "Model", engine.Webpage, new object[] {Model });

                  
                    engine.Render(writer);
                }

                return writer.ToString();
            }
            catch (Exception ex)
            {
                return " " + virtualfile + " " + ex.Message + " " + ex.StackTrace;
            }            
        }

        public override string[] FileExtensions() {
            return new string[] { "cshtml", "vbhtml" };
        }
    }

}