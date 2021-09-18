using PDD_Ukraine.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PDD_Ukraine.Services
{
    public class DataStore : IDataStore<Card>
    {
        private const string _databaseName = "default.realm";
        private string _dbPath;
        private Realm _realmInstance;
        private readonly Random _random = new Random();

        public DataStore()
        {
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _databaseName);
            if (!File.Exists(_dbPath))
            {
                //получаем текущую сборку
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                //берем из нее ресурс базы данных и создаем из него поток
                using (Stream stream = assembly.GetManifestResourceStream($"PDD_Ukraine.Assets.{_databaseName}"))
                {
                    using (FileStream fileStream = new FileStream(_dbPath, FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(fileStream);  // копируем файл базы данных в нужное нам место
                        fileStream.Flush();
                    }
                }
            }
            _realmInstance = GetInstance();
        }

        public Realm GetInstance()
        {
            RealmConfiguration config = new RealmConfiguration(_dbPath);
            return Realm.GetInstance(config);
        }

        public IEnumerable<Card> GetFilteredCards(CardState cardState)
        {
            return _realmInstance.All<Card>().Where(x => x.State == (int)cardState);
        }

        public IEnumerable<Card> GetCards()
        {
            return _realmInstance.All<Card>();
        }

        public void SetStateCard(Card card, CardState cardState)
        {
            _realmInstance.Write(() => card.State = (int)cardState);
        }

        public void ResetState(ObservableCollection<Card> unAnsweredCards, ObservableCollection<Card> correctAnsweredCards, ObservableCollection<Card> incorrectAnsweredCards)
        {
            _realmInstance.Write(() =>
            {
                foreach (var correctAnsweredCard in correctAnsweredCards)
                {
                    correctAnsweredCard.State = (int)CardState.UnAnswered;
                    unAnsweredCards.Add(correctAnsweredCard);
                }

                foreach (var incorrectAnsweredCard in incorrectAnsweredCards)
                {
                    incorrectAnsweredCard.State = (int)CardState.UnAnswered;
                    unAnsweredCards.Add(incorrectAnsweredCard);
                }
            });
        }

        public void SetRandomOrder(ObservableCollection<Card> unAnsweredCards)
        {
            _realmInstance.Write(() =>
            {
                foreach (Card unAnsweredCard in unAnsweredCards)
                {
                    unAnsweredCard.Order = _random.Next(unAnsweredCards.Count);
                }
            });
        }
    }
}