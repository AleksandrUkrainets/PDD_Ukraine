using MLToolkit.Forms.SwipeCardView.Core;
using PDD_Ukraine.Models;
using PDD_Ukraine.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PDD_Ukraine.ViewModels
{
    public class FlashCardsViewModel : BaseViewModel
    {
        public FlashCardsViewModel(INavigation navigation)
        {
            Title = "Знаки ПДД Украины";
            Threshold = (uint)(App.ScreenWidth / 3);
            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
            ContinueCommand = new Command(OnContinue);
            EndCommand = new Command(OnEnd);

            //EmergencyReset();

            UnAnsweredCards = new ObservableRangeCollection<Card>(GetFilteredCards(CardState.UnAnswered));
            AllCards = new ObservableRangeCollection<Card>(GetFilteredCards(CardState.UnAnswered));
            CorrectAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.CorrectAnswered));
            IncorrectAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.IncorrectAnswered));

            CurrentCard = UnAnsweredCards.Count != 0 ? UnAnsweredCards[0] : new Card();
            _countsAllCards = UnAnsweredCards.Count + CorrectAnsweredCards.Count + IncorrectAnsweredCards.Count;
            _progress = 1.0f - ((float)UnAnsweredCards.Count / (float)_countsAllCards);
            OnCheckBalanceCards();
        }

        private void EmergencyReset()
        {
            foreach (var card in GetCards())
            {
                DataStore.SetStateCard(card, CardState.UnAnswered);
            }
        }


        public ICommand SwipedCommand { get; }
        public ICommand ContinueCommand { get; }
        public ICommand EndCommand { get; }

        private Card _currentCard;
        private Card _nextCard;

        private int _countsAllCards;
        private float _progress = 0f;
        private uint _threshold;
        private bool _isVisibleContinueButton = false;
        private bool _isVisibleEndButton = false;
        private string _backgroundImage = "background.png";

        public ObservableRangeCollection<Card> UnAnsweredCards { get; private set; }
        public ObservableCollection<Card> CorrectAnsweredCards { get; private set; }
        public ObservableCollection<Card> IncorrectAnsweredCards { get; private set; }
        public ObservableRangeCollection<Card> AllCards { get; private set; }

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

        public uint Threshold
        {
            get => _threshold;
            set
            {
                SetProperty(ref _threshold, value);
            }
        }

        public bool IsVisibleContinueButton
        {
            get => _isVisibleContinueButton;
            set
            {
                SetProperty(ref _isVisibleContinueButton, value);
            }
        }

        public bool IsVisibleEndButton

        {
            get => _isVisibleEndButton;
            set
            {
                SetProperty(ref _isVisibleEndButton, value);
            }
        }

        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                SetProperty(ref _backgroundImage, value);
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


        private void OnSwipedCommand(SwipedCardEventArgs eventArgs)
        {
            var direction = eventArgs.Direction;
            switch (direction)
            {
                case SwipeCardDirection.Right:
                    AddCardToTrueAnswer();
                    OnCheckBalanceCards();
                    return;

                case SwipeCardDirection.Left:
                    AddCardToFalseAnswer();
                    OnCheckBalanceCards();
                    return;

                case SwipeCardDirection.Down:
                    ResetState();
                    OnCheckBalanceCards();
                    return;
            }
        }

        private void OnCheckBalanceCards()
        {
            if(UnAnsweredCards.Count == 0)
            {
                IsVisibleEndButton = true;
                IsVisibleContinueButton = true;
                BackgroundImage = "back_empty.png";
            }
            else
            {
                IsVisibleEndButton = false;
                IsVisibleContinueButton = false;
                BackgroundImage = "background.png";
            }
        }

        private void OnContinue()
        {
            if (IncorrectAnsweredCards.Count != 0)
            {
                _countsAllCards = AllCards.Count();
                AllCards.Clear();

                foreach (var card in UnAnsweredCards)//
                {
                    DataStore.SetStateCard(card, CardState.Ended);//
                }
                UnAnsweredCards.Clear();//

                DataStore.SetRandomOrder(IncorrectAnsweredCards);
                var sortedIncorrectAnsweredCards = new ObservableCollection<Card>(IncorrectAnsweredCards.OrderBy(card => card.Order));

                UnAnsweredCards.AddRange(sortedIncorrectAnsweredCards);
                AllCards.AddRange(sortedIncorrectAnsweredCards);

                foreach (var card in IncorrectAnsweredCards)
                {
                    DataStore.SetStateCard(card, CardState.UnAnswered);
                }

                foreach (var card in CorrectAnsweredCards)
                {
                    DataStore.SetStateCard(card, CardState.Ended);
                }

                CorrectAnsweredCards.Clear();
                IncorrectAnsweredCards.Clear();
                ResetProgress();
            }
            else
            {
                OnRestart();
            }

            BackgroundImage = "background.png";
            IsVisibleEndButton = false;
            IsVisibleContinueButton = false;
        }

        private void OnEnd()
        {
        }

        private void OnRestart()
        {           
            var allCards = new ObservableCollection<Card>(GetCards());
            foreach (var card in allCards)//
            {
                DataStore.SetStateCard(card, CardState.UnAnswered);//
            }
            DataStore.SetRandomOrder(allCards);

            CorrectAnsweredCards.Clear();
            IncorrectAnsweredCards.Clear();
            UnAnsweredCards.Clear();
            UnAnsweredCards.AddRange(allCards);
            AllCards.AddRange(allCards);
            ResetProgress();
        }

        private void ResetState()
        {
            DataStore.ResetState(UnAnsweredCards, CorrectAnsweredCards, IncorrectAnsweredCards);
            CorrectAnsweredCards.Clear();
            IncorrectAnsweredCards.Clear();
            DataStore.SetRandomOrder(UnAnsweredCards);
            var sortedUnAnsweredCards = new ObservableCollection<Card>(UnAnsweredCards.OrderBy(card => card.Order));
            UnAnsweredCards.Clear();

            //foreach (Card sortedUnAnsweredCard in sortedUnAnsweredCards)
            //{
            //    UnAnsweredCards.Add(sortedUnAnsweredCard);
            //}
            UnAnsweredCards.AddRange(sortedUnAnsweredCards);

            ResetProgress();
        }

        private void ResetProgress()
        {
            CurrentCard = UnAnsweredCards.Count != 0 ? UnAnsweredCards[0] : new Card();
            _countsAllCards = UnAnsweredCards.Count + CorrectAnsweredCards.Count + IncorrectAnsweredCards.Count;
            Progress = 1.0f - ((float)UnAnsweredCards.Count / (float)_countsAllCards);
        }

        private void AddCardToTrueAnswer()
        {
            if (_countsAllCards - CorrectAnsweredCards.Count - IncorrectAnsweredCards.Count > 0)
            {
                DataStore.SetStateCard(_currentCard, CardState.CorrectAnswered);
                CorrectAnsweredCards.Add(_currentCard);
                GetNextCard();
                DeleteCurrentCard();
            }
        }

        private void AddCardToFalseAnswer()
        {
            if (_countsAllCards - CorrectAnsweredCards.Count - IncorrectAnsweredCards.Count > 0)
            {
                DataStore.SetStateCard(_currentCard, CardState.IncorrectAnswered);
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

        private IEnumerable<Card> GetFilteredCards(CardState cardState)
        {
            return DataStore.GetFilteredCards(cardState);
        }

        private IEnumerable<Card> GetCards()
        {
            return DataStore.GetCards();
        }
    }
}