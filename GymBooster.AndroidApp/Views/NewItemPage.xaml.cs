using System;
using System.Collections.Generic;
using System.ComponentModel;
using GymBooster.Common.Objects.DTO;
using Xamarin.Forms;


namespace GymBooster.AndroidApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public TrainingDTO Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new TrainingDTO("ID", "Title", DateTime.UtcNow, new List<ExerciseDTO>());
            
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}