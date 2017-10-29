using PetFinderApp.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : MasterDetailPage
    {
        private User user;
        public UserProfile(User userFromLogIn)
        {
            InitializeComponent();
            user = userFromLogIn;
            BindingContext = user;
            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new NavigationPage(new MyPets(user.id))
            {
                BarBackgroundColor = Color.FromHex("#894183")
            };
            IsPresented = false;
            var items = new List<MenuItemNav>()
            {
                new MenuItemNav
                {
                    itemName = "My Pets",
                    itemIcon = "ic_favorite_white.png"
                },
                new MenuItemNav
                {
                    itemName = "Lost Pets",
                    itemIcon = "security.png"
                },
                new MenuItemNav
                {
                    itemName = "Pets for Adoption",
                    itemIcon = "paw.png"
                },
                new MenuItemNav
                {
                    itemName = "Logout",
                    itemIcon = "logout.png"
                }

            };
            listView.ItemsSource = items;

            //DisplayAlert("Success", "Welcome"+user.firstname, "ok");
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MyPets(user.id));
            IsPresented = false;
        }



        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var tappedItem = e.SelectedItem as MenuItemNav;
            //(e.SelectedItem as ViewCell).View.BackgroundColor = Color.Brown;
            if (tappedItem.itemName == "My Pets")
            {

                Detail = new NavigationPage(new MyPets(user.id))
                {
                    BarBackgroundColor = Color.FromHex("#894183")
                };
                IsPresented = false;
            }
            else if (tappedItem.itemName == "Lost Pets")
            {
                Detail = new NavigationPage(new LostPets())
                {
                    BarBackgroundColor = Color.FromHex("#894183")
                };
                IsPresented = false;
            }
            else if (tappedItem.itemName == "Pets for Adoption")
            {
                Detail = new NavigationPage(new PetsForAdoption(user.id))
                {
                    BarBackgroundColor = Color.FromHex("#894183")
                };
                IsPresented = false;
            }
            else if (tappedItem.itemName == "Logout")
            {
                Detail = new NavigationPage(new LoginUser())
                {
                    BarBackgroundColor = Color.FromHex("#894183")
                };
                IsPresented = false;
            }
            listView.SelectedItem = null;

        }
    }
}