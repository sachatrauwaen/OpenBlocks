#region Copyright

// 
// Copyright (c) 2014
// by SatraBel
// 

#endregion

#region Using Statements

using System;
using DotNetNuke.Entities.Modules;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;
using System.IO;
using DotNetNuke.Common;
using ICSharpCode.SharpZipLib.Zip;
using System.Web.UI.HtmlControls;
using System.Web;
using DotNetNuke.Web.UI.WebControls;

#endregion

namespace SatraBel.TemplateEditor
{
    public partial class Template_Edit : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TemplateEditorUtils.ModuleDataBind(ddlModule, PortalId, LocalResourceFile, Server);
            }
        }
        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTypeCopy.Visible = false;
            ddlTypeCopy.Visible = false;
            lblTemplate.Visible = false;
            ddlTemplate.Visible = false;
            ddlTypeCopy.SelectedIndex = 0;
            ddlTemplate.SelectedIndex = 0;
            lblZip.Visible = false;
            updZip.Visible = false;

            if ((rblMode.SelectedValue == "0" || rblMode.SelectedValue == "2") && (ddlModule.SelectedIndex > 0 && ddlType.SelectedIndex > 0))
                btnValid.Enabled = true;


            
            if (rblMode.SelectedValue == "1")
            {
                //lblMode.Text = Localization.GetString("lblModeCopy", LocalResourceFile);
                //lblMode.HelpText = Localization.GetString("lblModeCopy.Help", LocalResourceFile);
                lblMode.Visible = true;
                lblTypeCopy.Visible = true;
                ddlTypeCopy.Visible = true;
                lblTemplate.Visible = true;
                ddlTemplate.Visible = true;
            }
            else
            {
                if (rblMode.SelectedValue == "2")
                {
                    //lblMode.Text = Localization.GetString("lblModeZip", LocalResourceFile);
                    //lblMode.HelpText = Localization.GetString("lblModeZip.Help", LocalResourceFile);
                    lblMode.Visible = true;
                    lblZip.Visible = true;
                    updZip.Visible = true;
                }
            }
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTemplate.Items.Clear();
            ddlTemplate.Items.Add(new ListItem(Localization.GetString("selectTemplate", LocalResourceFile), ""));
            ddlTypeCopy.SelectedIndex = 0;
            ddlTemplate.SelectedIndex = 0;

            ddlActions();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlActions();
        }

        private void ddlActions()
        {
            if (rblMode.SelectedValue == "0" || rblMode.SelectedValue == "2")
            {
                if (ddlModule.SelectedIndex > 0 && ddlType.SelectedIndex > 0)
                    btnValid.Enabled = true;

                if (rblMode.SelectedValue == "2")
                {
                    //lblMode.Text = Localization.GetString("lblModeZip", LocalResourceFile);
                    //lblMode.HelpText = Localization.GetString("lblModeZip.Help", LocalResourceFile);
                }
            }
            else
            {
                if (rblMode.SelectedValue == "1")
                {
                    //lblMode.Text = Localization.GetString("lblModeCopy", LocalResourceFile);
                    //lblMode.HelpText = Localization.GetString("lblModeCopy.Help", LocalResourceFile);

                    if (ddlModule.SelectedIndex > 0 && ddlType.SelectedIndex > 0 && ddlTypeCopy.SelectedIndex > 0 && ddlTemplate.SelectedIndex > 0)
                    {
                        btnValid.Enabled = true;
                    }
                }
            }
        }

        protected void ddlTypeCopy_SelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateEditorUtils.TemplateDataBind(ddlModule, ddlTypeCopy, ddlTemplate, PortalId, LocalResourceFile, Server);
            if (rblMode.SelectedValue == "1")
            {
                //lblMode.Text = Localization.GetString("lblModeCopy", LocalResourceFile);
                //lblMode.HelpText = Localization.GetString("lblModeCopy.Help", LocalResourceFile);
            }
        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnValid.Enabled = ddlModule.SelectedIndex > 0 && ddlType.SelectedIndex > 0 && ddlTypeCopy.SelectedIndex > 0 && ddlTemplate.SelectedIndex > 0;
            if (rblMode.SelectedValue == "1")
            {
                //lblMode.Text = Localization.GetString("lblModeCopy", LocalResourceFile);
                //lblMode.HelpText = Localization.GetString("lblModeCopy.Help", LocalResourceFile);
            }
        }

        protected void btnValid_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["lkbRefresh"] != null)
            {
                HttpCookie myCookie = new HttpCookie("lkbRefresh");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            Response.Cookies["ddlModule"].Value = ddlModule.SelectedValue;
            Response.Cookies["ddlType"].Value = ddlType.SelectedValue;
            Response.Cookies["ddlTemplate"].Value = tbxNewTemplate.Text;

            if (rblMode.SelectedValue == "0")
            {
                string newpath = Server.MapPath(TemplateEditorUtils.GenerateDirectory(ddlModule, ddlType, tbxNewTemplate, PortalId, Server));
                Directory.CreateDirectory(newpath);
                Response.Redirect(Globals.NavigateURL(), true);
            }
            else
            {
                if (rblMode.SelectedValue == "1")
                {
                    string newpath = Server.MapPath(TemplateEditorUtils.GenerateDirectory(ddlModule, ddlType, tbxNewTemplate, PortalId, Server));
                    Directory.CreateDirectory(newpath);
                    string copypath = Server.MapPath(TemplateEditorUtils.GenerateDirectory(ddlModule, ddlTypeCopy, ddlTemplate, PortalId, Server));
                    CopyDirectoriesAndFiles(copypath, newpath);
                    Response.Redirect(Globals.NavigateURL(), true);
                }
                else
                {
                    if (rblMode.SelectedValue == "2" && updZip.HasFile)
                    {
                        string newpath = Server.MapPath(TemplateEditorUtils.GenerateDirectory(ddlModule, ddlType, tbxNewTemplate, PortalId, Server));
                        Directory.CreateDirectory(newpath);
                        UnzipFile(updZip.PostedFile);
                        Response.Redirect(Globals.NavigateURL(), true);
                    }
                    else
                    {
                        //lblMode.Text = Localization.GetString("lblModeZip", LocalResourceFile);
                        //lblMode.HelpText = Localization.GetString("lblModeZip.Help", LocalResourceFile);
                        btnValid.Enabled = true;
                        lblemptyFile.Visible = true;
                        lblemptyFile.Text = Localization.GetString("emptyFile", LocalResourceFile);

                    }
                }
            }
        }

        private void CopyDirectoriesAndFiles(string copypath, string newpath)
        {
            //Copy Files

            var filesList = Directory.GetFiles(copypath);
            foreach (string copyfile in filesList)
            {
                string FileName = Path.GetFileName(copyfile);
                string newfile = newpath + "\\" + FileName;
                File.Copy(copyfile, newfile);
            }

            //Copy Directories

            var dryList = Directory.GetDirectories(copypath);

            foreach (string copysubdir in dryList)
            {
                string newsubdir = newpath + "\\" + copysubdir.Substring(copypath.Length);
                Directory.CreateDirectory(newsubdir);
                CopyDirectoriesAndFiles(copysubdir, newsubdir);
            }
        }


        private void UnzipFile(HttpPostedFile objFileUpload)
        {
            ZipInputStream s = new ZipInputStream(objFileUpload.InputStream);
            ZipEntry theEntry;
            string virtualPath = "~" + TemplateEditorUtils.GenerateDirectory(ddlModule, ddlType, tbxNewTemplate, PortalId, Server) + "/";
            string fileName = string.Empty;
            string folderName = string.Empty;
            string fileExtension = string.Empty;
            string fileSize = string.Empty;

            while ((theEntry = s.GetNextEntry()) != null)
            {
                folderName = Path.GetDirectoryName(theEntry.Name);
                fileName = Path.GetFileName(theEntry.Name);
                fileExtension = Path.GetExtension(fileName);

                if (!string.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(folderName))
                            Directory.CreateDirectory(Server.MapPath(virtualPath + folderName));

                        FileStream streamWriter = File.Create(Server.MapPath(virtualPath + folderName + "/" + fileName));
                        int size = 2048;
                        byte[] data = new byte[2048];

                        do
                        {
                            size = s.Read(data, 0, data.Length);
                            streamWriter.Write(data, 0, size);
                        } while (size > 0);

                        fileSize = Convert.ToDecimal(streamWriter.Length / 1024).ToString() + " KB";

                        streamWriter.Close();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                }
            }
            s.Close();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(), true);
        }
    }
}