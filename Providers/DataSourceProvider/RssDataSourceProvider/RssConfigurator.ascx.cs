using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Localization;
using System.Collections;

namespace Satrabel.OpenBlocks.DataSource
{

    public partial class RssConfigurator : DataSourceConfigurator
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /*
        public override string getToken()
        {
            return "dataprovider=\"RSS\" datasource=\"RssFeed\" url=\"" + txtUrl.Text + "\"";
        }
        */
        public override void LoadSettings(Hashtable settings)
        {
            
            try
            {            
                txtUrl.Text = (string)settings["url"];
            }
            catch (Exception)
            {

            }
        }

        public override Hashtable SaveSettings()
        {
            Hashtable settings = new Hashtable();
            settings.Add("dataprovider", "RSS");
            settings.Add("datasource", "RssFeed");
            settings.Add("url", txtUrl.Text);
            return settings;
        }


    }
}