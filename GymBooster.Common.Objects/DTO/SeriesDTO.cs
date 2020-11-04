using System;
using System.Diagnostics;

namespace GymBooster.Common.Objects.DTO
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class SeriesDTO
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

        public override string ToString()
        {
            return $"{NumberOfReps} | {Weight} | '{Comment}'";
        }
    }
}