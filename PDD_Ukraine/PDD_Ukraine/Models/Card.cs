using System;
using System.ComponentModel;

namespace PDD_Ukraine.Models
{
    public class Card //: INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LinkToImage { get; }
        public CardState State { get; set; }
        //private string id;
        //private string text;
        //private string description;
        //public string Id
        //{
        //    get { return id; }
        //    set
        //    {
        //        if (id != value)
        //        {
        //            id = value;
        //            OnPropertyChanged("id");
        //        }
        //    }
        //}
        //public string Text
        //{
        //    get { return text; }
        //    set
        //    {
        //        if (text != value)
        //        {
        //            text = value;
        //            OnPropertyChanged("text");
        //        }
        //    }
        //}
        //public string Description
        //{
        //    get { return description; }
        //    set
        //    {
        //        if (description != value)
        //        {
        //            description = value;
        //            OnPropertyChanged("description");
        //        }
        //    }
        //}


        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged(string prop = "")
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //}
    }
}