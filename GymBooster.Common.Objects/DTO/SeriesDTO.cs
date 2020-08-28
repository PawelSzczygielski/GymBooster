using System;
using System.Diagnostics;

namespace GymBooster.Common.Objects.DTO
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class SeriesDTO : IEquatable<SeriesDTO>
    {
        public string Comment { get; set; }
        public decimal NumberOfReps { get; set; }
        public decimal Weight { get; set; }

        protected SeriesDTO()
        {
        }

        public SeriesDTO(decimal numberOfReps, decimal weight, string comment = "")
        {
            NumberOfReps = numberOfReps;
            Weight = weight;
            Comment = comment;
        }

        public bool Equals(SeriesDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return NumberOfReps == other.NumberOfReps && Weight == other.Weight;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SeriesDTO) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (NumberOfReps.GetHashCode() * 397) ^ Weight.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{NumberOfReps} | {Weight} | '{Comment}'";
        }
    }
}