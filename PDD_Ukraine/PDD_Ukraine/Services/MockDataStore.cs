using PDD_Ukraine.Models;
using Realms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDD_Ukraine.Services
{
    public class MockDataStore : IDataStore<Card>
    {
        private readonly List<Card> items;

        //private Realm realm = Realm.GetInstance();
        public MockDataStore()
        {
            items = new List<Card>()
            {
                new Card { Name = "First item", Description = "This is an item description.", State = CardState.CorrectAnswered.ToString()},
                new Card { Name = "Second item", Description = "This is an item description.", State = CardState.IncorrectAnswered.ToString() },
                new Card { Name = "Third item", Description = "This is an item description.", State = CardState.UnAnswered.ToString() },
                new Card { Name = "Fourth item", Description = "This is an item description.", State = CardState.UnAnswered.ToString() },
                new Card { Name = "Fifth item", Description = "This is an item description.", State = CardState.UnAnswered.ToString() },
                new Card { Name = "Sixth item", Description = "This is an item description.", State = CardState.UnAnswered.ToString() }
            };
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