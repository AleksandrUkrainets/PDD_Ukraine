using Realms;

namespace PDD_Ukraine.Services
{
    public interface IDataStore<T>
    {
        //Task<bool> AddItemAsync(T item);
        //Task<bool> UpdateItemAsync(T item);
        //Task<bool> DeleteItemAsync(string id);
        //Task<T> GetCardAsync(string id);

        //Task<IEnumerable<T>> GetCardsAsync(bool forceRefresh = false);
        Realm GetInstance();
    }
}