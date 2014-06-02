using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Satrabel.OpenBlocks.Configurator
{
    public class ConfiguratorUtils
    {
        public static List<ListItem> GetModuleList(string ModuleName)
        {
            List<ListItem> lst = new List<ListItem>();
            PortalSettings portalSettings = PortalController.GetCurrentPortalSettings();
            List<TabInfo> objTabs = TabController.GetPortalTabs(portalSettings.PortalId, -1, true, true);
            var objTabController = new TabController();
            var objDesktopModuleController = new DesktopModuleController();
            var objDesktopModuleInfo = objDesktopModuleController.GetDesktopModuleByModuleName(ModuleName);
            if (objDesktopModuleInfo == null)
            {
                objDesktopModuleInfo = objDesktopModuleController.GetDesktopModuleByName(ModuleName);
                if (objDesktopModuleInfo == null)
                {
                    return lst;
                }
            }
            foreach (TabInfo objTab in objTabs.Where(tab => !tab.IsDeleted))
            {
                ModuleController objModules = new ModuleController();
                foreach (KeyValuePair<int, ModuleInfo> pair in objModules.GetTabModules(objTab.TabID))
                {
                    ModuleInfo objModule = pair.Value;
                    if (objModule.IsDeleted)
                    {
                        continue;
                    }
                    if (objModule.DesktopModuleID != objDesktopModuleInfo.DesktopModuleID)
                    {
                        continue;
                    }
                    string strPath = objTab.TabName;
                    TabInfo objTabSelected = objTab;
                    while (objTabSelected.ParentId != Null.NullInteger)
                    {
                        objTabSelected = objTabController.GetTab(objTabSelected.ParentId, objTab.PortalID, false);
                        if (objTabSelected == null)
                        {
                            break;
                        }
                        strPath = string.Format("{0} -> {1}", objTabSelected.TabName, strPath);
                    }
                    var objListItem = new ListItem
                    {
                        Value = objModule.ModuleID.ToString(),
                        Text = string.Format("{0} -> {1}", strPath, objModule.ModuleTitle)
                    };
                    lst.Add(objListItem);
                }
            }
            return lst;
        }
    }
}