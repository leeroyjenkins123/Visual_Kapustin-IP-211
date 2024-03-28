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

namespace program.ViewModels.Pages
{
    public class DataGridViewModel : BasePageViewModel
    {
        public override string GetName()
        {
            return "DataGridView Page";
        }

        public ObservableCollection<Person> People { get; }

        public DataGridViewModel()
        {
            // var people = new List<Person>
            //     {
            //         new Person("Neil", "Armstrong"),
            //         new Person("Buzz", "Lightyear"),
            //         new Person("James", "Kirk")
            //     };
            // People = new ObservableCollection<Person>(people);

            List<Person> people = new List<Person>();

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
                people = JsonConvert.DeserializeObject<List<Person>>(info);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                throw new Newtonsoft.Json.JsonException();
            }

            People = new ObservableCollection<Person>(people);
        }
    }
}
