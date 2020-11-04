using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GymBooster.Common.Objects.DTO
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class ExerciseDTO
    {
        public string Name { get; set; }
        public List<SeriesDTO> Series { get; set; }

        protected ExerciseDTO()
        {
        }

        public ExerciseDTO(string name, List<SeriesDTO> series)
        {
            Name = name;
            Series = series;
        }
    }
}