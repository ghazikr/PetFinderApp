
using Xamarin.Forms;

namespace PetFinderApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginUser())
            {
                BarBackgroundColor = Color.FromHex("#7A3B75"),

            };

            //MainPage = new CarouselTest();


        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
