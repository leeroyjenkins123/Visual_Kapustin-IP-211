using System.Collections.Generic;
using System.Collections.ObjectModel;
using program.Models;
using ReactiveUI;
using program.ViewModels;

namespace program.ViewModels
{

    public class MainWindowViewModel : ViewModelBase
    {
        private object content;
        public object Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        private ObservableCollection<TreeListViewModel> collection = new ObservableCollection<TreeListViewModel>();

        public MainWindowViewModel(List<Presentation> presentations)
        {
            foreach (var model in presentations) { collection.Add(new TreeListViewModel(model)); }

            Content = collection[0];
        }

    }
}