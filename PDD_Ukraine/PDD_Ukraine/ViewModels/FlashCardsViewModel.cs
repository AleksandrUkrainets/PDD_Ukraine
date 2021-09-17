using MLToolkit.Forms.SwipeCardView.Core;
using PDD_Ukraine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PDD_Ukraine.ViewModels
{
    public class FlashCardsViewModel : BaseViewModel
    {
        public FlashCardsViewModel()
        {
            Title = "Знаки ПДД Украины";
            Threshold = (uint)(App.ScreenWidth / 3);
            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);

            UnAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.UnAnswered));
            CorrectAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.CorrectAnswered));
            IncorrectAnsweredCards = new ObservableCollection<Card>(GetFilteredCards(CardState.IncorrectAnswered));
            AllCards = new ObservableCollection<Card>(GetCards());
            
            CurrentCard = UnAnsweredCards.Count != 0 ? UnAnsweredCards[0] : new Card();
            _countsAllCards = UnAnsweredCards.Count + CorrectAnsweredCards.Count + IncorrectAnsweredCards.Count;
            _progress = 1.0f - ((float)UnAnsweredCards.Count / (float)_countsAllCards);
        }

        private void OnSwipedCommand(SwipedCardEventArgs eventArgs)
        {
            var direction = eventArgs.Direction;
            switch (direction)
            {
                case SwipeCardDirection.Right:
                    AddCardToTrueAnswer();
                    return;
                case SwipeCardDirection.Left:
                    AddCardToFalseAnswer();
                    return;
                case SwipeCardDirection.Down:
                    ResetState();
                    return;
            }
        }

        public ICommand SwipedCommand { get; }

        private Card _currentCard;
        private Card _nextCard;

        private int _countsAllCards;
        private float _progress = 0f;
        private uint _threshold;

        public ObservableCollection<Card> UnAnsweredCards { get; private set; }
        public ObservableCollection<Card> CorrectAnsweredCards { get; private set; }
        public ObservableCollection<Card> IncorrectAnsweredCards { get; private set; }
        public ObservableCollection<Card> AllCards { get; private set; }

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

        public void GetNextCard()
        {
            if (UnAnsweredCards.Count > 1)
            {
                _nextCard = UnAnsweredCards[1];
            }
            else _nextCard = new Card();
        }

        private void ResetState()
        {
            DataStore.ResetState(UnAnsweredCards, CorrectAnsweredCards, IncorrectAnsweredCards);
            CorrectAnsweredCards.Clear();
            IncorrectAnsweredCards.Clear();
            DataStore.SetRandomOrder(UnAnsweredCards);
            var sortedUnAnsweredCards = new ObservableCollection < Card > (UnAnsweredCards.OrderBy(card => card.Order));
            UnAnsweredCards.Clear();

            foreach(Card sortedUnAnsweredCard in sortedUnAnsweredCards)
            {
                UnAnsweredCards.Add(sortedUnAnsweredCard);
            }

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