using System.Collections.Generic;

namespace GymBooster.Api.DTO
{
    public class ExcerciseDTO
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public List<SeriesDTO> Series { get; set; }

        protected ExcerciseDTO()
        {
        }

        public ExcerciseDTO(string name, string comment = "")
        {
            Name = name;
            Comment = comment;
            Series = new List<SeriesDTO>();
        }
    }
}