using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;

/// <summary>
/// Description résumée de TemplateEditorUtils
/// </summary>
public class TemplateEditorUtils
{
    public static string GenerateDirectory(DropDownList ddlModule, DropDownList ddlType, DropDownList ddlTemplate, int PortalId, HttpServerUtility Server)
    {
        return GenerateDirectory(ddlModule.SelectedValue, int.Parse(ddlType.SelectedValue), ddlTemplate.SelectedValue, PortalId, Server);
    }

    public static string GenerateDirectory(DropDownList ddlModule, DropDownList ddlType, TextBox tbxNewTemplate, int PortalId, HttpServerUtility Server)
    {
        return GenerateDirectory(ddlModule.SelectedValue, int.Parse(ddlType.SelectedValue), tbxNewTemplate.Text, PortalId, Server);
    }
    public static string GenerateDirectory(string Module, int Type, string template, int PortalId, HttpServerUtility Server)
    { 
        return GenerateDirectory(Module, Type, template, PortalId, Server, false);
    }
    public static string GenerateDirectory(string Module, int Type, string template, int PortalId, HttpServerUtility Server, bool CheckDirExist)
    {
        string path = "";

        string[] pathLst = GetPathList(Module, Type, PortalId);
        path = pathLst[0];
        foreach (string item in pathLst)
        {

            if (!CheckDirExist || Directory.Exists(Server.MapPath(item)))
            {
                path = item + template;
                break;
            }
        }
        return path;
    }

    private static string[] GetPathList(string FolderName, int SelectedType, int PortalId)
    {
        string type = "_default";
        if (SelectedType == 3)
            type = PortalId.ToString();


        if (FolderName.StartsWith("Easy"))
        {
            if (SelectedType == 1)
                return new string[]{
                    "/DesktopModules/" + FolderName + "/Templates/" + type + "/"
                };
            else if (SelectedType == 2)
                return new string[]{                    
                    "/Portals/" + type + "/" + FolderName + "/Templates/",
                    "/Portals/" + type + "/" + FolderName + "/"
                };
            else
                return new string[]{
                    "/DesktopModules/" + FolderName + "/Templates/" + type + "/",                
                    "/Portals/" + type + "/" + FolderName + "/Templates/",
                    "/Portals/" + type + "/" + FolderName + "/"
                };

        }
        else if (FolderName == "skins" || FolderName == "containers")
        {
            if (SelectedType == 1)
                return new string[]{
                    "/DesktopModules/" + FolderName + "/Templates/" + type + "/"
                };
            else if (SelectedType == 2)
                return new string[]{                    
                   
                    "/Portals/" + type + "/" + FolderName + "/"
                };
            else
                return new string[]{
                  
                    "/Portals/" + type + "/" + FolderName + "/"
                };
        }
        else if (FolderName == "RazorModules/RazorHost")
        {
            if (SelectedType == 1)
                return new string[]{
                    "/DesktopModules/" + FolderName + "/Scripts/"
                };
            else if (SelectedType == 2)
                return new string[]{                    
                   
                    "/Portals/" + type + "/" + FolderName + "/Scripts/"
                };
            else
                return new string[]{
                  
                    "/Portals/" + type + "/" + FolderName + "/Scripts/"
                };
        }
        else
        {
            if (SelectedType == 1)
                return new string[]{
                    "/DesktopModules/" + FolderName + "/Templates/"
                };
            else
                return new string[]{
                    "/Portals/" + type + "/" + FolderName + "/Templates/"                
                };

        }


    }

    public static void ModuleDataBind(DropDownList ddlModule, int PortalId, string LocalResourceFile, HttpServerUtility Server)
    {

        ddlModule.Items.Clear();
        ddlModule.Items.Add(Localization.GetString("selectModule", LocalResourceFile));

        DesktopModuleController mc = new DesktopModuleController();
        var dtmLst = DesktopModuleController.GetDesktopModules(PortalId).Values.Where(m => !m.IsAdmin && !m.FolderName.StartsWith("Admin"));
        foreach (DesktopModuleInfo dtm in dtmLst)
        {
            bool TemplateExist = false;
            for (int i = 1; i < 4; i++)
            {
                string[] pathLst = GetPathList(dtm.FolderName, i, PortalId);
                foreach (string pathitem in pathLst)
                {
                    if (Directory.Exists(Server.MapPath(pathitem)))
                    {
                        ddlModule.Items.Add(new ListItem(dtm.FriendlyName, dtm.FolderName));
                        TemplateExist = true;
                        break;
                    }
                }
                if (TemplateExist) break;
            }
        }
        ddlModule.Items.Add(new ListItem("Skins", "skins"));
        ddlModule.Items.Add(new ListItem("Containers", "containers"));
        ddlModule.Items.Add(new ListItem("Widgets", "widgets"));
    }

    public static void TemplateDataBind(DropDownList ddlModule, DropDownList ddlType, DropDownList ddlTemplate, int PortalId, string LocalResourceFile, HttpServerUtility Server)
    {
        TemplateDataBind(ddlModule.SelectedValue, int.Parse(ddlType.SelectedValue), ddlTemplate, PortalId, LocalResourceFile, Server);
    }

    public static void TemplateDataBind(string Module, int Type, DropDownList ddlTemplate, int PortalId, string LocalResourceFile, HttpServerUtility Server)
    {
        if (Type > 0)
        {
            ddlTemplate.Items.Clear();
            ddlTemplate.Items.Add(new ListItem(Localization.GetString("selectTemplate", LocalResourceFile), ""));
            string path = TemplateEditorUtils.GenerateDirectory(Module, Type, ddlTemplate.SelectedValue, PortalId, Server);
            if (Directory.Exists(Server.MapPath(path)))
            {
                var dryLst = Directory.GetDirectories(Server.MapPath(path));

                foreach (string item in dryLst)
                {
                    int nb = item.LastIndexOf('\\');
                    ddlTemplate.Items.Add(item.Substring(nb + 1));
                }
            }
        }
        else
        {
            ddlTemplate.Items.Clear();
            ddlTemplate.Items.Add(new ListItem(Localization.GetString("selectTemplate", LocalResourceFile), ""));
        }
    }

