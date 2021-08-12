using PDD_Ukraine.Models;
using Realms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PDD_Ukraine.ViewModels
{
    public class FlashCardsViewModel : BaseViewModel
    {
        private Realm realm = Realm.GetInstance();
        private List<Card> items;
        //private Map _map;

        //public override IDisposable OpenDB()
        //{
        //    return this.realm = RealmType.GetInstance(RealmConfiguration.DefaultConfiguration.ConfigWithPath(this.Path));
        //}

        public FlashCardsViewModel()
        {
            var instance = DataStore.GetInstance();
            //items = new List<Card>()
            //{
            //    new Card { Name = "First item", Description = "This is an item description.", State = (int)CardState.CorrectAnswered },
            //    new Card { Name = "Second item", Description = "This is an item description.", State = (int)CardState.IncorrectAnswered },
            //    new Card { Name = "Third item", Description = "This is an item description.", State = (int)CardState.UnAnswered },
            //    new Card { Name = "Fourth item", Description = "This is an item description.", State = (int)CardState.UnAnswered },
            //    new Card { Name = "Fifth item", Description = "This is an item description.", State = (int)CardState.UnAnswered },
            //    new Card { Name = "Sixth item", Description = "This is an item description.", State = (int)CardState.UnAnswered }
            //};

            //FillDataBase();
            Title = "Rotate Animation with Anchors";

            AddCardToTrueAnswerCommand = new Command(AddCardToTrueAnswer);
            AddCardToFalseAnswerCommand = new Command(AddCardToFalseAnswer);

            UnAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.UnAnswered));
            CorrectAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.CorrectAnswered));
            IncorrectAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.IncorrectAnswered));
            CurrentCard = UnAnsweredCards.Count != 0 ? UnAnsweredCards[0] : new Card();
            _countsAllCards = UnAnsweredCards.Count + CorrectAnsweredCards.Count + IncorrectAnsweredCards.Count;
            _progress = 1.0f - ((float)UnAnsweredCards.Count / (float)_countsAllCards);
        }

        public ICommand AddCardToTrueAnswerCommand { get; }
        public ICommand AddCardToFalseAnswerCommand { get; }
        public Command LoadCardsCommand { get; }

        private Card _currentCard;
        private Card _nextCard;

        private int _countsAllCards;
        private float _progress = 0f;

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

        public float Progress
        {
            get => _progress;
            set
            {
                SetProperty(ref _progress, value);
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
                realm.Write(() => _currentCard.State = (int)CardState.CorrectAnswered);
                CorrectAnsweredCards.Add(_currentCard);
                GetNextCard();
                DeleteCurrentCard();
            }
        }

        private void AddCardToFalseAnswer()
        {
            if (_countsAllCards - CorrectAnsweredCards.Count - IncorrectAnsweredCards.Count > 0)
            {
                realm.Write(() => _currentCard.State = (int)CardState.IncorrectAnswered);
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
            Progress = 1.0f - ((float)UnAnsweredCards.Count / (float)_countsAllCards);
        }

        //private async Task<IEnumerable<Card>> GetCards()
        //{
        //    IEnumerable<Card> cards = await DataStore.GetCardsAsync(true);

        //    return cards;
        //}

        private List<Card> GetCards()
        {
            var cards = realm.All<Card>();
            var listCards = new List<Card>(cards);
            return listCards;
        }

        private List<Card> GetFilteredCards(CardState cardState)
        {
            List<Card> result = new List<Card>();
            foreach (Card card in GetCards())
            {
                if (card.State == (int)cardState)
                {
                    result.Add(card);
                }
            }

            return result;
        }

        //--------------------

        public void FillDataBase()
        {
            realm.Write(() =>
            {
                foreach (var card in items)
                {
                    realm.Add(card);
                }
            });
        }
    }
}