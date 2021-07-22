using PDD_Ukraine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PDD_Ukraine.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            GetNextItemCommand = new Command(GetNextItem);
            Title = "Rotate Animation with Anchors";

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
            ExecuteLoadItemsCommand();
            GetNextItem();
        }

        public ICommand GetNextItemCommand { get; }

        //------------------------
        private Item _selectedItem;

        private Item _nextItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }

        //public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public Item NextItem
        {
            get => _nextItem;
            set
            {
                SetProperty(ref _nextItem, value);
            }
        }

        //public string TextNextItem { get; private set; }
        //public string DescriptionNextItem { get; private set; }

        private int _numberItem = 0;

        public async void GetNextItem()
        {
            IEnumerable<Item> items = await DataStore.GetItemsAsync(true);
            List<Item> listItems = new List<Item>();
            foreach (var item in items)
            {
                listItems.Add(item);
            }
            NextItem = GetItem(ref _numberItem, listItems);
            //NextItem = new Item
            //{
            //    Id = GetItem(ref numberItem, listItems).Id,
            //    Text = GetItem(ref numberItem, listItems).Text,
            //    Description = GetItem(ref numberItem, listItems).Description
            //};
            //Title = GetItem(ref numberItem).Text;
            //TextNextItem = GetItem(ref numberItem).Text;

            _numberItem++;
        }

        private Item GetItem(ref int numberItem, List<Item> listItems)
        {
            if (numberItem <= listItems.Count - 1)
            {
                if (listItems.Count > 0)
                {
                    Items.Add(listItems[numberItem]);
                }

                return listItems[numberItem];
            }
            else return listItems[listItems.Count - 1];
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                _numberItem = 0;
                //Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                //foreach (var item in items)
                //{
                //    Items.Add(item);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        //private async void OnAddItem(object obj)
        //{
        //    await Shell.Current.GoToAsync(nameof(NewItemPage));
        //}

        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            // await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}