    public static void TemplateDataBind(string Module, DropDownList ddlTemplate, int PortalId, string LocalResourceFile, HttpServerUtility Server)
    {

        ddlTemplate.Items.Clear();
        ddlTemplate.Items.Add(new ListItem(Localization.GetString("selectTemplate", LocalResourceFile), ""));
        for (int type = 1; type < 4; type++)
        {
            string path = TemplateEditorUtils.GenerateDirectory(Module, type, "", PortalId, Server);
            if (Directory.Exists(Server.MapPath(path)))
            {
                var dryLst = Directory.GetDirectories(Server.MapPath(path));
                if (dryLst.Count() > 0 && Directory.GetFiles(Server.MapPath(path)).Count() == 0)
                {
                    foreach (string item in dryLst)
                    {
                        int nb = item.LastIndexOf('\\');
                        ddlTemplate.Items.Add(new ListItem(GetTypes()[type] + " - " + item.Substring(nb + 1), ReverseMapPath(item)));
                    }
                }
                else
                {
                    ddlTemplate.Items.Add(new ListItem(GetTypes()[type] , path));

                }
            }
        }
    }
    public static void FileDataBind(string Module, DropDownList ddlTemplate, DropDownList ddlFile, int PortalId, string LocalResourceFile, HttpServerUtility Server)
    {
        ddlFile.Items.Clear();
        ddlFile.Items.Add(new ListItem(Localization.GetString("selectFile", LocalResourceFile), ""));
        string dir = Server.MapPath(ddlTemplate.SelectedValue);
        if (!string.IsNullOrEmpty(ddlTemplate.SelectedValue) && Directory.Exists(dir))
        {
            var fileLst = GetFiles(dir, new string[] { ".cshtml", ".liquid", ".html", ".htm" });
            foreach (string item in fileLst)
            {
                int nb = item.LastIndexOf('\\');
                ddlFile.Items.Add(item.Substring(nb + 1));
            }
        }
    }
    private static List<string> GetFiles(string dir, string[] extensions)
    {
        List<string> lst = new List<string>();
        lst.AddRange(Directory.GetFiles(dir).Where(f => extensions.Contains(Path.GetExtension(f))));
        foreach (var item in Directory.GetDirectories(dir))
        {
            lst.AddRange(GetFiles(item, extensions));
        }
        return lst;
    }

    public static Dictionary<int, string> GetTypes()
    {
        Dictionary<int, string> dic = new Dictionary<int, string>();
        dic.Add(1, "System");
        dic.Add(2, "Host");
        dic.Add(3, "Site");
        return dic;
    }

    public static string ReverseMapPath(string path)
    {
        string appPath = HttpContext.Current.Server.MapPath("~");
        string res = string.Format("~/{0}", path.Replace(appPath, "").Replace("\\", "/"));
        return res;
    }

    public static void ZipFolder(string RootFolder, string CurrentFolder, ZipOutputStream zStream)
    {
        string[] SubFolders = Directory.GetDirectories(CurrentFolder);

        foreach (string Folder in SubFolders)
        {
            ZipFolder(RootFolder, Folder, zStream);
        }

        string relativePath = CurrentFolder.Substring(RootFolder.Length) + "\\";

        if (relativePath.Length > 1)
        {
            ZipEntry dirEntry;
            dirEntry = new ZipEntry(relativePath);
            dirEntry.DateTime = DateTime.Now;
        }

        foreach (string file in Directory.GetFiles(CurrentFolder))
        {
            AddFileToZip(zStream, relativePath, file);
        }
    }

    private static void AddFileToZip(ZipOutputStream zStream, string relativePath, string file)
    {
        byte[] buffer = new byte[4096];

        string fileRelativePath = (relativePath.Length > 1 ? relativePath : string.Empty)
                                  + Path.GetFileName(file);

        ZipEntry entry = new ZipEntry(fileRelativePath);
        entry.DateTime = DateTime.Now;
        zStream.PutNextEntry(entry);

        using (FileStream fs = File.OpenRead(file))
        {
            int sourceBytes;
            do
            {
                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                zStream.Write(buffer, 0, sourceBytes);
            } while (sourceBytes > 0);
        }
    }
    public static void DeleteDirectory(string dirpath)
    {
        string[] files = Directory.GetFiles(dirpath);
        string[] dirs = Directory.GetDirectories(dirpath);

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        foreach (string dir in dirs)
        {
            DeleteDirectory(dir);
        }

        DeleteFolderWithWait(dirpath, 10, 10);
    }

    public static bool DeleteFolderWithWait(string dirname, Int16 waitInMilliseconds, Int16 maxAttempts)
    {
        if (!Directory.Exists(dirname))
        {
            return true;
        }
        bool dirDeleted = false;
        int i = 0;
        while (dirDeleted != true)
        {
            if (i > maxAttempts)
            {
                break;
            }
            i = i + 1;
            try
            {
                if (Directory.Exists(dirname))
                {
                    Directory.Delete(dirname);
                }
                dirDeleted = true;
            }
            catch (Exception exc)
            {

                dirDeleted = false;
            }
            if (dirDeleted == false)
            {
                Thread.Sleep(waitInMilliseconds);
            }
        }
        return dirDeleted;
    }

}