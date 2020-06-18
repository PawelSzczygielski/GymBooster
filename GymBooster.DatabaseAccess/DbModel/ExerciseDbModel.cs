using System.Collections.Generic;
using System.Diagnostics;

namespace GymBooster.DatabaseAccess.DbModel
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class ExerciseDbModel
    {
        public string Name { get; set; }
        public List<SeriesDbModel> Series { get; set; }

        protected ExerciseDbModel()
        {
            Series = new List<SeriesDbModel>();
        }

        public ExerciseDbModel(string name, List<SeriesDbModel> series) : this()
        {
            Name = name;
            Series = series ?? new List<SeriesDbModel>();
        }

        public override string ToString()
        {
            return $"{Name} | {Series.Count}";
        }
    }
}