#region Copyright

// 
// Copyright (c) [YEAR]
// by [OWNER]
// 

#endregion

#region Using Statements

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using System.Web.Hosting;
using System.Web;
using System.IO;
using DotNetNuke.Security;
using System.Collections.Generic;
using Satrabel.OpenBlocks.Token;

#endregion

namespace Satrabel.OpenBlocks.TemplateEditor
{
    [SupportedModules("OpenTemplateStudio")]
    public class TemplateEditorController : DnnApiController
    {

        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage MyResponse()
        {

            return Request.CreateResponse(HttpStatusCode.OK, "Hello World!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage UpdateFile(UpdateFileDTO submitted)
        {
            string realfilename = HostingEnvironment.MapPath(submitted.Filename);
            File.WriteAllText(realfilename, submitted.Content);
            return Request.CreateResponse(HttpStatusCode.OK, realfilename);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage OpenFile(OpenFileDTO submitted)
        {
            string realfilename = HostingEnvironment.MapPath(submitted.Filename);
            //File.WriteAllText(realfilename, submitted.Content);
            string Content = File.ReadAllText(realfilename);
            return Request.CreateResponse(HttpStatusCode.OK, Content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage NewFile(OpenFileDTO submitted)
        {
            string realfilename = HostingEnvironment.MapPath(submitted.Filename);
            string Content = "Add new file content";
            if (Path.GetExtension(realfilename) == ".cshtml") {
                Content = "@inherits Satrabel.Providers.TemplateEngine.TemplateWebPage";
            }
            File.WriteAllText(realfilename, Content);

            return Request.CreateResponse(HttpStatusCode.OK, Content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage RunFile(UpdateFileDTO submitted)
        {
            try
            {


                string realfilename = HostingEnvironment.MapPath(submitted.Filename);

                File.WriteAllText(realfilename, submitted.Content);

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                string dsFileName = Path.ChangeExtension(realfilename, ".datasource");
                if (File.Exists(dsFileName))
                {
                    string[] lines = File.ReadAllLines(dsFileName);
                    foreach (var item in lines)
                    {
                        string[] par = item.Split('=');
                        parameters.Add(par[0], par[1]);
                    }
                }
                parameters.Add("templatefile", submitted.Filename);
                string Output = WidgetTokenProvider.ExecuteWidget(parameters);

                return Request.CreateResponse(HttpStatusCode.OK, Output);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage DataFile(UpdateFileDTO submitted)
        {
            string realfilename = HostingEnvironment.MapPath(submitted.Filename);
            File.WriteAllText(realfilename, submitted.Content);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string dsFileName = Path.ChangeExtension(realfilename, ".datasource");
            string[] lines = File.ReadAllLines(dsFileName);
            foreach (var item in lines)
            {
                string[] par = item.Split('=');
                parameters.Add(par[0], par[1]);
            }
            //parameters.Add("templatefile", dfeTree.TreeView.SelectedValue);

            string Output = WidgetTokenProvider.ExecuteWidget(parameters);

            return Request.CreateResponse(HttpStatusCode.OK, Output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage WidgetFile(OpenFileDTO submitted)
        {
            string realfilename = HostingEnvironment.MapPath(submitted.Filename);
            

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string dsFileName = Path.ChangeExtension(realfilename, ".datasource");
            string[] lines = File.ReadAllLines(dsFileName);
            foreach (var item in lines)
            {
                string[] par = item.Split('=');
                parameters.Add(par[0], par[1]);
            }
            parameters.Add("templatefile", submitted.Filename);

            //string Output = WidgetTokenProvider.ExecuteWidget(parameters);

            string Output = "{{widget ";
            foreach (var item in parameters) {
                Output += item.Key + "=\"" + item.Value + "\" ";
            }
            Output += "}}";

            return Request.CreateResponse(HttpStatusCode.OK, Output);
        }

        public class UpdateFileDTO
        {
            public string Filename { get; set; }
            public string Content { get; set; }
        }

        public class OpenFileDTO
        {
            public string Filename { get; set; }
        }
    }
}
