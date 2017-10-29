
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetFinderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestDesign : ContentPage
    {
        public TestDesign()
        {
            InitializeComponent();
            Gender.SelectedIndex = 0;
        }
    }
}