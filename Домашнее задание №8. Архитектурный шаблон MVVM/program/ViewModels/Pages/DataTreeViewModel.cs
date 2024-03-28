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
    public class DataTreeViewModel : BasePageViewModel
    {
        public override string GetName()
        {
            return "DataGridView Page";
        }

        public ObservableCollection<Tree> Tree { get; }

        public DataTreeViewModel()
        {
            List<Person> people = new List<Person>();
            Tree = new ObservableCollection<Tree>();

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

            foreach (var user in people)
            {
                Tree.Add(new Tree("ID: " + user.Id.ToString(), new ObservableCollection<Tree>
                {
                   new Tree("Name: " + user.Name), new Tree("Username: "+user.Username), new Tree("Email: "+user.Email),
                   new Tree("Address: ", new ObservableCollection<Tree>
                   {
                    new Tree("City:"+ user.Address.Street), new Tree("Street: " + user.Address.Street), new Tree("Suite: "+user.Address.Suite),
                    new Tree("Zipcode: "+user.Address.Zipcode), new Tree("Geo: ", new ObservableCollection<Tree>{
                        new Tree("Lat: "+user.Address.Geo.Lat.ToString()), new Tree("Lng: "+user.Address.Geo.Lng.ToString())
                    })
                   }),
                   new Tree("Phone: "+user.Phone), new Tree("Website: "+user.Website), new Tree("Company: ", new ObservableCollection<Tree>{
                    new Tree("Name: "+user.Company.Name), new Tree("CatchPhrase: "+user.Company.CatchPhrase), new Tree("Bs: "+user.Company.Bs)
                   })
                }));
            }
        }
    }
}
