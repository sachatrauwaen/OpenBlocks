﻿
using Satrabel.OpenBlocks.Configurator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Satrabel.OpenBlocks.Token
{
    public partial class HtmlModuleConfigurator : TokenConfigurator
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlModules.Items.AddRange(ConfiguratorUtils.GetModuleList("HTML").ToArray());            
            }
        }

        public override void LoadSettings(Hashtable settings)
        {            
            try
            {
                ddlModules.SelectedValue = (string)settings["ModuleId"];
            }
            catch (Exception)
            {
            }
        }
        public override Hashtable SaveSettings()
        {
            Hashtable settings = new Hashtable();
            settings.Add("ModuleId", ddlModules.SelectedValue);
            return settings;
        }

        public override string getToken()
        {
            return "{{htmlmodule moduleid=\"" + ddlModules.SelectedValue + "\" }}";            
        }
    }
}