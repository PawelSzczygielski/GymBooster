//using System;
//using System.Collections.Generic;
//using System.Diagnostics;

//namespace GymBooster.AndroidApp.Models
//{
//    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
//    public class TrainingDbModel
//    {
//        public Guid Id { get; set; }
//        public string Title { get; set; }
//        public List<ExerciseView> Excercises { get; set; }

//        protected TrainingDbModel()
//        {
//            Excercises = new List<ExerciseDbModel>();
//        }

//        public TrainingDbModel(string id, string title, List<ExerciseDbModel> excercises) : this()
//        {
//            var parsingOk = ObjectId.TryParse(id, out ObjectId parsedId);
//            Id = parsingOk ? parsedId : ObjectId.Empty;
//            Title = title;
//            Excercises = excercises ?? new List<ExerciseDbModel>();
//        }

//        public override string ToString()
//        {
//            return $"{Id} | {Title} | {Excercises.Count}";
//        }
//    }

//    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
//    public class ExerciseDbModel
//    {
//        public string Name { get; set; }
//        public List<SeriesDbModel> Series { get; set; }

//        protected ExerciseDbModel()
//        {
//            Series = new List<SeriesDbModel>();
//        }

//        public ExerciseDbModel(string name, List<SeriesDbModel> series) : this()
//        {
//            Name = name;
//            Series = series ?? new List<SeriesDbModel>();
//        }

//        public override string ToString()
//        {
//            return $"{Name} | {Series.Count}";
//        }
//    }
//    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
//    public class SeriesDbModel
//    {
//        public decimal RepsNo { get; set; }
//        public decimal Weight { get; set; }
//        public string Comment { get; set; }

//        protected SeriesDbModel()
//        {
//        }

//        public SeriesDbModel(decimal repsNo, decimal weight, string comment)
//        {
//            RepsNo = repsNo;
//            Weight = weight;
//            Comment = comment;
//        }

//        public override string ToString()
//        {
//            return $"{RepsNo} | {Weight} | '{Comment}'";
//        }
//    }
//}