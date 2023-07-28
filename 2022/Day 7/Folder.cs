using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7
{
    public class Folder
    {
        public Folder( string name, Folder? parent)
        {
            this.files = new List<string>();
            this.filesizes = new List<int>();
            this.folders = new List<Folder>();
            this.name = name;
            this.parent = parent;
        }

        public List<string> files { get; set; }

        public List<int> filesizes { get; set; }

        public List<Folder> folders { get; set; }

        public int foldersize { get; set; }

        public string name { get; set; }

        public Folder? parent { get; set; }


    }
}
