using System.Collections.Generic;
using System.Linq;

namespace program.Models
{
    public class Composite : Component
    {
        public Composite(string name, object value) : base(name, value) { }

        public override void Add(Component component)
        {
            Childern.Add(component);
        }
        public override void Remove(Component component)
        {
            Childern.Remove(component);
        }
    }
}
