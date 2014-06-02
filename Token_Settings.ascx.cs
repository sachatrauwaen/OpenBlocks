#region Copyright

// 
// Copyright (c) 2014
// by SatraBel
// 

#endregion

#region Using Statements

using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using Satrabel.OpenBlocks.Token;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Linq;

#endregion

namespace Satrabel.OpenBlocks
{

    public partial class Token_Settings : ModuleSettingsBase
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            foreach (var provider in TokenProvider.GetProviderList())
            {
                if (!string.IsNullOrEmpty(provider.Configurator))
                {
                    var ctrl = this.LoadControl(provider.Configurator);
                    TokenConfigurator conf = ctrl as TokenConfigurator;
                    if (conf != null)
                    {
                        conf.PortalId = PortalId;
                        conf.LocalResourceFile = LocalResourceFile;
                        //conf.Visible = 
                        conf.ID = provider.FriendlyName;
                        phConfigurator.Controls.Add(ctrl);
                    }
                }
            }
        }

        #region Base Method Implementations

        public override void LoadSettings()
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    foreach (var item in TokenProvider.GetProviderList())
                    {
                        ddlTokens.Items.Add(new ListItem(item.FriendlyName));
                    }
                    //txtField.Text = (string)TabModuleSettings["field"];
                    ddlTokens.SelectedValue = (string)TabModuleSettings["TokenProvider"];

                    SelectProvider(ddlTokens.SelectedValue);
                }
            }
            catch (Exception exc) // Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void SelectProvider(string FriendlyName)
        {
            foreach (TokenConfigurator conf in phConfigurator.Controls)
            {
                conf.Visible = FriendlyName == conf.ID;
                conf.LoadSettings(TabModuleSettings);
            }
        }

        public override void UpdateSettings()
        {
            try
            {
                ModuleController controller = new ModuleController();
                controller.UpdateTabModuleSetting(TabModuleId, "TokenProvider", ddlTokens.SelectedValue);
                foreach (TokenConfigurator conf in phConfigurator.Controls)
                {
                    var sets = conf.SaveSettings();
                    foreach (DictionaryEntry set in sets)
                    {
                        controller.UpdateTabModuleSetting(TabModuleId, (string)set.Key, (string)set.Value);
                    }
                    if (conf.Visible) 
                    {
                        controller.UpdateTabModuleSetting(TabModuleId, "Token", conf.getToken());
                        
                    }
                }
            }
            catch (Exception exc) // Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        protected void ddlTokens_SelectedIndexChanged(object sender, EventArgs e)
        {
            var provider = TokenProvider.Instance(ddlTokens.SelectedValue);
            SelectProvider(ddlTokens.SelectedValue);
            if (!string.IsNullOrEmpty(provider.Configurator))
            {
                /*
                var ctrl = this.LoadControl(provider.Configurator);
                TokenConfigurator conf = ctrl as TokenConfigurator;
                if (conf != null)
                {
                    conf.PortalId = PortalId;
                    conf.LocalResourceFile = LocalResourceFile;
                    this.Controls.Add(ctrl);

                    ModuleController controller = new ModuleController();
                    controller.UpdateTabModuleSetting(TabModuleId, "Configurator", provider.Configurator);
                }
                 */
            }
        }


        public string Token
        {
            get
            {
                if (Page.IsPostBack)
                    return Request.Form[hfToken.UniqueID];
                else
                    return hfToken.Value;
            }
            set
            {
                hfToken.Value = value;
            }
        }

    }

}


