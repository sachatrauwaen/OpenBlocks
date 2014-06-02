using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Web.Razor;
using System.IO;
using DotNetNuke.Entities.Portals;
using DotNetNuke.UI.Modules;
using DotLiquid.FileSystems;
using DotLiquid;
using System.Web.Hosting;
using DotLiquid.Exceptions;
using System.Text.RegularExpressions;
using Satrabel.DataSource;
using DotLiquid.NamingConventions;

namespace Satrabel.Providers.TemplateEngine
{
    public class LiquidTemplateProvider : TemplateProvider
    {
        public override string Execute(string virtualfile, object Model)
        {            
            try
            {
                Template.NamingConvention = new CSharpNamingConvention();
                Template template = Template.Parse(VirtualPathProviderHelper.Load(virtualfile));
                return template.Render(Hash.FromAnonymousObject(new { Model = Model, ModelToJson = Model.ToJson(), ModelDump = ObjectDumper.Dump(Model) }));
            }
            catch (Exception ex)
            {
                return " " + virtualfile + " " + ex.Message;
            }            
        }

        public override string[] FileExtensions() {
            return new string[] { "liquid" };
        }
    }

    public class VirtualPathProviderFileSystem : IFileSystem
    {
        public string FileNameFormat
        {
            get { return "_{0}.liquid"; }
        }

        protected VirtualPathProviderFileSystem()
        {
            
        }

        public string ReadTemplateFile(Context context, string templateName)
        {
            var templatePath = (string)context[templateName];

            if (templatePath == null || !Regex.IsMatch(templatePath, @"^[a-zA-Z0-9_]+$"))
                throw new FileSystemException("Error - Illegal template name '{0}'", templatePath);

            string fullPath = Path.Combine(PortalSettings.Current.HomeDirectoryMapPath, string.Format(FileNameFormat, templatePath));
            if (!HostingEnvironment.VirtualPathProvider.FileExists(fullPath))
            {
                throw new FileSystemException("Error - No such template. Looked in the following locations:<br />{0}", string.Join("<br />", ""));
            }

                

            return VirtualPathProviderHelper.Load(fullPath);
        }
    }

    public static class VirtualPathProviderHelper
    {

        public static string Load(string virtualPath)
        {
            if (!HostingEnvironment.VirtualPathProvider.FileExists(virtualPath))
                return null;

            try
            {
                var virtualFile = HostingEnvironment.VirtualPathProvider.GetFile(virtualPath);
                using (var stream = virtualFile.Open())
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }

        }
    }

}