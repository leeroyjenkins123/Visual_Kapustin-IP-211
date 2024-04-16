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
        public object Value { get; }

        public Tree(string title, object value)
        {
            Title = title;
            Value = value;
        }

        public Tree(string title, object value, ObservableCollection<Tree> subTree)
        {
            Title = title;
            Value = value;
            SubTree = subTree;
        }
    }
}
