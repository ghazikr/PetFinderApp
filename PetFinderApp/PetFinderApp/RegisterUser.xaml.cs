using PetFinderApp.Models;
using PetFinderApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUser : ContentPage
    {
        private UserService userService;

        public RegisterUser()
        {
            InitializeComponent();
            userService = new UserService();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (AllFieldsFilled())
            {
                User user = new User
                {
                    firstname = firstname.Text,
                    lastname = lastname.Text,
                    email = email.Text,
                    password = password.Text
                };
                var status = await userService.InsertUser(user);


                if (status == "1")
                {
                    await DisplayAlert("Success", "Your are now registered !", "ok");
                    await Navigation.PushAsync(new LoginUser());
                }
                else
                {
                    await DisplayAlert("Error", "Try again !", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "please finish all the fields !", "ok");
            }
        }

        private bool AllFieldsFilled()
        {
            return ((String.IsNullOrEmpty(firstname.Text)) || (String.IsNullOrEmpty(lastname.Text)) ||
                    (String.IsNullOrEmpty(email.Text)) || (String.IsNullOrEmpty(password.Text)))
                ? false
                : true;

        }
    }
}