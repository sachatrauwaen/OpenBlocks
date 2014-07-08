using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Portals;
using System.IO;
using System.Web.Hosting;
using System.Drawing;


namespace Satrabel.OpenBlocks.DataSource
{
    public class DnnDataSourceProvider : DataSourceProvider
    {
        public static UserInfo User(int UserId){
            return UserController.GetUser(PortalSettings.Current.PortalId, UserId, true);
        }
        public static IEnumerable<UserInfo> Users()
        {
            return UserController.GetUsers(PortalSettings.Current.PortalId).Cast<UserInfo>();
        }
        public static IEnumerable<string> Images(string path)
        {
            return Directory.GetFiles(HostingEnvironment.MapPath(path)).Select(f => path + "/" + Path.GetFileName(f));
        }
        public static IEnumerable<ImageModel> ImagesExt(string path)
        {
            foreach(var item in  Images(path))
            {
                Size size = GetSize(HostingEnvironment.MapPath(item));
                yield return new ImageModel()
                {
                    Path = item,
                    Width = size.Width,
                    Height = size.Height
                };
            }
        }

        private static Size GetSize(string path)
        {
            using (Bitmap b = new Bitmap(path))
            {
                return b.Size;
            }
        }

        public static FilesModel Files(string path, string searchPatern)
        {
            FilesModel model = new FilesModel();
            var files = Directory.GetFiles(HostingEnvironment.MapPath(path), searchPatern).Select(f => path + "/" + Path.GetFileName(f));
            model.Items.AddRange(files);
            return model;
        }
    }
    public class FilesModel
    {
        public FilesModel()
        {
            Items = new List<string>();
        }
        public List<string> Items { get; private set; }
    }
    public class ImageModel {
        public string Path { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }



}