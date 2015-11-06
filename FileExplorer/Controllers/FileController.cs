using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.AccessControl;
using System.Web;
using System.Web.Http;
using FileExplorer.Core;
using FileExplorer.Models;
using WebGrease.Css.Extensions;

namespace FileExplorer.Controllers
{
    public class FileController : ApiController
    {
        [HttpGet]
        public Folder GetDirSub(string path)
        {
            FolderHelper helper = new FolderHelper();
            Folder folder = new Folder();

            if (Directory.Exists(path))
            {
                folder.CurrentDir = path;

                helper.GetFiles(path, folder);
                helper.GetDirs(path, folder);

                folder.Host = HttpContext.Current.Request.Url.Host;
            }
            return folder;
        }
        [HttpGet]
        public Folder GetRootDirs()
        {
            FolderHelper helper = new FolderHelper();
            Folder folder = new Folder();

            folder.CurrentDir = "";
            var dirs = DriveInfo.GetDrives().Select(dir => dir.Name);
            foreach (var dir in dirs)
            {
                if(dir != "C:\\")
                    helper.GetSizes(dir, folder);
            }
            folder.Dirs.AddRange(dirs);

            return folder;
        }
    }
}
