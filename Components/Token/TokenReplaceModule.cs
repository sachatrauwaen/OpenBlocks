using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Instrumentation;
using System.Text.RegularExpressions;
using System.Xml;
using DotNetNuke.Modules.Html;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel;
using DotNetNuke.Framework;
using System.Web.UI;
using DotNetNuke.UI.WebControls;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Upgrade;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Definitions;
using DotNetNuke.Entities.Tabs;

namespace Satrabel.OpenBlocks.Token
{
    public class TokenReplaceModule : IHttpModule
    {
         public string ModuleName
        {
            get
            {
                return "TokenModule";
            }
        }

        #region IHttpModule Members

        public void Init(HttpApplication application)
        {
            
            //application.BeginRequest += OnBeginRequest;
            //UrlRewriterLogging logger = new UrlRewriterLogging();
            //application.EndRequest += logger.OnEndRequest;
            application.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
            //application.Error += OnError;
            
            
        }

        public void Dispose()
        {
        }

        #endregion

        private static bool PagesChecked = false;
        public void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            try
            {
                //First check if we are upgrading/installing or if it is a non-page request
                var app = (HttpApplication)sender;
                HttpRequest request = app.Request;

                //First check if we are upgrading/installing
                if (request.Url.LocalPath.ToLower().Contains("install.aspx")
                        || request.Url.LocalPath.ToLower().Contains("upgradewizard.aspx")
                        || request.Url.LocalPath.ToLower().Contains("installwizard.aspx"))
                {
                    return;
                }

                //exit if a request for a .net mapping that isn't a content page is made i.e. axd
                if (request.Url.LocalPath.ToLower().EndsWith(".aspx") == false && request.Url.LocalPath.ToLower().EndsWith(".asmx") == false &&
                    request.Url.LocalPath.ToLower().EndsWith(".ashx") == false)
                {
                    return;
                }


                if (request.HttpMethod != "GET") {
                    //return;
                }

                if (HttpContext.Current != null)
                {
                    HttpContext context = HttpContext.Current;
                    if ((context == null))
                    {
                        return;
                    }
                    var page = context.Handler as DotNetNuke.Framework.CDefault;
                    if ((page == null))
                    {
                        return;
                    }
                    page.PreRender += Page_Prerender;
                    page.Load += Page_Load;



                    try
                    {
                        /*
                        ResponseFilterStream filter = new ResponseFilterStream(app.Response.Filter);
                        filter.TransformString += filter_TransformString;
                        app.Response.Filter = filter;
                        */
                        /*
                        if (!PagesChecked)
                        {
                            var portalController = new PortalController();
                            var portal = portalController.GetPortal(PortalSettings.Current.PortalId);

                            var tabController = new TabController();
                            TabInfo adminPage = tabController.GetTab(portal.AdminTabId, portal.PortalID, false);


                            
                            var newPage = Upgrade.AddAdminPage(portal, "Template Studio", "Open Template Studio",
                                                            "~/Icons/Sigma/Files_16X16_Standard.png",
                                                            "~/Icons/Sigma/Files_32X32_Standard.png",
                                                             true);
                            if (newPage != null) { 
                            var moduleDefId = GetModuleDefinition("OpenTemplateStudio", "OpenTemplateStudio");
                            int TabModuleID = Upgrade.AddModuleToPage(newPage, moduleDefId, "Template Studio", "~/Icons/Sigma/Files_16X16_Standard.png", true);
                            if (TabModuleID > 0)
                            {

                                var moduleController = new ModuleController();

                                foreach (var module in moduleController.GetTabModules(newPage.TabID).Values)
                                {
                                    moduleController.UpdateTabModuleSetting(module.TabModuleID, "hideadminborder", "true");
                                }


                            }

                            }
                        }
                        */
                    }
                    catch { }
                    PagesChecked = true;
                    
                }
            }
            catch (Exception ex)
            {
                /*
                var objEventLog = new EventLogController();
                var objEventLogInfo = new LogInfo();
                objEventLogInfo.AddProperty("Analytics.AnalyticsModule", "OnPreRequestHandlerExecute");
                objEventLogInfo.AddProperty("ExceptionMessage", ex.Message);
                objEventLogInfo.LogTypeKey = EventLogController.EventLogType.HOST_ALERT.ToString();
                objEventLog.AddLog(objEventLogInfo);
                 
                DnnLog.Error(objEventLogInfo);
                */
                DnnLog.Error(ex);
            }
        }

