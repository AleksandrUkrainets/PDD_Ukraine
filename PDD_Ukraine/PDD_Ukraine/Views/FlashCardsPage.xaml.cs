using PDD_Ukraine.ViewModels;
using System;
using Xamarin.Forms;

namespace PDD_Ukraine.Views
{
    public partial class FlashCardsPage : ContentPage
    {
        private bool _isFrontSideCard;

        public FlashCardsPage()
        {
            InitializeComponent();
            BindingContext = new FlashCardsViewModel();
        }

        private void rotateLayout_Clicked(object sender, EventArgs e)
        {
            RotateCard();
        }

        private async void RotateCard()
        {
            absoluteLayout3.TranslateTo(100, 0, 200);
            await absoluteLayout3.RotateYTo(-90, 100);
            absoluteLayout3.RotationY = -270;

            if (!_isFrontSideCard)
            {
                imageButton.IsVisible = false;
                textButton.IsVisible = true;
                _isFrontSideCard = true;
            }
            else
            {
                imageButton.IsVisible = true;
                textButton.IsVisible = false;
                _isFrontSideCard = false;
            }

            absoluteLayout3.RotateYTo(-360, 100);
            await absoluteLayout3.TranslateTo(0, 0, 100);
            absoluteLayout3.RotationY = 0;
        }

        private async void SwipeItem_Invoked_Right(object sender, EventArgs e)
        {
            absoluteLayout3.IsVisible = false;
            MoveToStartPosition();
        }

        private async void SwipeItem_Invoked_Left(object sender, EventArgs e)
        {
            absoluteLayout3.IsVisible = false;
            MoveToStartPosition();
        }

        private async void MoveToStartPosition()
        {
            absoluteLayout3.RotateYTo(-360, 100);
            await absoluteLayout3.TranslateTo(0, 0, 100);
            absoluteLayout3.RotationY = 0;
            imageButton.IsVisible = true;
            textButton.IsVisible = false;
            _isFrontSideCard = false;
            absoluteLayout3.IsVisible = true;
        }
    }
}