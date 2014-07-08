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
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Web.UI;

namespace Satrabel.OpenBlocks.DataSource
{

    public partial class DnnConfigurator : DataSourceConfigurator
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             var folders = FolderManager.Instance.GetFolders(PortalId);
				foreach (FolderInfo folder in folders)
				{
				    var folderItem = new ListItem
				                         {
				                             Text =
				                                 folder.FolderPath == Null.NullString
				                                     ? Utilities.GetLocalizedString("PortalRoot")
				                                     : folder.DisplayPath,
				                             Value = folder.FolderPath
				                         };
				    ddlFolder.Items.Add(folderItem);
				}
			
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
                ddlFolder.SelectedValue = (string)settings["folder"];
            }
            catch (Exception)
            {

            }
        }

        public override Hashtable SaveSettings()
        {
            Hashtable settings = new Hashtable();
            settings.Add("dataprovider", "DNN");
            settings.Add("datasource", "Files");
            settings.Add("path", PortalSettings.Current.HomeDirectory + ddlFolder.SelectedValue);
            settings.Add("searchPatern", tbSearchPatern.Text);
            

            return settings;
        }


    }
}