        private static int GetModuleDefinition(string desktopModuleName, string moduleDefinitionName)
        {
            // get desktop module
            var desktopModule = DesktopModuleController.GetDesktopModuleByModuleName(desktopModuleName, Null.NullInteger);
            if (desktopModule == null)
            {
                return -1;
            }

            // get module definition
            ModuleDefinitionInfo objModuleDefinition = ModuleDefinitionController.GetModuleDefinitionByFriendlyName(moduleDefinitionName, desktopModule.DesktopModuleID);
            if (objModuleDefinition == null)
            {
                return -1;
            }


            return objModuleDefinition.ModuleDefID;
        }


        public static string filter_TransformString(string output)
        {
            if (PortalSettings.Current.UserMode == PortalSettings.Mode.View)
            {
                return TokenReplaceUtils.Replace(output);
            }
            else
            {
                return output;
            }
            
        }

        private void Page_Load(object sender, EventArgs e)
        {
            // You are in Page_Load of the current Page
        }

        private void Page_Prerender(object sender, EventArgs e)
        {
            CDefault page = (CDefault)sender;

            var lcs = page.GetAllControls().OfType<LiteralControl>().Where(c => c.Parent.ID == "lblContent" && 
                                c.Parent.Parent.GetType().BaseType.FullName == "DotNetNuke.Modules.Html.HtmlModule");
            foreach (var item in lcs)
            {
                LiteralControl lc = (LiteralControl)item;
                lc.Text = TokenReplaceUtils.Replace(lc.Text);
            }

            var labels = page.GetAllControls().OfType<Label>().Where(c => c.ID == "lblText"
                            && c.Parent.GetType().BaseType.FullName == "DotNetNuke.UI.Skins.Controls.Text");

            foreach (var label in labels)
            {
                label.Text = TokenReplaceUtils.Replace(label.Text);
            }
            /*
            var blogContents = page.GetAllControls().OfType<UserControl>().Where(c => c.ID == "vtContents"
                           && c.Parent.GetType().BaseType.FullName == "DotNetNuke.Modules.Blog.Blog");

            foreach (var blog in blogContents)
            {
                object template = Reflection.GetProperty(blog.GetType(), "Template", blog);
                object contents = Reflection.GetProperty(template.GetType(), "Contents", template);

                string newContents = TokenReplaceUtils.Replace(contents as string);
                Reflection.SetProperty(template.GetType(), "Contents", template, new object[]{newContents});
            }  
            */


            /*
            var htmlFinder = new ControlFinder<HtmlModule>();
            htmlFinder.FindChildControlsRecursive(page);
            foreach (var html in htmlFinder.FoundControls)
            {
                var literalFinder = new ControlFinder<DNNLabelEdit>();
                literalFinder.FindChildControlsRecursive(html);
                foreach (var item in literalFinder.FoundControls)
	            {
                    LiteralControl lc = (LiteralControl)item.Controls[0];
                    lc.Text = TokenReplaceUtils.Replace(lc.Text);
	            }                
            }
             */
        }

    }

    class ControlFinder<T> where T : Control
    {
        private readonly List<T> _foundControls = new List<T>();
        public IEnumerable<T> FoundControls
        {
            get { return _foundControls; }
        }

        public void FindChildControlsRecursive(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl is T)
                {
                    _foundControls.Add((T)childControl);
                }
                else
                {
                    FindChildControlsRecursive(childControl);
                }
            }
        }
    }

    public static class PageExtensions
    {
        public static IEnumerable<Control> GetAllControls(this Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                yield return control;
                foreach (Control descendant in control.GetAllControls())
                {
                    yield return descendant;
                }
            }
        }

    }
}