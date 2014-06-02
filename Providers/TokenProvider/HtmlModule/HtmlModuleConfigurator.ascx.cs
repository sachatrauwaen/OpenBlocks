
using Satrabel.Modules.OpenBlocks.Components.Configurator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Satrabel.Token
{
    public partial class HtmlModuleConfigurator : TokenConfigurator
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void LoadSettings(Hashtable settings)
        {
            ddlModules.Items.AddRange(ConfiguratorUtils.GetModuleList("HTML").ToArray());            
            try
            {
                ddlModules.SelectedValue = (string)settings["HtmlModule_ModuleId"];
            }
            catch (Exception)
            {
            }
        }
        public override Hashtable SaveSettings()
        {
            Hashtable settings = new Hashtable();
            settings.Add("HtmlModule_ModuleId", ddlModules.SelectedValue);
            return settings;
        }

        public override string getToken()
        {
            return "{{htmlmodule moduleid=\"" + ddlModules.SelectedValue + "\" }}";            
        }
    }
}