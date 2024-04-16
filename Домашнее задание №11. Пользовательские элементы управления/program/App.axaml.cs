using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using program.Models;
using program.ViewModels;
using program.Views;

namespace program;

public partial class App : Application
{

    private List<Presentation> presentations =
            new List<Presentation>();
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        List<User> people = new List<User>();

        string info = "";
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/users");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    info = reader.ReadToEnd();
                }
            }
        }
        catch (HttpRequestException)
        {
            throw new HttpRequestException();
        }

        try
        {
            people = JsonConvert.DeserializeObject<List<User>>(info);
        }
        catch (Newtonsoft.Json.JsonException)
        {
            throw new Newtonsoft.Json.JsonException();
        }

        foreach (User person in people)
        {
            presentations.Add(new Presentation(person));
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(presentations),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}