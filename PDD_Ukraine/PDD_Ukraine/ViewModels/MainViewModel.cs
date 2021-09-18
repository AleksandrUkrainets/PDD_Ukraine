using System;
using System.Collections.Generic;
using System.Text;

namespace PDD_Ukraine.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Title = "Знаки ПДД Украины";
        }

        private string _backgroundImage = "back_empty.png";

        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                SetProperty(ref _backgroundImage, value);
            }
        }
    }
}
