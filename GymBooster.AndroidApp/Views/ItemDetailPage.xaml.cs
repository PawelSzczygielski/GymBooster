using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using GymBooster.AndroidApp.ViewModels;
using GymBooster.Common.Objects.DTO;

namespace GymBooster.AndroidApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new TrainingDTO("ID", "Title", DateTime.UtcNow,  new List<ExerciseDTO>());

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}