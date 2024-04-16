using System.Collections.Generic;

namespace program.Models
{
    public abstract class Component
    {
        private List<Component> children = new List<Component>();
        public List<Component> Childern
        {
            get => children;
            private set => children = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }

        public Component(string name, object value)
        {
            Name = name;
            Value = value;
        }

        protected Component(string name)
        {
            Name = name;
        }

        public abstract void Add(Component component);
        public abstract void Remove(Component component);
    }
}
