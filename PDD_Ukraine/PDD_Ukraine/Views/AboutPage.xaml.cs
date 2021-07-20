using PDD_Ukraine.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDD_Ukraine.Views
{
    public partial class AboutPage : ContentPage
    {
        private bool _isFrontSideCard;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }

        //void OnSizeChanged(object sender, EventArgs e)
        //{
        //    center = new Point(absoluteLayout.Width / 2, absoluteLayout.Height / 2);
        //    radius = Math.Min(absoluteLayout.Width, absoluteLayout.Height) / 2;
        //    AbsoluteLayout.SetLayoutBounds(image,
        //        new Rectangle(center.X - image.Width / 2, center.Y - radius, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
        //}

        private void rotateLayout_Clicked(object sender, EventArgs e)
        {
            RotateCard();
        }

        private async void RotateCard()
        {
            ImageButton imageButton = absoluteLayout3.FindByName("imageButton") as ImageButton;
            Button but2 = absoluteLayout3.FindByName("rotateLayout2") as Button;
            absoluteLayout3.TranslateTo(100, 0, 400);
            await absoluteLayout3.RotateYTo(-90, 200);
            absoluteLayout3.RotationY = -270;

            if (!_isFrontSideCard)
            {
                imageButton.IsVisible = false;
                but2.IsVisible = true;
                _isFrontSideCard = true;
            }
            else
            {
                imageButton.IsVisible = true;
                but2.IsVisible = false;
                _isFrontSideCard = false;
            }

            absoluteLayout3.RotateYTo(-360, 200);
            await absoluteLayout3.TranslateTo(0, 0, 222);
            absoluteLayout3.RotationY = 0;
        }
    }
}
