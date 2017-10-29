using PetFinderApp.Models;
using PetFinderApp.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPets : ContentPage
    {
        private PetService petService;
        private int userid;
        private int currentPetId;
        private Pet pet;
        private ObservableCollection<Pet> pets;
        private Pet selecPet;

        public MyPets(int userID)
        {
            InitializeComponent();

            userid = userID;
            pet = new Pet();
            petService = new PetService();
            pets = new ObservableCollection<Pet>();



            //SelectedItem = Children[0];

            // DisplayAlert("jjj", ChildrenToString(), "lll0");

            //img.Source=ImageSource.FromResource("PetFinderApp.Images.Pomsky.jpg");



        }

        protected override async void OnAppearing()
        {

            pets = await petService.getPets(userid);
            CarouselPets.ItemsSource = pets;
            if (pets.Count > 0)
            {
                currentPetId = pets[0].id;
                selecPet = pets[0];
            }
            else
            {
                await DisplayAlert("Notification", "Not Pets Found", "ok");
            }
            CarouselPets.ItemSelected += (sender, args) =>
            {
                selecPet = args.SelectedItem as Pet;
                if (selecPet == null)
                    return;

                currentPetId = selecPet.id;

            };

        }


        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPet(userid));
        }

        private async void report_lost_clicked(object sender, EventArgs e)
        {
            var status = await petService.reportLost(currentPetId);

            if (status == "1")
            {
                await DisplayAlert("Success", "We will help you find your dog", "ok");
                pets.Remove(selecPet);

            }
            else
            {
                await DisplayAlert("Error", "Try again !", "Ok");
            }
        }
    }
}