using GymBooster.Common.Objects.DTO;


namespace GymBooster.AndroidApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public TrainingDTO Item { get; set; }
        public ItemDetailViewModel(TrainingDTO item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
