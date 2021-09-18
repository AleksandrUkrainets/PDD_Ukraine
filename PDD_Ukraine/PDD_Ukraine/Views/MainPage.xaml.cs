using PDD_Ukraine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDD_Ukraine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        async void NavigateToCardsPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//FlashCardsPage");
        }
    }
}