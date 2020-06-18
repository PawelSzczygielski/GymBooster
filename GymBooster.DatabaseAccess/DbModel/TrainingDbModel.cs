using System.Collections.Generic;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymBooster.DatabaseAccess.DbModel
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class TrainingDbModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public List<ExerciseDbModel> Excercises { get; set; }

        protected TrainingDbModel()
        {
            Excercises = new List<ExerciseDbModel>();
        }

        public TrainingDbModel(string id, string title, List<ExerciseDbModel> excercises) : this()
        {
            var parsingOk = ObjectId.TryParse(id, out ObjectId parsedId);
            Id = parsingOk ? parsedId : ObjectId.Empty;
            Title = title;
            Excercises = excercises ?? new List<ExerciseDbModel>();
        }

        public override string ToString()
        {
            return $"{Id} | {Title} | {Excercises.Count}";
        }
    }
}