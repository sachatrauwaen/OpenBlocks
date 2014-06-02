#region Copyright

// 
// Copyright (c) 2014
// by SatraBel
// 

#endregion

#region Using Statements

using System;
using DotNetNuke.Entities.Modules;
using System.Web.Hosting;
using System.Collections.Generic;
using System.IO;
using Satrabel.Token;
using DotNetNuke.UI.Skins.Controls;

#endregion

namespace SatraBel.TemplateEditor
{

    public partial class Template_Preview : PortalModuleBase
    {

        #region Event Handlers

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        string Template = "";
        string Mode = "preview";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Request.QueryString["template"] != null)
            {
                Template = Request.QueryString["template"];
            }
            if (Request.QueryString["mode"] != null)
            {
                Mode = Request.QueryString["mode"];
            }
            if (!Page.IsPostBack)
            {
                
                try
                {
                    string realfilename = HostingEnvironment.MapPath(Template);
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    string dsFileName = Path.ChangeExtension(realfilename, ".datasource");
                    if (File.Exists(dsFileName))
                    {
                        string[] lines = File.ReadAllLines(dsFileName);
                        foreach (var item in lines)
                        {
                            string[] par = item.Split(new char[]{'='},2);
                            parameters.Add(par[0], par[1]);
                        }
                    }
                    if (Mode != "data")
                    {
                        parameters.Add("templatefile", Template);
                    }

                    if (Mode == "widget")
                    {

                        string Output = "{{widget ";
                        foreach (var item in parameters)
                        {
                            Output += item.Key + "=\"" + item.Value + "\" ";
                        }
                        Output += "}}";
                        lContent.Text = Output;
                        
                        hlThis.NavigateUrl = Request.RawUrl.Replace("&mode=widget", "");
                        hlThis.Text = hlThis.NavigateUrl;
                        hlThis.Visible = true;
                    }
                    else
                    {

                        lContent.Text = WidgetTokenProvider.ExecuteWidget(parameters);
                    }
                }
                catch (Exception ex)
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, Template + "/" + ex.Message, ModuleMessage.ModuleMessageType.RedError);
                }
            }
        }



        #endregion

    }
}

