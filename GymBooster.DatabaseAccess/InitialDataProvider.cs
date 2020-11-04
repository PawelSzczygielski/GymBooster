using System;
using System.Collections.Generic;
using GymBooster.DatabaseAccess.DbModel;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GymBooster.DatabaseAccess
{
    public class InitialDataProvider
    {
        public static void AddData(ITrainingContext trainingContext)
        {
            trainingContext.Trainings.DeleteMany(FilterDefinition<TrainingDbModel>.Empty);
            trainingContext.Trainings.InsertMany(new List<TrainingDbModel>
            {
                new TrainingDbModel(ObjectId.GenerateNewId(1).ToString(), "Legs", new DateTime(2020,10,15, 19, 2, 23),  new List<ExerciseDbModel>
                {
                    new ExerciseDbModel("Squat", new List<SeriesDbModel>
                    {
                        new SeriesDbModel(10, 60, string.Empty),
                        new SeriesDbModel(8, 100, string.Empty),
                        new SeriesDbModel(6, 120, string.Empty),
                        new SeriesDbModel(6, 140, string.Empty),
                        new SeriesDbModel(6, 160, string.Empty),
                        new SeriesDbModel(6, 180, string.Empty)
                    }),
                    new ExerciseDbModel("Hack machine", new List<SeriesDbModel>
                    {
                        new SeriesDbModel(10, 60, string.Empty),
                        new SeriesDbModel(8, 100, string.Empty),
                        new SeriesDbModel(6, 120, string.Empty),
                        new SeriesDbModel(6, 140, string.Empty)
                    })
                }),

                new TrainingDbModel(ObjectId.GenerateNewId(1).ToString(), "Chest", new DateTime(2020,10,16, 18, 14, 8), new List<ExerciseDbModel>
                {
                    new ExerciseDbModel("Bench press", new List<SeriesDbModel>
                    {
                        new SeriesDbModel(10, 60, string.Empty),
                        new SeriesDbModel(8, 100, string.Empty),
                        new SeriesDbModel(6, 120, string.Empty),
                        new SeriesDbModel(6, 140, string.Empty),
                        new SeriesDbModel(6, 160, string.Empty),
                        new SeriesDbModel(6, 180, string.Empty)
                    }),
                    new ExerciseDbModel("Reversed bench press", new List<SeriesDbModel>
                    {
                        new SeriesDbModel(10, 60, string.Empty),
                        new SeriesDbModel(8, 100, string.Empty),
                        new SeriesDbModel(6, 120, string.Empty),
                        new SeriesDbModel(6, 140, string.Empty)
                    })
                }),
            });
        }
    }
}
