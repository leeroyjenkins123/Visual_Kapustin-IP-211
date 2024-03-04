using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace program
{
    internal abstract class FoldersNFiles : Base
    {
        private string name = "";

        private string fullname = "";

        public string FullName
        {
            get => fullname;
            set => _ = SetField(ref fullname, value);
        }

        public string Name
        {
            get => name;
            set => _ = SetField(ref name, value);
        }

        public FoldersNFiles(string name)
        {
            Name = name;
        }
    }

    internal class DirModel : FoldersNFiles
    {
        public DirModel(string directory) : base(directory)
        {
            FullName = directory;
        }

        public DirModel(DirectoryInfo directory) : base(directory.Name)
        {
            FullName = directory.FullName;
        }
    }

    internal class ObjModel : FoldersNFiles
    {
        public ObjModel(string name) : base(name)
        {

        }

        public ObjModel(FileInfo file) : base(file.Name)
        {
            FullName = file.FullName;
        }
    }
}