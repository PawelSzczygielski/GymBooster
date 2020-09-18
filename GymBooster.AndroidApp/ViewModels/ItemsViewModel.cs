using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using GymBooster.AndroidApp.Services;
using GymBooster.AndroidApp.Views;
using GymBooster.Common.Objects.DTO;
using Xamarin.Forms;

namespace GymBooster.AndroidApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<TrainingDTO> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            
            Title = "Browse";
            Items = new ObservableCollection<TrainingDTO>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, TrainingDTO>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as TrainingDTO;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
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
    }
}