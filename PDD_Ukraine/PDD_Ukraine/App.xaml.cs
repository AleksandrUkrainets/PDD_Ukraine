using PDD_Ukraine.Services;
using Xamarin.Forms;

namespace PDD_Ukraine
{
    public partial class App : Application
    {
        public static double ScreenHeight;
        public static double ScreenWidth;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<DataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}