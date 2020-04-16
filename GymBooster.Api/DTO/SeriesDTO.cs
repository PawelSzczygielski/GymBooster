namespace GymBooster.Api.DTO
{
    public class SeriesDTO
    {
        public string Comment { get; set; }
        public int NumberOfReps { get; set; }

        protected SeriesDTO()
        {
        }

        public SeriesDTO(int numberOfReps, string comment = "")
        {
            NumberOfReps = numberOfReps;
            Comment = comment;
        }
    }
}