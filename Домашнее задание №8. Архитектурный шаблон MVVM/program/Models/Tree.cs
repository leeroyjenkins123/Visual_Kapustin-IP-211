using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.Models
{
    public class Tree
    {
        public ObservableCollection<Tree>? SubTree { get; }
        public string Title { get; }

        public Tree(string title)
        {
            Title = title;
        }

        public Tree(string title, ObservableCollection<Tree> subTree)
        {
            Title = title;
            SubTree = subTree;
        }
    }
}
