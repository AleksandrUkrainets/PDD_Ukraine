using PDD_Ukraine.Models;
using Realms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PDD_Ukraine.Services
{
    public interface IDataStore<T>
    {
        Realm GetInstance();

        IEnumerable<Card> GetFilteredCards(CardState cardState);

        void SetStateCard(Card currentCard, CardState cardState);

        void ResetState(ObservableCollection<Card> unAnsweredCards, ObservableCollection<Card> correctAnsweredCards, ObservableCollection<Card> incorrectAnsweredCards);

        void SetRandomOrder(ObservableCollection<Card> unAnsweredCards);
    }
}