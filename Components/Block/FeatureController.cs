/*
' Copyright (c) 2014 Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using System;
using DotNetNuke.Entities.Modules.Definitions;
using DotNetNuke.Services.Upgrade;
using DotNetNuke.Entities.Portals;
using System.Collections;
using DotNetNuke.Entities.Tabs;

namespace Satrabel.OpenBlocks.Block
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for OpenBlocks
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController : IUpgradeable //: IPortable, ISearchable, 
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<OpenBlocksInfo> colOpenBlockss = GetOpenBlockss(ModuleID);
        //if (colOpenBlockss.Count != 0)
        //{
        //    strXML += "<OpenBlockss>";

        //    foreach (OpenBlocksInfo objOpenBlocks in colOpenBlockss)
        //    {
        //        strXML += "<OpenBlocks>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objOpenBlocks.Content) + "</content>";
        //        strXML += "</OpenBlocks>";
        //    }
        //    strXML += "</OpenBlockss>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlOpenBlockss = DotNetNuke.Common.Globals.GetContent(Content, "OpenBlockss");
        //foreach (XmlNode xmlOpenBlocks in xmlOpenBlockss.SelectNodes("OpenBlocks"))
        //{
        //    OpenBlocksInfo objOpenBlocks = new OpenBlocksInfo();
        //    objOpenBlocks.ModuleId = ModuleID;
        //    objOpenBlocks.Content = xmlOpenBlocks.SelectSingleNode("content").InnerText;
        //    objOpenBlocks.CreatedByUser = UserID;
        //    AddOpenBlocks(objOpenBlocks);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<OpenBlocksInfo> colOpenBlockss = GetOpenBlockss(ModInfo.ModuleID);

        //foreach (OpenBlocksInfo objOpenBlocks in colOpenBlockss)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objOpenBlocks.Content, objOpenBlocks.CreatedByUser, objOpenBlocks.CreatedDate, ModInfo.ModuleID, objOpenBlocks.ItemId.ToString(), objOpenBlocks.Content, "ItemId=" + objOpenBlocks.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        public string UpgradeModule(string Version)
        {
            string result = "";
            try
            {
                switch (Version)
                {
                    case "00.00.01":
                        ModuleDefinitionInfo mDef = ModuleDefinitionController.GetModuleDefinitionByFriendlyName("OpenBlocks");

                        //Add tab to Admin Menu
                        if (mDef != null)
                        {
                            /*
                            var hostPage = Upgrade.AddHostPage("Open Blocks",
                                                            "Open Blocks",
                                                            "~/Icons/Sigma/Software_16X16_Standard.png",
                                                            "~/Icons/Sigma/Software_32X32_Standard.png",
                                                            true);

                            //Add module to page
                            Upgrade.AddModuleToPage(hostPage, mDef.ModuleDefID, "Open Blocks", "~/Icons/Sigma/Software_32X32_Standard.png", true);
                            */
                            
                            AddAdminPages("Blocks",
                                                 "Manage resuable blocks of content",
                                                 "~/Icons/Sigma/Software_16X16_Standard.png",
                                                 "~/Icons/Sigma/Software_32X32_Standard.png",
                                                 true,
                                                 mDef.ModuleDefID,
                                                 "Open Blocks",
                                                 "~/Icons/Sigma/Software_16X16_Standard.png",
                                                 true);

                            //result = hostPage == null ? "hostpage null" : "hostpage created";
                        }

                        
                        break;
                }
                return "Success " + result;
            }
            catch (Exception)
            {
                return "Failed";
            }

        }

        #endregion

        public static void AddAdminPages(string tabName, string description, string tabIconFile, string tabIconFileLarge, bool isVisible, int moduleDefId, string moduleTitle, string moduleIconFile, bool inheritPermissions)
        {
            var portalController = new PortalController();
            ArrayList portals = portalController.GetPortals();

            //Add Page to Admin Menu of all configured Portals
            for (int intPortal = 0; intPortal <= portals.Count - 1; intPortal++)
            {
                var portal = (PortalInfo)portals[intPortal];

                //Create New Admin Page (or get existing one)
                TabInfo newPage = Upgrade.AddAdminPage(portal, tabName, description, tabIconFile, tabIconFileLarge, isVisible);

                //Add Module To Page
                Upgrade.AddModuleToPage(newPage, moduleDefId, moduleTitle, moduleIconFile, inheritPermissions);
                var moduleController = new ModuleController();

                if (newPage != null) { 
                    foreach (var module in moduleController.GetTabModules(newPage.TabID).Values)
                    {
                        moduleController.UpdateTabModuleSetting(module.TabModuleID, "hideadminborder", "true");
                    }
                }

            }
        }



    }

}
