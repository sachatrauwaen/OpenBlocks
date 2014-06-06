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
using Satrabel.OpenBlocks.Token;
using DotNetNuke.UI.Skins.Controls;
using Satrabel.OpenBlocks.DataSource;
using System.Collections;
using System.Web.UI.WebControls;

#endregion

namespace SatraBel.OpenBlocks
{

    public partial class Template_DataSource : PortalModuleBase
    {

        #region Event Handlers

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                foreach (var provider in DataSourceProvider.GetProviderList())
                {
                    if (!string.IsNullOrEmpty(provider.Configurator))
                    {
                        var ctrl = this.Page.LoadControl(provider.Configurator);
                        DataSourceConfigurator conf = ctrl as DataSourceConfigurator;
                        if (conf != null)
                        {
                            conf.PortalId = PortalId;
                            conf.LocalResourceFile = LocalResourceFile;
                            conf.ID = provider.FriendlyName;
                            phConfigurator.Controls.Add(ctrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, TemplateFileName + "/" + ex.Message, ModuleMessage.ModuleMessageType.RedError);
            }
        }

        string TemplateFileName = "";


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Request.QueryString["template"] != null)
            {
                TemplateFileName = Request.QueryString["template"];
            }

            if (!Page.IsPostBack)
            {
                try
                {
                    foreach (var item in DataSourceProvider.GetProviderList())
                    {
                        if (item.Configurator != null)
                        {
                            ddlDataSource.Items.Add(new ListItem(item.FriendlyName));
                        }
                    }
                    string realfilename = HostingEnvironment.MapPath(TemplateFileName);
                    Hashtable settings = new Hashtable();
                    string dsFileName = Path.ChangeExtension(realfilename, ".datasource");
                    //string token = "";
                    if (File.Exists(dsFileName))
                    {
                        string[] lines = File.ReadAllLines(dsFileName);
                        foreach (var item in lines)
                        {
                            string[] par = item.Split(new char[] { '=' }, 2);
                            settings.Add(par[0], par[1]);
                        }
                        ddlDataSource.SelectedValue = (string)settings["dataprovider"];
                    }

                    foreach (DataSourceConfigurator conf in phConfigurator.Controls)
                    {
                        conf.Visible = ddlDataSource.SelectedValue == conf.ID;
                        conf.LoadSettings(settings);

                        //conf.setToken(token);
                    }
                }
                catch (Exception ex)
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, TemplateFileName + "/" + ex.Message, ModuleMessage.ModuleMessageType.RedError);
                }
            }
        }

        protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataSourceConfigurator conf in phConfigurator.Controls)
            {
                conf.Visible = ddlDataSource.SelectedValue == conf.ID;

            }

        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string realfilename = HostingEnvironment.MapPath(TemplateFileName);
            string dsFileName = Path.ChangeExtension(realfilename, ".datasource");
            foreach (DataSourceConfigurator conf in phConfigurator.Controls)
            {
                if (conf.Visible)
                {
                    var ht = conf.SaveSettings();
                    List<string> lst = new List<string>();
                    var ModelParameters = new Dictionary<string, string>();
                    foreach (DictionaryEntry item in ht)
                    {
                        lst.Add(item.Key + "=" + item.Value);
                        ModelParameters.Add((string)item.Key, (string)item.Value);
                    }
                    File.WriteAllLines(dsFileName, lst.ToArray());
                    UpdateRazorFile(realfilename,  ModelParameters);
                }
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        private void UpdateRazorFile(string realfilename,  Dictionary<string, string> Parameters)
        {

            if (Path.GetExtension(realfilename) == ".cshtml")
            {
                try
                {
                    object Model = DataSourceUtils.GetModel(Parameters);

                    string[] Lines = File.ReadAllLines(realfilename);
                    if (Lines.Length > 0 && Lines[0].Contains("@inherits"))
                    {
                        Lines[0] = "@inherits Satrabel.OpenBlocks.TemplateEngine.TemplateWebPage<" + Model.GetType().FullName + ">";
                        File.WriteAllLines(realfilename, Lines);
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                

               
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

    }
}

