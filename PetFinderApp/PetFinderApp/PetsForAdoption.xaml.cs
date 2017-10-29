
using PetFinderApp.Models;
using PetFinderApp.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetsForAdoption : ContentPage
    {
        private PetService petService;
        private int currentPetId;
        private int userid;
        private Pet selecPet;
        private ObservableCollection<Pet> PetsForAdop;

        public PetsForAdoption(int UserId)
        {
            InitializeComponent();
            petService = new PetService();
            userid = UserId;
            PetsForAdop = new ObservableCollection<Pet>();
        }
        protected override async void OnAppearing()
        {
            PetsForAdop = await petService.getNonAdoptedPets();
            CarouselView.ItemsSource = PetsForAdop;
            if (PetsForAdop.Count > 0)
            {
                currentPetId = PetsForAdop[0].id;
                selecPet = PetsForAdop[0];
            }
            else
            {
                await DisplayAlert("Notification", "Not Pets Found", "ok");
            }
            CarouselView.ItemSelected += (sender, args) =>
            {
                selecPet = args.SelectedItem as Pet;
                if (selecPet == null)
                    return;

                currentPetId = selecPet.id;

            };

        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var status = await petService.adoptPetById(currentPetId, userid);
            if (status == "1")
            {
                await DisplayAlert("Success", "Pet Adopted", "ok");
                //await Navigation.PopModalAsync();
                PetsForAdop.Remove(selecPet);
            }
            else
            {
                await DisplayAlert("Error", "Try again !", "Ok");
            }
        }
    }
}