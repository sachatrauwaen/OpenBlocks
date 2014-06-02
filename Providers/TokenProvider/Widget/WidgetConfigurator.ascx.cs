using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DotNetNuke.Services.Exceptions;
using System.Collections;
using Satrabel.OpenBlocks.DataSource;


namespace Satrabel.OpenBlocks.Token
{

    public partial class WidgetConfigurator : TokenConfigurator
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                //ddlType.SelectedValue = Request.Cookies["ddlType"].Value.ToString();

                /*
                if (ddlType.SelectedIndex > 0)
                {
                    TemplateEditorUtils.TemplateDataBind("Widgets", int.Parse(ddlType.SelectedValue), ddlTemplate, PortalId, LocalResourceFile, Server);
                    if (Request.Cookies["ddlTemplate"] != null)
                    {
                        string template_value = Request.Cookies["ddlTemplate"].Value.ToString();
                        var template_item = ddlTemplate.Items.FindByValue(template_value);
                        if (template_item != null)
                        {
                            ddlTemplate.SelectedValue = Request.Cookies["ddlTemplate"].Value.ToString();

                            if (Request.Cookies["lkbRefresh"] != null)
                            {
                                //showFile(Request.Cookies["lkbRefresh"].Value.ToString());
                            }
                        }
                    }
                } 
                 */
            }
        }

        public override string getToken()
        {
            string DataSourceToken = "";
            foreach (DataSourceConfigurator conf in phConfigurator.Controls)
            {
                if (conf.Visible)
                {
                    DataSourceToken = conf.getToken();
                    break;
                }

            }
            string TemplateFile = "";
            
                TemplateFile =  ddlTemplate.SelectedValue + "/" + ddlFile.SelectedValue;
            return "{{widget templatefile=\"" + TemplateFile + "\" " + DataSourceToken + " }}";
        }



        public override void LoadSettings(Hashtable settings)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    foreach (var item in DataSourceProvider.GetProviderList())
                    {
                        ddlDataSource.Items.Add(new ListItem(item.FriendlyName));
                    }
                    //txtField.Text = (string)TabModuleSettings["field"];

                    ddlDataSource.SelectedValue = (string)settings["Widget_DataSourceProvider"];

                    SelectProvider(ddlDataSource.SelectedValue, settings);



                    TemplateEditorUtils.TemplateDataBind("Widgets", ddlTemplate, PortalId, LocalResourceFile, Server);
                    ddlTemplate.SelectedValue = (string)settings["Widget_Template"];
                    TemplateEditorUtils.FileDataBind("Widgets", ddlTemplate, ddlFile, PortalId, LocalResourceFile, Server);
                    ddlFile.SelectedValue = (string)settings["Widget_File"];

                }
            }
            catch (Exception exc) // Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public override Hashtable SaveSettings()
        {
            Hashtable settings = new Hashtable();
            foreach (DataSourceConfigurator conf in phConfigurator.Controls)
            {
                var sets = conf.SaveSettings();
                foreach (DictionaryEntry set in sets)
                {
                    settings.Add(set.Key, set.Value);
                }
            }

            
            settings.Add("Widget_Template", ddlTemplate.SelectedValue);
            settings.Add("Widget_File", ddlFile.SelectedValue);
            settings.Add("Widget_DataSourceProvider", ddlDataSource.SelectedValue);
            //settings.Add("Token", getToken());
            return settings;
        }

        private void SelectProvider(string FriendlyName, Hashtable settings)
        {
            foreach (DataSourceConfigurator conf in phConfigurator.Controls)
            {
                conf.Visible = FriendlyName == conf.ID;
                if (settings != null)
                {
                    conf.LoadSettings(settings);
                }
            }
        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateEditorUtils.FileDataBind("Widgets", ddlTemplate, ddlFile, PortalId, LocalResourceFile, Server);
        }
        protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectProvider(ddlDataSource.SelectedValue, null);

        }


    }
}