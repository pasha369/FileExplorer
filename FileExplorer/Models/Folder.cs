using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileExplorer.Models
{
    public class Folder
    {
        public string Host { get; set; }
        public List<string> Dirs { set; get; }
        public List<String> Files { get; set; }
        public int Less10Mb { get; set; }
        public int Mb10Mb50 { get; set; }
        public int More100Mb { get; set; }
        public string CurrentDir { get; set; }
        public Folder()
        {
            Dirs = new List<string>();
            Files = new List<string>();
        }
    }
}