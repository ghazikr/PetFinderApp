using Newtonsoft.Json;
using PetFinderApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetFinderApp.Services
{
    public class UserService
    {
        public static string serverUrl = "http://192.168.1.3";
        public async Task<string> InsertUser(User user)
        {
            var client = new HttpClient();

            var content = JsonConvert.SerializeObject(user);


            var response = await client.PostAsync(serverUrl + "/PetFinderApp/signup.php",
                new StringContent(content, Encoding.UTF8, "text/json"));

            //read the json response of the server
            var serverResponse = await response.Content.ReadAsStringAsync();

            var serverResponseDeserialized = JsonConvert.DeserializeObject<IDictionary<string, string>>(serverResponse);
            string status = "";

            serverResponseDeserialized.TryGetValue("status", out status);


            return status;
        }


        public async Task<User> login(string email, string password)
        {
            var client = new HttpClient();
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("email", email);
            values.Add("password", password);

            string content = JsonConvert.SerializeObject(values);


            var response = await client.PostAsync(serverUrl + "/PetFinderApp/info.php",
                new StringContent(content, Encoding.UTF8, "text/json"));
            var serverResponse = await response.Content.ReadAsStringAsync();

            var serverResponseDeserialized = JsonConvert.DeserializeObject<User>(serverResponse);
            return serverResponseDeserialized;

            //  var content =
            //  await client.GetStringAsync("http://192.168.1.4/xamarin/info.php?uid=1");
        }
    }
}
