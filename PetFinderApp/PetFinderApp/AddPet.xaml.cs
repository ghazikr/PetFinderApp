using Newtonsoft.Json;
using PCLStorage;
using PetFinderApp.Models;
using PetFinderApp.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPet : ContentPage
    {
        private int UserId;
        private PetService PetService;
        private string imgURL;
        private MediaFile file;


        public AddPet(int userid)
        {

            UserId = userid;
            PetService = new PetService();
            InitializeComponent();
            Gender.SelectedIndex = 0;
            adopOrlost.SelectedIndex = 0;

            pickPhoto.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(
                    new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                    });


                if (file == null)
                    return;


                //pathlabel.Text = "path = " + file.Path;


                /* // select a file and made a copy of it
                 * IFile fileToBeMoved = await FileSystem.Current.GetFileFromPathAsync(file.Path);
                  var newFile = "/storage/sdcard/" + DateTime.Now.Ticks + ".jpg";
                DependencyService.Get<IFileHelper>().Copy(fileToBeMoved.Path, newFile);
                */

                /*image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    //file.Dispose();
                    return stream;
                });*/



            };
        }



        private async Task<string> UploadFile()
        {
            // made copy of file and rename it
            IFile OriginalFile = await FileSystem.Current.GetFileFromPathAsync(file.Path);

            var newFile = Path.GetDirectoryName(OriginalFile.Path) + DateTime.Now.Ticks + Path.GetExtension(file.Path);

            DependencyService.Get<IFileHelper>().Copy(OriginalFile.Path, newFile);

            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(file.GetStream()),
                "\"file\"",
                $"\"{newFile}\"");

            var httpClient = new HttpClient();

            var uploadServiceBaseAddress = UserService.serverUrl + "/PetFinderApp/upload.php";


            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

            var serverResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var serverResponseDeserialized = JsonConvert.DeserializeObject<IDictionary<string, string>>(serverResponse);
            string pathImage = "";

            serverResponseDeserialized.TryGetValue("path", out pathImage);
            // delete file
            //file.Dispose();
            return pathImage;
        }

        private async void btn_click(object sender, EventArgs e)
        {

            var WebSiteUrl = UserService.serverUrl + "/PetFinderApp";
            var pathImage = await UploadFile();
            var FullPathImage = WebSiteUrl + "/Upload/" + pathImage;
            var adopt = 1;
            var lost = 1;
            if (!string.IsNullOrWhiteSpace(pathImage))
            {
                if (adopOrlost.SelectedItem.ToString() == "For Adoption")
                {
                    adopt = 0;
                    lost = 0;
                }

                Pet pet = new Pet()
                {
                    name = Name.Text,
                    breed = Breed.Text,
                    age = Convert.ToInt32(Age.Text),
                    gender = Gender.SelectedItem.ToString(),
                    idUser = UserId,
                    ImageUrl = FullPathImage,
                    height = Convert.ToInt32(Height.Text),
                    weight = Convert.ToInt32(weight.Text),
                    isAdopted = adopt,
                    isLost = lost,
                    health = health.Text


                };

                //DisplayAlert("jjj", imgConverted, "kkk");
                var k = pet.breed;
                var status = await PetService.addPet(pet);

                if (status == "1")
                {
                    await DisplayAlert("Success", "Pet Added", "ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Try again !", "Ok");
                }
            }
        }
    }
}