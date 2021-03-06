﻿#region Copyright

// 
// Copyright (c) 2014
// by SatraBel
// 

#endregion

#region Using Statements

using System;
using System.Linq;
using System.Xml.Linq;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Web.Razor;
using DotNetNuke.UI.Skins.Controls;
using System.Web.UI;
//using DotNetNuke.UI.Utilities;
using ICSharpCode.SharpZipLib.Zip;
using System.Net;
using System.Threading;
using System.Web;
using System.Collections.Generic;
using Satrabel.OpenBlocks.Token;
using Satrabel.OpenBlocks.DataSource;
using DotNetNuke.Framework;
using System.Xml;
using DotNetNuke.Common;



#endregion

namespace SatraBel.OpenBlocks
{
    public partial class Template_View : PortalModuleBase, IActionable //, IClientAPICallbackEventHandler
    {

        public String PreviewUrl;
        public String DataSourceUrl;
        public String ConfigUrl;
        RadMenuItem customMenuOption = new RadMenuItem("New File");




        protected void Page_Load(object sender, EventArgs e)
        {

            ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
            lkbDelete.Attributes.Add("onClick", " return confirm('" + Localization.GetString("msgConfirm", LocalResourceFile) + "')");
            lbCreate.NavigateUrl = EditUrl();
            PreviewUrl = ModuleContext.NavigateUrl(TabId, "Preview", true, "mid=" + ModuleId);
            DataSourceUrl = ModuleContext.NavigateUrl(TabId, "DataSource", true, "mid=" + ModuleId);
            hlSettings.NavigateUrl = EditUrl("Config");
            hlHome.NavigateUrl = Globals.NavigateURL(PortalSettings.HomeTabId);
            /*
            string SaveDataSource = Page.ClientScript.GetPostBackEventReference(Page, "SaveDataSource");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SaveDataSource", string.Format("function SaveDataSource(){{ {0} }}\n", SaveDataSource), true);
            string LoadDataSource = Page.ClientScript.GetPostBackEventReference(Page, "SaveDataSource");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadDataSource", string.Format("function LoadDataSource(){{ {0} }}\n", LoadDataSource), true);
            if (Page.IsPostBack)
            {
                string eventArg = Request["__EVENTARGUMENT"];
                if (eventArg == "SaveDataSource")
                {
                    string ds = "";
                    foreach (DataSourceConfigurator conf in phConfigurator.Controls)
                    {
                        if (conf.Visible)
                        {
                            ds += conf.getToken();
                        }
                    }
                    File.WriteAllText(Server.MapPath(dfeTree.TreeView.SelectedValue), ds);
                }
                else if (eventArg == "LoadDataSource")
                {
                    string ds = File.ReadAllText(Server.MapPath(dfeTree.TreeView.SelectedValue));
                    var dic = DataSourceConfigurator.ParseToken(ds);
                    foreach (DataSourceConfigurator conf in phConfigurator.Controls)
                    {
                        conf.Visible = conf.ID == dic["dataprovider"];
                        if (conf.Visible)
                        {
                            conf.setToken(ds);
                        }
                    }                    
                }
            }
             */
            if (!Page.IsPostBack)
            {
                //hlNoSkin.NavigateUrl = Request.RawUrl + "?SkinSrc=%5BG%5DSkins/_default/No%20Skin";
                TemplateEditorUtils.ModuleDataBind(ddlModule, PortalId, LocalResourceFile, Server);
                if (Request.Cookies["ddlModule"] != null)
                {
                    string module_value = Request.Cookies["ddlModule"].Value.ToString();
                    var module_item = ddlModule.Items.FindByValue(module_value);
                    if (module_item != null)
                    {
                        ddlModule.SelectedValue = module_value;
                        if (ddlModule.SelectedIndex > 0)
                        {
                            TemplateEditorUtils.TemplateDataBind(ddlModule.SelectedValue, ddlTemplate, PortalId, LocalResourceFile, Server);
                            if (!string.IsNullOrEmpty(ddlTemplate.SelectedValue))
                            {
                                //ddlTemplate_SelectedIndexChanged(null, null);
                            }
                            else if (Request.Cookies["ddlTemplate"] != null)
                            {
                                string template_value = Request.Cookies["ddlTemplate"].Value.ToString();
                                var template_item = ddlTemplate.Items.FindByValue(template_value);
                                if (template_item != null)
                                {
                                    ddlTemplate.SelectedValue = Request.Cookies["ddlTemplate"].Value.ToString();

                                }
                            }
                            if (!string.IsNullOrEmpty(ddlTemplate.SelectedValue))
                            {
                                if (Request.Cookies["lkbRefresh"] != null)
                                {
                                    showFile(Request.Cookies["lkbRefresh"].Value.ToString());
                                }
                            }
                        }
                    }
                }
                if (Request.Cookies["cbFullScreen"] != null)
                {
                    cbFullScreen.Checked = Boolean.Parse(Request.Cookies["cbFullScreen"].Value.ToString());
                    //cbFullScreen_CheckedChanged(null, null);
                    ScopeWrapper.CssClass = cbFullScreen.Checked ? "overlay" : "";
                }
            }
            else
            {
                string ctrlname = Page.Request.Params["__EVENTTARGET"];
                if (!string.IsNullOrEmpty(ctrlname))
                {
                    var targetControl = this.Page.FindControl(ctrlname);
                    if (targetControl == ddlModule)
                    {
                        TemplateEditorUtils.TemplateDataBind(ddlModule.SelectedValue, ddlTemplate, PortalId, LocalResourceFile, Server);
                    }
                }
            }
            if (!string.IsNullOrEmpty(ddlTemplate.SelectedValue))
            {
                //string path = TemplateEditorUtils.GenerateDirectory(ddlModule, ddlType, ddlTemplate, PortalId, Server);
                string path = ddlTemplate.SelectedValue;
                if (dfeTree.Configuration.ViewPaths.Length == 0 || path != dfeTree.Configuration.ViewPaths[0])
                {
                    dfeTree.TreeView.Nodes.Clear();
                    dfeTree.Configuration.ViewPaths = new string[] { path };
                    dfeTree.Configuration.DeletePaths = dfeTree.Configuration.ViewPaths;
                    dfeTree.Configuration.UploadPaths = dfeTree.Configuration.ViewPaths;
                }
            }
            else
            {
                ClearTreeView();

            }
            customMenuOption.Value = "newfile";
            dfeTree.TreeView.ContextMenus[0].Items.Add(customMenuOption);
            dfeTree.TreeView.OnClientContextMenuItemClicked = "OnTreeContextMenuItemClicked";
            RefreshControls();
            DotNetNuke.Framework.AJAX.RegisterPostBackControl(ddlModule);
            //DotNetNuke.Framework.AJAX.RegisterPostBackControl(ddlType);
            DotNetNuke.Framework.AJAX.RegisterPostBackControl(ddlTemplate);
            //DotNetNuke.Framework.AJAX.RegisterPostBackControl(lkbRefresh);
            //DotNetNuke.Framework.AJAX.RegisterPostBackControl(btnValid);
            DotNetNuke.Framework.AJAX.RegisterPostBackControl(lkbDelete);
            //DotNetNuke.Framework.AJAX.RegisterPostBackControl(lkbRun);
            lkbData.Visible = hlDataSource.Visible = hlPreview.Visible = hlWidget.Visible = (ddlModule.SelectedValue == "widgets" || ddlModule.SelectedValue == "RazorModules/RazorHost");

        }

