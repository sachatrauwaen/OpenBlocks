using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Satrabel.Token;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SatraBel.Widget
{
    public partial class TokenSettings : System.Web.UI.UserControl 
    {
        public string LocalResourceFile { get; set; }
        public int PortalId { get; set; }
        protected string PostBackStr;
        protected string CloseDialogStr;


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            /*
            string cbReference  = Page.ClientScript.GetCallbackEventReference(this, "arg", "alert(\"hello\")", "");
            string callbackScript = "<script type=\"text/javascript\">function CallServer(arg, context)" + "{ " + cbReference + ";}</script>";
             Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", callbackScript);
             */
            PostBackStr = Page.ClientScript.GetPostBackEventReference(Page, "EditorClose");
            
            if (Page.IsPostBack)
            {
                string eventArg = Request["__EVENTARGUMENT"];
                if (eventArg == "EditorClose")
                {
                    CloseDialogStr = "<script type=\"text/javascript\">" +
                    "window.parent.EditorCloseCallBack('" + GetToken() + "');" +
                    "</script>";                    
                }
            }
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            LocalResourceFile = ResXFile;
            //PortalId = 
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

                        conf.ID = provider.FriendlyName;
                        phConfigurator.Controls.Add(ctrl);
                    }
                }
            }
        }

        #region Base Method Implementations

        public void LoadSettings(Hashtable settings)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    foreach (var item in TokenProvider.GetProviderList())
                    {
                        ddlTokens.Items.Add(new ListItem(item.FriendlyName));
                    }                  
                    ddlTokens.SelectedValue = (string)settings["TokenProvider"];
                    foreach (TokenConfigurator conf in phConfigurator.Controls)
                    {
                        conf.Visible = ddlTokens.SelectedValue == conf.ID;
                        conf.LoadSettings(settings);
                    }
                }
            }
            catch (Exception exc) // Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        public Hashtable SaveSettings()
        {

            Hashtable settings = new Hashtable();
            settings.Add("TokenProvider", ddlTokens.SelectedValue);
            foreach (TokenConfigurator conf in phConfigurator.Controls)
            {
                var sets = conf.SaveSettings();
                foreach (DictionaryEntry set in sets)
                {
                    settings.Add((string)set.Key, (string)set.Value);
                }
                if (conf.Visible)
                {
                    settings.Add("Token", conf.getToken());
                }
            }
            return settings;
        }

        #endregion

        protected void ddlTokens_SelectedIndexChanged(object sender, EventArgs e)
        {
            var provider = TokenProvider.Instance(ddlTokens.SelectedValue);
            foreach (TokenConfigurator conf in phConfigurator.Controls)
            {
                conf.Visible = ddlTokens.SelectedValue == conf.ID;
            }
        }

        public string GetToken()
        {
            foreach (TokenConfigurator conf in phConfigurator.Controls)
            {
                if (conf.Visible)
                {
                    return conf.getToken();
                }
            }
            return "";
        }

        protected string ResXFile
        {
            get
            {
                return
                    this.ResolveUrl(
                        string.Format(
                            "~/DesktopModules/Satrabel/Widget/{0}/TokenSettings.ascx.resx",
                            Localization.LocalResourceDirectory));
            }
        }        
    }
}