using PDD_Ukraine.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PDD_Ukraine.ViewModels
{
    public class FlashCardsViewModel : BaseViewModel
    {
        public FlashCardsViewModel()
        {
            Title = "Rotate Animation with Anchors";

            AddCardToTrueAnswerCommand = new Command(AddCardToTrueAnswer);
            AddCardToFalseAnswerCommand = new Command(AddCardToFalseAnswer);

            UnAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.UnAnswered));
            CorrectAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.CorrectAnswered));
            IncorrectAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.IncorrectAnswered));
            CurrentCard = UnAnsweredCards[0];
            _countsAllCards = UnAnsweredCards.Count + CorrectAnsweredCards.Count + IncorrectAnsweredCards.Count;
        }

        public ICommand AddCardToTrueAnswerCommand { get; }
        public ICommand AddCardToFalseAnswerCommand { get; }
        public Command LoadCardsCommand { get; }

        private Card _currentCard;
        private Card _nextCard;

        private int _countsAllCards;

        public ObservableCollection<Card> UnAnsweredCards { get; private set; }
        public ObservableCollection<Card> CorrectAnsweredCards { get; private set; }
        public ObservableCollection<Card> IncorrectAnsweredCards { get; private set; }

        public Card CurrentCard
        {
            get => _currentCard;
            set
            {
                SetProperty(ref _currentCard, value);
            }
        }

        public void GetNextCard()
        {
            if (UnAnsweredCards.Count > 1)
            {
                _nextCard = UnAnsweredCards[1];
            }
            else _nextCard = new Card();
        }

        private void AddCardToTrueAnswer()
        {
            if (_countsAllCards - CorrectAnsweredCards.Count - IncorrectAnsweredCards.Count > 0)
            {
                CorrectAnsweredCards.Add(_currentCard);
                GetNextCard();
                DeleteCurrentCard();
            }
        }

        private void AddCardToFalseAnswer()
        {
            if (_countsAllCards - CorrectAnsweredCards.Count - IncorrectAnsweredCards.Count > 0)
            {
                IncorrectAnsweredCards.Add(_currentCard);
                GetNextCard();
                DeleteCurrentCard();
            }
        }

        private void DeleteCurrentCard()
        {
            CurrentCard = _nextCard;
            if (UnAnsweredCards.Count > 0)
            {
                UnAnsweredCards.RemoveAt(0);
            }
        }

        private async Task<IEnumerable<Card>> GetCards()
        {
            IEnumerable<Card> cards = await DataStore.GetCardsAsync(true);

            return cards;
        }

        private List<Card> GetFilteredCards(CardState cardState)
        {
            List<Card> result = new List<Card>();
            foreach (Card card in GetCards().Result)
            {
                if (card.State == cardState)
                {
                    result.Add(card);
                }
            }

            return result;
        }

    }
}