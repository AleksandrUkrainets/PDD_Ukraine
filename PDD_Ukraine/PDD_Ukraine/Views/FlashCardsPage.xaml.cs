using MLToolkit.Forms.SwipeCardView.Core;
using PDD_Ukraine.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PDD_Ukraine.Views
{
    public partial class FlashCardsPage : ContentPage
    {
        public FlashCardsPage()
        {
            InitializeComponent();
            BindingContext = new FlashCardsViewModel();
            SwipeCardView.Dragging += OnDragging;
            SwipeCardView.Tapped += OnTapped;
        }

        private async void OnTapped(object sender, TappedCardEventArgs e)
        {
            RedFrame.BackgroundColor = Color.FromHex("#8095BF");
            YellowFrame.BackgroundColor = Color.Yellow;
            GreenFrame.BackgroundColor = Color.FromHex("#8095BF");
            YellowLabel.FontAttributes = FontAttributes.Bold;
            await Task.Delay(1000);
            YellowFrame.BackgroundColor = Color.FromHex("#8095BF");
            YellowLabel.FontAttributes = FontAttributes.None;
        }

        private async void OnDragging(object sender, DraggingCardEventArgs e)
        {
            var backgroundColor = Color.FromHex("#8095BF");

            switch (e.Position)
            {
                case DraggingCardPosition.Start:
                    RedFrame.BackgroundColor = backgroundColor;
                    YellowFrame.BackgroundColor = backgroundColor;
                    GreenFrame.BackgroundColor = backgroundColor;
                    break;

                case DraggingCardPosition.UnderThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                    }
                    else if (e.Direction == SwipeCardDirection.Up)
                    {
                    }
                    break;

                case DraggingCardPosition.OverThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        RedFrame.BackgroundColor = Color.Red;
                        YellowFrame.BackgroundColor = Color.FromHex("#8095BF");
                        GreenFrame.BackgroundColor = Color.FromHex("#8095BF");
                        RedLabel.FontAttributes = FontAttributes.Bold;
                        await Task.Delay(1000);
                        RedFrame.BackgroundColor = Color.FromHex("#8095BF");
                        RedLabel.FontAttributes = FontAttributes.None;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        GreenFrame.BackgroundColor = Color.Green;
                        YellowFrame.BackgroundColor = Color.FromHex("#8095BF");
                        RedFrame.BackgroundColor = Color.FromHex("#8095BF");
                        GreenLabel.FontAttributes = FontAttributes.Bold;
                        await Task.Delay(1000);
                        GreenFrame.BackgroundColor = Color.FromHex("#8095BF");
                        GreenLabel.FontAttributes = FontAttributes.None;
                    }
                    else if (e.Direction == SwipeCardDirection.Up)
                    {
                    }
                    break;

                case DraggingCardPosition.FinishedUnderThreshold:
                    break;

                case DraggingCardPosition.FinishedOverThreshold:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}