        private void RefreshControls()
        {
            btnZip.Visible = false;
            lkbDelete.Visible = false;
            if (!string.IsNullOrEmpty(ddlTemplate.SelectedValue))
            {
                btnZip.Visible = true;
                lkbDelete.Visible = true;
                DotNetNuke.Framework.AJAX.RegisterPostBackControl(btnZip);
            }
        }
        protected void treeview1_ContextMenuItemClick(object sender, Telerik.Web.UI.RadTreeViewContextMenuEventArgs e)
        {
            if (e.Node != null)
            {
                string s = e.Node.Text;
                string t = e.MenuItem.Value;
                string chemin = dfeTree.Configuration.ViewPaths[0];
                chemin = chemin.Remove(chemin.LastIndexOf('/'));
                File.WriteAllText(Server.MapPath(chemin + "/" + e.Node.FullPath + "/newfile.css"), "new");
                dfeTree.TreeView.Nodes.Clear();
            }
        }
        private void ClearTreeView()
        {
            dfeTree.TreeView.Nodes.Clear();
            dfeTree.Configuration.ViewPaths = new string[] { };
            dfeTree.Configuration.DeletePaths = dfeTree.Configuration.ViewPaths;
            dfeTree.Configuration.UploadPaths = dfeTree.Configuration.ViewPaths;
            //dfeTree.InitialPath = "";
        }

        protected void lkbRefresh_Click(object sender, EventArgs e)
        {
            Response.Cookies["lkbRefresh"].Value = dfeTree.TreeView.SelectedValue;
            showFile(dfeTree.TreeView.SelectedValue);
        }

