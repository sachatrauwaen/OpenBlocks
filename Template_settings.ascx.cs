using DotNetNuke.Entities.Modules;
using Satrabel.OpenBlocks.Block;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Satrabel.OpenBlocks
{
    public partial class Template_settings : OpenBlocksModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbCKEditor_Click(object sender, EventArgs e)
        {
            string FileName = Path.Combine(PortalSettings.HomeDirectoryMapPath, "CKToolbarButtons.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);
            if (doc.SelectNodes("/ArrayOfToolbarButton//ToolbarButton[contains(ToolbarName,'dnntokens')]").Count == 0)
            {
                var ArrayOfToolbarButton = doc.GetElementsByTagName("ArrayOfToolbarButton").Item(0);
                XmlElement ToolbarButton = doc.CreateElement("ToolbarButton");
                ArrayOfToolbarButton.AppendChild(ToolbarButton);
                XmlElement ToolbarName = doc.CreateElement("ToolbarName");
                ToolbarName.InnerText = "dnntokens";
                ToolbarButton.AppendChild(ToolbarName);
                XmlElement ToolbarIcon = doc.CreateElement("ToolbarIcon");
                ToolbarIcon.InnerText = "../plugins/dnntokens/icon.gif";
                ToolbarButton.AppendChild(ToolbarIcon);
                doc.Save(FileName);
            }

            FileName = Path.Combine(PortalSettings.HomeDirectoryMapPath, "CKEditorSettings.xml");
            doc = new XmlDocument();
            doc.Load(FileName);
            var nodes = doc.SelectNodes("/EditorProviderSettings//Config");
            if (nodes.Count > 0)
            {
                var extraPlugins = nodes.Item(0).Attributes["extraPlugins"].Value;
                string[] plugins = extraPlugins.Split(',');
                if (!plugins.Contains("dnntokens"))
                {
                    nodes.Item(0).Attributes["extraPlugins"].Value = extraPlugins + ",dnntokens";
                    doc.Save(FileName);
                }
            }
            // Add ...
            FileName = Path.Combine(PortalSettings.HomeDirectoryMapPath, "CKToolbarSets.xml");
            doc = new XmlDocument();
            doc.Load(FileName);
            nodes = doc.SelectNodes("/ArrayOfToolbarSet//ToolbarSet[contains(Name,'Full')]//ToolbarGroups//ToolbarGroup[contains(name,'insert')]//items");
            if (nodes.Count > 0 && !nodes.Item(0).InnerXml.Contains("<string>dnntokens</string>"))
            {
                //nodes = doc.SelectNodes("/ArrayOfToolbarSet//ToolbarSet[contains(Name,'Full')]//ToolbarGroups//ToolbarGroup[contains(name,'insert')]//items");
                if (nodes.Count > 0)
                {
                    var elem = doc.CreateElement("string");
                    elem.InnerXml = "dnntokens";
                    nodes.Item(0).AppendChild(elem);
                    doc.Save(FileName);
                }
            }
            // Install plugin
            TemplateEditorUtils.DirectoryCopy(Server.MapPath("~/DesktopModules/OpenBlocks/Providers/HtmlEditorProviders"), Server.MapPath("~/Providers/HtmlEditorProviders"), true, true);
        }

        protected void lbBlockPages_Click(object sender, EventArgs e)
        {
            FeatureController fc = new FeatureController();
            fc.UpgradeModule("00.00.01");
        }
    }
}