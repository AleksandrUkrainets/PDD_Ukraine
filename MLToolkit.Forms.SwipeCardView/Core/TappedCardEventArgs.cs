using System;
using System.Collections.Generic;
using System.Text;

namespace MLToolkit.Forms.SwipeCardView.Core
{
    public class TappedCardEventArgs : System.EventArgs
    {

        public TappedCardEventArgs(object item, object parameter)
        {
            Item = item;
            Parameter = parameter;
        }

        /// <summary>Gets the item parameter.</summary>
        public object Item { get; private set; }

        /// <summary>Gets the command parameter.</summary>
        public object Parameter { get; private set; }
    }
}
