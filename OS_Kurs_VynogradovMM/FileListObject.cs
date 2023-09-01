using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Kurs_VynogradovMM
{
    public class FileList
    {
        string Path;
        string Name;
        public FileList(string path, string name)
        {
            Path = path;
            Name = name;
        }
        public string getPath() { return Path; }
        public string getName() { return Name; }
    }
}
