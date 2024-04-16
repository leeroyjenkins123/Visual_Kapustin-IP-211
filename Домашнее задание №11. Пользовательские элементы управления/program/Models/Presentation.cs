using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using program.ViewModels;

namespace program.Models
{
    public class Presentation
    {
        private Component presentationComponent;

        private ObservableCollection<Tree> treeView;

        public Presentation(object presentationObject)
        {
            var factory = new Factory();

            if (presentationObject is User user)
            {
                presentationComponent = factory.Create(user);
            }

            Adaptation toTreeViewAdapter =
                new Adaptation(presentationComponent);

            treeView = toTreeViewAdapter.Adapt();
        }

        public ObservableCollection<Tree> GetPresentation()
        {
            return this.treeView;
        }
    }
}
