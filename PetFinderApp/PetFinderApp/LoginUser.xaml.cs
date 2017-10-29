using PetFinderApp.Models;
using PetFinderApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUser : ContentPage
    {
        private UserService userService;
        public LoginUser()
        {
            InitializeComponent();
            userService = new UserService();

            /*var stack1 = new Gradient_Stack
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                StartColor = Color.FromHex("#3a3a38"),
                EndColor = Color.FromHex("#4d4329")
            };*/
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterUser());
        }

        private async void login_handled(object sender, EventArgs e)
        {
            if (AllFieldsFilled())
            {
                string pass = password.Text;
                string mail = email.Text;
                User resUser = await userService.login(mail, pass);
                if (resUser == null)
                    await DisplayAlert("Error ", "wrong login/password", "ok");
                else
                {
                    //await DisplayAlert("Success", "Your are now registered !", "ok");
                    Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new NavigationPage(new UserProfile(resUser)));
                }
            }
            else
            {
                DisplayAlert("Error", "please finish all the fields !", "ok");
            }
        }
        private bool AllFieldsFilled()
        {
            return ((String.IsNullOrEmpty(email.Text)) || (String.IsNullOrEmpty(password.Text)))
                ? false
                : true;

        }
    }
}