using PetFinderApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LostPets : ContentPage
    {
        private PetService petService;
        public LostPets()
        {
            InitializeComponent();
            petService = new PetService();

        }

        protected override async void OnAppearing()
        {
            var lostPets = await petService.getAllLostPets();
            ListView.ItemsSource = lostPets;
        }
    }
}