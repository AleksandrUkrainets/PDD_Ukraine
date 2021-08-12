using PDD_Ukraine.Services;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace PDD_Ukraine
{
    public partial class App : Application
    {
        private const string databaseName = "default.realm";
        private string dbPath;
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
            if (!File.Exists(dbPath))
            {
                // получаем текущую сборку
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                // берем из нее ресурс базы данных и создаем из него поток
                using (Stream stream = assembly.GetManifestResourceStream($"PDD_Ukraine.Assets.{databaseName}"))
                {
                    using (FileStream fileStream = new FileStream(dbPath, FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(fileStream);  // копируем файл базы данных в нужное нам место
                        fileStream.Flush();
                    }
                }
            }
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