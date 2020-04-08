namespace GymBooster.Api.DTO
{
    public class CreateTrainingDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }

        protected CreateTrainingDTO()
        {
        }

        public CreateTrainingDTO(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}