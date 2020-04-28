using System;
using System.Collections.Generic;
using System.Linq;

namespace GymBooster.Api.DTO
{
    public class ExcerciseDTO : IEquatable<ExcerciseDTO>
    {
        public string Name { get; set; }
        public List<SeriesDTO> Series { get; set; }

        protected ExcerciseDTO()
        {
        }

        public ExcerciseDTO(string name)
        {
            Name = name;
            Series = new List<SeriesDTO>();
        }

        public bool Equals(ExcerciseDTO other)
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
            return Equals((ExcerciseDTO) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Series);
        }
    }
}