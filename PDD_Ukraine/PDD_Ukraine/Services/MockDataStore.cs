using PDD_Ukraine.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PDD_Ukraine.Services
{
    public class MockDataStore : IDataStore<Card>
    {
        private readonly List<Card> items;

        //private Realm realm = Realm.GetInstance();

        //private const string databaseName = "default.realm";
        //private string dbPath;

        public Realm GetInstance()
        {
            //dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
            //if (!File.Exists(dbPath))
            //{
            //    // получаем текущую сборку
            //    var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            //    // берем из нее ресурс базы данных и создаем из него поток
            //    using (Stream stream = assembly.GetManifestResourceStream($"PDD_Ukraine.Assets.{databaseName}"))
            //    {
            //        using (FileStream fileStream = new FileStream(dbPath, FileMode.OpenOrCreate))
            //        {
            //            stream.CopyTo(fileStream);  // копируем файл базы данных в нужное нам место
            //            fileStream.Flush();
            //        }
            //    }
            //}
            //RealmConfiguration config = new RealmConfiguration(dbPath);
            //return Realm.GetInstance(config);
            return Realm.GetInstance();
        }

        //void djkfhg()
        //{
        //    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
        //    // если база данных не существует (еще не скопирована)
        //    if (!File.Exists(dbPath))
        //    {
        //        // получаем текущую сборку
        //        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
        //        // берем из нее ресурс базы данных и создаем из него поток
        //        using (Stream stream = assembly.GetManifestResourceStream($"HelloApp.{DATABASE_NAME}"))
        //        {
        //            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
        //            {
        //                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
        //                fs.Flush();
        //            }
        //        }
        //    }
        //    database = new FriendAsyncRepository(dbPath);
        //}
        public MockDataStore()
        {


            //items = new List<Card>()
            //{
            //    new Card { Name = "First item", Description = "This is an item description.", State = CardState.CorrectAnswered.ToString()},
            //    new Card { Name = "Second item", Description = "This is an item description.", State = CardState.IncorrectAnswered.ToString() },
            //    new Card { Name = "Third item", Description = "This is an item description.", State = CardState.UnAnswered.ToString() },
            //    new Card { Name = "Fourth item", Description = "This is an item description.", State = CardState.UnAnswered.ToString() },
            //    new Card { Name = "Fifth item", Description = "This is an item description.", State = CardState.UnAnswered.ToString() },
            //    new Card { Name = "Sixth item", Description = "This is an item description.", State = CardState.UnAnswered.ToString() }
            //};
            //FillDataBase();
        }

        //public async Task<Card> GetCardAsync(string id)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        //}

        //public async Task<IEnumerable<Card>> GetCardsAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(items);
        //}
        //public void FillDataBase()
        //{
        //    realm.Write(() =>
        //    {
        //        foreach(var card in items)
        //        {
        //            realm.Add(card);
        //        }
        //    });
        //}
        //public async Task<IEnumerable<Card>> GetCardsAsync(bool forceRefresh = false)
        //{
        //    var allCards = realm.All<Card>();
        //    return await Task.FromResult(allCards);
        //}
    }
}