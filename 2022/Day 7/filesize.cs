using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7
{
    public class filesize
    {
        public static Folder calculate(Folder folder)
        {
            var foldersize = 0;

            foreach (var subfolder in folder.folders)
            {
                if (subfolder.foldersize == 0)
                {
                    subfolder.foldersize = filesize.calculate(subfolder).foldersize;
                }

                foldersize = foldersize + subfolder.foldersize;

            }
            foreach (var file in folder.filesizes)
            {
                foldersize = foldersize + file;
            }
            folder.foldersize=foldersize;

            return folder;
        }

        public static List<int> list(Folder folder)
        {
            var fandfsizes = new List<int>();

            foreach (var subfolder in folder.folders)
            {
                var subsize=filesize.list(subfolder);
                fandfsizes.AddRange(subsize);
                fandfsizes.Add(subfolder.foldersize);
            }

            return fandfsizes;
        }


        public static int total(List<int> fandfsizes)
        {
            var number= 0;

            foreach (var size in fandfsizes)
            {
                if (size <= 100000)
                {
                    number = number + size;
                }
            }
            return number;
        }


        public static List<Folder> dirlist(Folder folder)
        {
            var alldirlist = new List<Folder>();

            foreach (var subfolder in folder.folders)
            {
                var subsubfolders = new List<Folder>();
                if (subfolder.folders.Count > 0)
                {
                    subsubfolders = filesize.dirlist(subfolder);
                }
                alldirlist.Add(subfolder);
                alldirlist.AddRange(subsubfolders);

            }

            return alldirlist;
        }


    }
}
