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
using Satrabel.DataSource;
using DotNetNuke.UI.Utilities;
using System.Reflection;

namespace Satrabel.Providers.TemplateEngine
{
    public class HtmlTemplateProvider : TemplateProvider
    {
        public override string Execute(string virtualfile, object Model)
        {            
            try
            {
                
                if (Model == null)
                {
                    
                }
                else
                {
                    /*
                    Object obj = engine.Webpage;
                    PropertyInfo prop = obj.GetType().GetProperty("Model", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(obj, Model, null);
                    }
                    */
                    
                }

                return File.ReadAllText(HostingEnvironment.MapPath(virtualfile));
            }
            catch (Exception ex)
            {
                return " " + virtualfile + " " + ex.Message + " " + ex.StackTrace;
            }            
        }

        public override string[] FileExtensions() {
            return new string[] { "htm", "html" };
        }
    }

}