        private void showFile(string filename)
        {
            if (Path.GetExtension(filename) == ".png" || Path.GetExtension(filename) == ".jpg" || Path.GetExtension(filename) == ".gif")
            {
                tbxEdit.Text = "";
                tbxEdit.Visible = false;
                //imgEdit.Visible = true;
                imgEdit.ImageUrl = filename;
                lFileName.Text = filename;
            }
            else
            {
                //btnValid.Visible = true;
                tbxEdit.Visible = true;
                //imgEdit.Visible = false;
                string RealFileName = Server.MapPath(filename);
                if (File.Exists(RealFileName))
                {
                    tbxEdit.Text = File.ReadAllText(RealFileName);
                    SetFileType(filename);
                    lFileName.Text = filename;
                }
            }
            lkbDelete.Visible = true;
            //lkbRun.Visible = Path.GetExtension(filename) == ".cshtml";
            //phToolbar.Visible = Path.GetExtension(filename) == ".cshtml";
            //lFileName.Text = tbxEdit.Visible ? filename : "";

        }
        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.Cookies["lkbRefresh"] != null)
            {
                HttpCookie myCookie = new HttpCookie("lkbRefresh");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                myCookie.Value = "";
                Response.Cookies.Add(myCookie);
            }
            Response.Cookies["ddlModule"].Value = ddlModule.SelectedValue;
            lFileName.Text = "";
            if (!string.IsNullOrEmpty(tbxEdit.Text))
                tbxEdit.Text = "";
            tbxEdit.Visible = true;
            //ddlTemplate.Items.Clear();
            //ddlTemplate.Items.Add(new ListItem(Localization.GetString("selectTemplate", LocalResourceFile), ""));
            //ddlType.SelectedIndex = 0;

            //TemplateEditorUtils.TemplateDataBind(ddlModule.SelectedValue, ddlTemplate, PortalId, LocalResourceFile, Server);
            // already done in page load
            if (!string.IsNullOrEmpty(ddlTemplate.SelectedValue))
            {
                ddlTemplate_SelectedIndexChanged(null, null);
            }

            //ClearTreeView();

