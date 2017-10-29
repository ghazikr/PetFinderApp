
using PetFinderApp.Models;
using PetFinderApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselTest : ContentPage
    {
        private PetService petService;
        public CarouselTest()
        {
            InitializeComponent();
            petService = new PetService();
            CarouselZoos.ItemSelected += (sender, args) =>
            {
                var zoo = args.SelectedItem as Pet;
                if (zoo == null)
                    return;

                var tt = zoo.id;
                DisplayAlert("hjjj", tt.ToString(), "jjjj");
            };
        }

        protected override async void OnAppearing()
        {
            var pets = await petService.getPets(1);
            CarouselZoos.ItemsSource = pets;
        }
    }
}