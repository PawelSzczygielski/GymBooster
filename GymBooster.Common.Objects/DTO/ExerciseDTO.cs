using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GymBooster.Common.Objects.DTO
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class ExerciseDTO : IEquatable<ExerciseDTO>
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

        public bool Equals(ExerciseDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Series.SequenceEqual(other.Series);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExerciseDTO) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Series != null ? Series.GetHashCode() : 0);
            }
        }
      
        public override string ToString()
        {
            return $"{Name} | {Series.Count}";
        }
    }
}