using System.Diagnostics;

namespace GymBooster.DatabaseAccess.DbModel
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class SeriesDbModel
    {
        public decimal RepsNo { get; set; }
        public decimal Weight { get; set; }
        public string Comment { get; set; }

        protected SeriesDbModel()
        {
        }

        public SeriesDbModel(decimal repsNo, decimal weight, string comment)
        {
            RepsNo = repsNo;
            Weight = weight;
            Comment = comment;
        }

        public override string ToString()
        {
            return $"{RepsNo} | {Weight} | '{Comment}'";
        }
    }
}