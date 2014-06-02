using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.ComponentModel;
using System.IO;
using DotNetNuke.Entities.Portals;
using System.Web.Hosting;

namespace Satrabel.Providers.TemplateEngine
{

    public abstract class TemplateProvider
    {
                
        #region "Shared/Static Methods"

        public static Dictionary<string, TemplateProvider> GetProviderList()
        {
            VerifyProviderInstalled();
            return ComponentFactory.GetComponents<TemplateProvider>();
        }

        public static TemplateProvider Instance(string FriendlyName)
        {
            VerifyProviderInstalled();
            return ComponentFactory.GetComponent<TemplateProvider>(FriendlyName);
        }


        private static void VerifyProviderInstalled()
        {

            if (ComponentFactory.GetComponentList<TemplateProvider>().Count() == 0)
            {
                ComponentFactory.InstallComponents(new ProviderInstaller("templateEngine", typeof(TemplateProvider)));
            }

        }

        #endregion

        public abstract string[] FileExtensions();

        public abstract string Execute(string virtualfile, object Model);

        public string Execute(string templatepath, string templatefile, object Model)
        {
            string dir = "";
            
            if (templatepath == "host")
                dir = "~/Portals/_Default/";
            else if (templatepath == "site")
                dir = "~" + PortalSettings.Current.HomeDirectory;
            else if (templatepath == "system")
                dir = "~/Desktopmodules/";

            
            if (string.IsNullOrEmpty(templatefile)) return "";
            string filename = templatefile;
            if (dir != "")
            {
                filename = dir + "widget/templates/" + templatefile;
            }
            return Execute(filename, Model);
        }

        public string Execute(string module, string storage, string template, string file, object Model)
        {
            string dir = "";

            if (storage == "host")
                dir = "~/Portals/_Default/";
            else if (storage == "site")
                dir = "~" + PortalSettings.Current.HomeDirectory;
            else if (storage == "system")
                dir = "~/Desktopmodules/";


            if (string.IsNullOrEmpty(file)) return "";
            string filename = file;
            if (dir != "")
            {
                filename = dir + module +"/templates/" + template +"/"+file;
            }
            return Execute(filename, Model);
        }

        public static string FindProviderAndExecute(string templatepath, string templatefile, object Model) {
            string FileExt = Path.GetExtension(templatefile).TrimStart('.');

            TemplateProvider provider = GetProviderList().Values.FirstOrDefault(p => p.FileExtensions().Contains(FileExt));
            return provider.Execute(templatepath, templatefile, Model);        
        }

        public static string FindProviderAndExecute(string module, string storage, string template, string file, object Model)
        {
            string FileExt = Path.GetExtension(file).TrimStart('.');

            TemplateProvider provider = GetProviderList().Values.FirstOrDefault(p => p.FileExtensions().Contains(FileExt));
            return provider.Execute(module, storage ,template , file, Model);
        }

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