            RefreshControls();
        }
        /*
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.Cookies["lkbRefresh"] != null)
            {
                HttpCookie myCookie = new HttpCookie("lkbRefresh");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            Response.Cookies["ddlType"].Value = ddlType.SelectedValue;

            if (!string.IsNullOrEmpty(tbxEdit.Text))
                tbxEdit.Text = "";
            tbxEdit.Visible = true;
            TemplateEditorUtils.TemplateDataBind(ddlModule, ddlType, ddlTemplate, PortalId, LocalResourceFile, Server);
            ClearTreeView();
            RefreshControls();
        }
         */
        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.Cookies["lkbRefresh"] != null)
            {
                HttpCookie myCookie = new HttpCookie("lkbRefresh");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                myCookie.Value = "";
                Response.Cookies.Add(myCookie);
            }
            Response.Cookies["ddlTemplate"].Value = ddlTemplate.SelectedValue;
            lFileName.Text = "";
            if (!string.IsNullOrEmpty(tbxEdit.Text))
                tbxEdit.Text = "";
            tbxEdit.Visible = true;
            RefreshControls();
        }
        protected void btnValid_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Server.MapPath(lFileName.Text), tbxEdit.Text);
            lblMsg.Visible = true;
            lblMsg.Text = Localization.GetString("msgSuccess", LocalResourceFile);
            //btnValid.Visible = true;
            lkbDelete.Visible = true;
            //lkbRun.Visible = Path.GetExtension(dfeTree.TreeView.SelectedValue) == ".cshtml";
        }
        protected void lkbDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxEdit.Text))
                tbxEdit.Text = "";

            tbxEdit.Visible = true;
            //string path = Server.MapPath(TemplateEditorUtils.GenerateDirectory(ddlModule, ddlType, ddlTemplate, PortalId, Server));
            string path = ddlTemplate.SelectedValue;
            TemplateEditorUtils.DeleteDirectory(Server.MapPath(path));
            lblMsg.Visible = true;
            lblMsg.Text = Localization.GetString("msgSuccessDelete", LocalResourceFile);
            ddlTemplate.Items.Clear();
            ddlTemplate.Items.Add(new ListItem(Localization.GetString("selectTemplate", LocalResourceFile), ""));
            TemplateEditorUtils.TemplateDataBind(ddlModule.SelectedValue, ddlTemplate, PortalId, LocalResourceFile, Server);
            ClearTreeView();
            RefreshControls();
        }

        protected void lkbRun_Click(object sender, EventArgs e)
        {
            try
            {
                tbxEdit.Visible = false;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                string dsFileName = Path.ChangeExtension(dfeTree.TreeView.SelectedValue, ".datasource");
                string[] lines = File.ReadAllLines(Server.MapPath(dsFileName));
                foreach (var item in lines)
                {
                    string[] par = item.Split('=');
                    parameters.Add(par[0], par[1]);
                }
                parameters.Add("templatefile", dfeTree.TreeView.SelectedValue);
                //ltlRun.Text = WidgetTokenProvider.ExecuteWidget(parameters);
                //ObjectDumper.Dump(obj);
            }
            catch (Exception ex)
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.Message, ModuleMessage.ModuleMessageType.RedError);
            }
        }

        protected void lkbData_Click(object sender, EventArgs e)
        {
            try
            {
                tbxEdit.Visible = false;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                string dsFileName = Path.ChangeExtension(dfeTree.TreeView.SelectedValue, ".datasource");
                string[] lines = File.ReadAllLines(Server.MapPath(dsFileName));
                foreach (var item in lines)
                {
                    string[] par = item.Split('=');
                    parameters.Add(par[0], Server.UrlEncode(par[1]));
                }
                //parameters.Add("templatefile", dfeTree.TreeView.SelectedValue);

                //ltlRun.Text = WidgetTokenProvider.ExecuteWidget(parameters);
                //ObjectDumper.Dump(obj);
            }
            catch (Exception ex)
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.Message, ModuleMessage.ModuleMessageType.RedError);
            }
        }
        /*
        public string RaiseClientAPICallbackEvent(string eventArgument)
        {
            int idx = eventArgument.IndexOf(':');
            string Command = eventArgument.Substring(0, idx);
            string filename = eventArgument.Substring(idx+1);
            if (Command == "new")
            {
                filename = Path.GetFullPath(Server.MapPath(filename));
                File.WriteAllText(filename, "Add content here");
            }
            else if (Command == "save")
            {
                idx = filename.IndexOf(':');
                string content = filename.Substring(idx + 1);
                filename = filename.Substring(0, idx);

                filename = Server.MapPath(filename);
                File.WriteAllText(filename, content);
                lblMsg.Visible = true;
                lblMsg.Text = Localization.GetString("msgSuccess", LocalResourceFile);
            }
            return "";
        }
        */
        private void SetFileType(string filePath)
        {
            string mimeType;
            switch (Path.GetExtension(filePath).ToLowerInvariant())
            {
                case ".vb":
                    mimeType = "text/x-vb";
                    break;
                case ".cs":
                    mimeType = "text/x-csharp";
                    break;
                case ".css":
                    mimeType = "text/css";
                    break;
                case ".js":
                    mimeType = "text/javascript";
                    break;
                case ".xml":
                case ".xslt":
                    mimeType = "application/xml";
                    break;
                case ".sql":
                case ".sqldataprovider":
                    mimeType = "text/x-sql";
                    break;
                default:
                    mimeType = "text/html";
                    break;
            }
            DotNetNuke.UI.Utilities.ClientAPI.RegisterClientVariable(Page, "mimeType", mimeType, true);
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("tpEdit", LocalResourceFile), ModuleActionType.AddContent, "", "", EditUrl(), false, SecurityAccessLevel.Host, true, false);
                return Actions;
            }
        }
        protected void btnZip_Click(object sender, EventArgs e)
        {
            //string path = Server.MapPath(TemplateEditorUtils.GenerateDirectory(ddlModule, ddlType, ddlTemplate, PortalId, Server) + "\\");
            string path = Server.MapPath(ddlTemplate.SelectedValue);
            string TemplateName = ddlTemplate.SelectedValue.TrimEnd('/');
            TemplateName = TemplateName.Substring(TemplateName.LastIndexOf("/") + 1);
            string filename = PortalSettings.HomeDirectoryMapPath + "Templates\\" + TemplateName + ".zip";
            ZipOutputStream zip = new ZipOutputStream(File.Create(filename));
            zip.SetLevel(9);
            TemplateEditorUtils.ZipFolder(path, path, zip);
            zip.Finish();
            zip.Close();
            Response.ContentType = "application/zip";
            Response.AppendHeader("content-disposition", "attachment; filename=\"" + TemplateName + ".zip\"");
            Response.CacheControl = "Private";
            Response.Cache.SetExpires(DateTime.Now.AddMinutes(3));
            Response.TransmitFile(filename);
            Response.Flush();
            Response.End();
        }

        protected void cbFullScreen_CheckedChanged(object sender, EventArgs e)
        {
            ScopeWrapper.CssClass = cbFullScreen.Checked ? "overlay" : "";
            Response.Cookies["cbFullScreen"].Value = cbFullScreen.Checked.ToString();
        }


    }
}

