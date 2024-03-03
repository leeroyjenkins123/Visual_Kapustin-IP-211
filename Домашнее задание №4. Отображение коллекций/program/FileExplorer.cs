using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace program
{
    internal class FileExplorer : Base
    {
        private ObservableCollection<FoldersNFiles> content;
        private Stack<FoldersNFiles> history = new Stack<FoldersNFiles>();

        private FoldersNFiles selected;

        public FoldersNFiles Selected
        {
            get => selected;
            set => _ = SetField(ref selected, value);
        }

        public ObservableCollection<FoldersNFiles> Directories
        {
            get => content;
            set => _ = SetField(ref content, value);
        }

        public FileExplorer()
        {
            Directories = new ObservableCollection<FoldersNFiles>();

            foreach (var drive in Directory.GetLogicalDrives())
            {
                Directories.Add(new DirModel(drive));
            }
        }

        public void Open(object element)
        {
            if (element is ObjModel objModel)
            {
                return;
            }
            if (element is DirModel dirModel)
            {
                Directories.Clear();
                DirectoryInfo dirInfo = new DirectoryInfo(dirModel.FullName);
                history.Push(dirModel);

                foreach (var dir in dirInfo.GetDirectories())
                {
                    Directories.Add(new DirModel(dir));
                }
                foreach (var file in dirInfo.GetFiles())
                {
                    Directories.Add(new ObjModel(file));
                }
            }
        }

        public void OpenPrev()
        {
            if (this.history.Count() == 1)
            {
                this.Directories.Clear();

                foreach (var drive in Directory.GetLogicalDrives())
                {
                    Directories.Add(new DirModel(drive));
                }
            }

            if (this.history.Count() > 1)
            {
                this.history.Pop();
                this.Open(this.history.Pop());
            }
        }
    }
}