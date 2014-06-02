using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Web.Client.ClientResourceManagement;
using System.Web.UI;
using System.IO;
using DotNetNuke.Web.Client;


namespace Satrabel.Providers.TemplateEngine
{
    
    public class TemplateHelper
    {
        private string VirtualPath;

        public TemplateHelper(string VirtualPath)
        {
            this.VirtualPath = VirtualPath;
        }

        public void RegisterScript(string filePath) {
            if (!filePath.StartsWith("http") && !filePath.StartsWith("/"))
                filePath = VirtualPath + filePath;
            
            ClientResourceManager.RegisterScript( (Page)HttpContext.Current.CurrentHandler, filePath, FileOrder.Js.DefaultPriority);
        }

        public void RegisterStyleSheet(string filePath)
        {
            if (!filePath.StartsWith("http") && !filePath.StartsWith("/"))
                filePath = VirtualPath + filePath;

            ClientResourceManager.RegisterStyleSheet((Page)HttpContext.Current.CurrentHandler, filePath, FileOrder.Css.ModuleCss);     
        }

    }
}