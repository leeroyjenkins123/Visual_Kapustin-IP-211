using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.IO;
using System.Collections.Specialized;
using System.Reactive.Linq;

public class CollectionsFactory
{
    public IObservable<NotifyCollectionChangedEventArgs> GetObservable(ObservableCollection<object> collection)
    {
        return Observable.FromEvent<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(handler =>
            {
                NotifyCollectionChangedEventHandler wrappedHandler = (sender, args) => handler(args);
                return wrappedHandler;
            },
            handler => collection.CollectionChanged += handler,
            handler => collection.CollectionChanged -= handler
            );
    }
}

class Program
{
    static void Main(string[] args)
    {
        ObservableCollection<object> collection = new ObservableCollection<object>();
        CollectionsFactory factory = new CollectionsFactory();
        factory.GetObservable(collection).Subscribe(args => LogTofile(args, "Logs.txt"));

        collection.Add("Test");
        collection.Add(123);
        collection.Add("Test 1");
        collection.Remove(123);
    }

    static void LogTofile(NotifyCollectionChangedEventArgs args, string file)
    {

        using (StreamWriter writer = File.AppendText(file))
        {
            writer.WriteLine($"Action: {args.Action}, New Item: {args.NewItems}, Old Item: {args.OldItems}");
        }
    }
}