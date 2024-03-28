using System.Collections.Generic;
using System.Collections.ObjectModel;
using program.Models;
using ReactiveUI;
using program.ViewModels.Pages;

namespace program.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    private object content;
    public object Content
    {
        get => content;
        set => this.RaiseAndSetIfChanged(ref content, value);
    }

    private ObservableCollection<BasePageViewModel> baseCollection;
    public ObservableCollection<BasePageViewModel> BaseCollection
    {
        get => baseCollection;
        set => this.RaiseAndSetIfChanged(ref baseCollection, value);
    }

    public MainWindowViewModel()
    {
        baseCollection = new ObservableCollection<BasePageViewModel>();
        baseCollection.Add(new DataGridViewModel());
        baseCollection.Add(new DataTreeViewModel());

        Content = baseCollection[0];

    }
}
