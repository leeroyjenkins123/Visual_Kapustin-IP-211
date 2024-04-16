using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using program.ViewModels;
using program.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ReactiveUI;
using System.Reflection;
using Avalonia.Controls;

namespace program.ViewModels
{
    public class TreeListViewModel : ViewModelBase
    {
        private ObservableCollection<Tree> treeView;
        public ObservableCollection<Tree> TreeView
        {
            get => treeView;
            set => this.RaiseAndSetIfChanged(ref treeView, value);
        }

        private Component component;

        private Presentation model;

        public TreeListViewModel(Presentation model)
        {
            this.model = model;
            TreeView = model.GetPresentation();
        }

    }

    public class Factory
    {
        private HashSet<Type> baseTypes = new HashSet<Type>
        {   typeof(string),
            typeof(int),
            typeof(double),
            typeof(long),
            typeof(float),
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(char),
            typeof(decimal)
        };

        private Component Recursive(Type treeType, Component curr, object property)
        {

            foreach (var prop in treeType.GetProperties(BindingFlags.Instance | BindingFlags.Instance | BindingFlags.Public))
            {
                if (baseTypes.Contains(prop.PropertyType))
                {
                    if (property != null)
                    {
                        curr.Add(new Leaf(prop.Name, prop.GetValue(property)));
                    }
                    else
                    {
                        curr.Add(new Leaf(prop.Name, "Error!"));
                    }
                }
                else
                {
                    curr.Add(Recursive(prop.PropertyType, new Composite(prop.Name, ""), prop.GetValue(property)));
                }
            }
            return curr;
        }

        public Component Create(object obj)
        {
            var type = obj.GetType();
            Component component;

            if (type.GetProperties().Count() != 0)
            {
                component = new Composite(type.Name, "");
                Recursive(obj.GetType(), component, obj);

                return component;
            }
            else
            {
                return new Leaf(type.Name, obj);
            }
        }
    }

    public class Adaptation
    {
        private Component component1;
        public Adaptation(Component component)
        {
            this.component1 = component;
        }

        private ObservableCollection<Tree> AdaptRecur(ObservableCollection<Tree> currLvL, Component component)
        {
            if (component is Leaf leaf)
            {
                currLvL.Add(new Tree(leaf.Name, leaf.Value));
            }

            if (component is Composite composite)
            {
                foreach (var child in composite.Childern)
                {
                    if (child is Leaf nestedLeaf)
                    {
                        currLvL.Add(new Tree(nestedLeaf.Name, nestedLeaf.Value));
                    }
                    else
                    {
                        currLvL.Add(new Tree(child.Name,
                            child.Value,
                            AdaptRecur(new ObservableCollection<Tree>(), child)));
                    }
                }
            }

            return currLvL;
        }
        public ObservableCollection<Tree> Adapt()
        {
            ObservableCollection<Tree> treeview = new ObservableCollection<Tree>();
            if (component1 is Composite composite)
            {
                treeview.Add(new Tree(composite.Name, composite.Value, AdaptRecur(new ObservableCollection<Tree>(), component1)));
            }
            return treeview;
        }
    }

}