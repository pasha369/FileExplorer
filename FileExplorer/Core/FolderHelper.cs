using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FileExplorer.Models;
using WebGrease.Css.Extensions;

namespace FileExplorer.Core
{
    public class FolderHelper
    {
        /// <summary>
        /// Get a count of file by 3 condition.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="folder">View model</param>
        /// <returns>Path to current dir</returns>
        public string GetSizeCount(string path, Folder folder)
        {
            try
            {
                if (File.Exists(path)) { CountFileSizes(path, folder); }
                if (Directory.Exists(path))
                {
                    foreach (var file in Directory.GetFiles(path))
                    {
                        CountFileSizes(file, folder);
                    }
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        try
                        {
                            GetSizeCount(subDir, folder);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            // swallow, log, whatever
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // If error access denied move next.
            }
            return path;
        }
        /// <summary>
        /// Count number of file by size.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="folder">View model</param>
        private void CountFileSizes(string path, Folder folder)
        {
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                if (info.Length <= 10000000)
                    folder.Less10Mb++;
                if (info.Length >= 10000000 && info.Length <= 50000000)
                    folder.Mb10Mb50++;
                if (info.Length >= 100000000)
                    folder.More100Mb++;
            }
        }
        /// <summary>
        /// Get sub dir by path and save them in folder field(Dirs).
        /// </summary>
        /// <param name="path">Dir path</param>
        /// <param name="folder">View model</param>
        public void GetDirs(string path, Folder folder)
        {
            Directory.GetDirectories(path)
                .Select(dir => this.GetSizeCount(dir, folder))
                .Select(dir => new DirectoryInfo(dir).Name)
                .ForEach(dir => folder.Dirs.Add(dir));
        }
        /// <summary>
        /// Get files by dir path and save them in folder field(Files).
        /// </summary>
        /// <param name="path">Dir path</param>
        /// <param name="folder">View model</param>
        public void GetFiles(string path, Folder folder)
        {
            Directory.GetFiles(path)
                .Select(file => this.GetSizeCount(file, folder))
                .Select(file => new FileInfo(file).Name)
                .ForEach(file => folder.Files.Add(file));
        }
    }
}