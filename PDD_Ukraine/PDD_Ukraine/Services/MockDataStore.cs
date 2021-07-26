using PDD_Ukraine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDD_Ukraine.Services
{
    public class MockDataStore : IDataStore<Card>
    {
        private readonly List<Card> items;

        public MockDataStore()
        {
            items = new List<Card>()
            {
                new Card { Id = Guid.NewGuid().ToString(), Name = "First item", Description = "This is an item description.", State = CardState.CorrectAnswered},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Second item", Description = "This is an item description.", State = CardState.IncorrectAnswered },
                new Card { Id = Guid.NewGuid().ToString(), Name = "Third item", Description = "This is an item description.", State = CardState.UnAnswered },
                new Card { Id = Guid.NewGuid().ToString(), Name = "Fourth item", Description = "This is an item description.", State = CardState.UnAnswered },
                new Card { Id = Guid.NewGuid().ToString(), Name = "Fifth item", Description = "This is an item description.", State = CardState.UnAnswered },
                new Card { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Description = "This is an item description.", State = CardState.UnAnswered }
            };
        }

        //public async Task<bool> AddItemAsync(Item item)
        //{
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> UpdateItemAsync(Item item)
        //{
        //    var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
        //    items.Remove(oldItem);
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> DeleteItemAsync(string id)
        //{
        //    var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
        //    items.Remove(oldItem);

        //    return await Task.FromResult(true);
        //}

        public async Task<Card> GetCardAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}