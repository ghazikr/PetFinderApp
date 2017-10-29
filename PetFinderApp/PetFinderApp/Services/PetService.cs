using Newtonsoft.Json;
using PetFinderApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetFinderApp.Services
{
    public class PetService
    {
        //public static string serverUrl = "http://192.168.8.4";
        public async Task<ObservableCollection<Pet>> getPets(int UserId)
        {

            var client = new HttpClient();
            Dictionary<string, int> values = new Dictionary<string, int>();
            values.Add("UserID", UserId);
            var content = JsonConvert.SerializeObject(values);


            var response = await client.PostAsync(UserService.serverUrl + "/PetFinderApp/GetPetsByUserID.php",
                new StringContent(content, Encoding.UTF8, "text/json"));
            var serverResponse = await response.Content.ReadAsStringAsync();

            var PetsList = JsonConvert.DeserializeObject<ObservableCollection<Pet>>(serverResponse);
            return PetsList;
        }

        public async Task<ObservableCollection<Pet>> getAllLostPets()
        {
            var client = new HttpClient();

            var content = JsonConvert.SerializeObject("");


            var response = await client.PostAsync(UserService.serverUrl + "/PetFinderApp/getAllLostPets.php",
                new StringContent(content, Encoding.UTF8, "text/json"));
            var serverResponse = await response.Content.ReadAsStringAsync();

            var PetsList = JsonConvert.DeserializeObject<ObservableCollection<Pet>>(serverResponse);
            return PetsList;
        }

        public async Task<ObservableCollection<Pet>> getNonAdoptedPets()
        {
            var client = new HttpClient();

            var content = JsonConvert.SerializeObject("");


            var response = await client.PostAsync(UserService.serverUrl + "/PetFinderApp/getNonAdoptedPets.php",
                new StringContent(content, Encoding.UTF8, "text/json"));
            var serverResponse = await response.Content.ReadAsStringAsync();

            var PetsList = JsonConvert.DeserializeObject<ObservableCollection<Pet>>(serverResponse);
            return PetsList;
        }

        public async Task<string> addPet(Pet p)
        {
            var client = new HttpClient();

            var content = JsonConvert.SerializeObject(p);


            var response = await client.PostAsync(UserService.serverUrl + "/PetFinderApp/AddPet.php",
                new StringContent(content, Encoding.UTF8, "text/json"));

            //read the json response of the server
            var serverResponse = await response.Content.ReadAsStringAsync();

            var serverResponseDeserialized = JsonConvert.DeserializeObject<IDictionary<string, string>>(serverResponse);
            string status = "";

            serverResponseDeserialized.TryGetValue("status", out status);


            return status;
        }


        public async Task<string> reportLost(int idPet)
        {
            var client = new HttpClient();

            Dictionary<string, int> values = new Dictionary<string, int>();
            values.Add("idPet", idPet);
            var content = JsonConvert.SerializeObject(values);


            var response = await client.PostAsync(UserService.serverUrl + "/PetFinderApp/ReportLost.php",
                new StringContent(content, Encoding.UTF8, "text/json"));

            //read the json response of the server
            var serverResponse = await response.Content.ReadAsStringAsync();

            var serverResponseDeserialized = JsonConvert.DeserializeObject<IDictionary<string, string>>(serverResponse);
            string status = "";

            serverResponseDeserialized.TryGetValue("status", out status);


            return status;
        }

        public async Task<string> adoptPetById(int idPet, int userId)
        {
            var client = new HttpClient();

            Dictionary<string, int> values = new Dictionary<string, int>();
            values.Add("petId", idPet);
            values.Add("userId", userId);
            var content = JsonConvert.SerializeObject(values);


            var response = await client.PostAsync(UserService.serverUrl + "/PetFinderApp/adoptPetById.php",
                new StringContent(content, Encoding.UTF8, "text/json"));

            //read the json response of the server
            var serverResponse = await response.Content.ReadAsStringAsync();

            var serverResponseDeserialized = JsonConvert.DeserializeObject<IDictionary<string, string>>(serverResponse);
            string status = "";

            serverResponseDeserialized.TryGetValue("status", out status);


            return status;
        }
    }
}
