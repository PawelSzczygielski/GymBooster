using System.Collections.Generic;
using System.Linq;
using GymBooster.Common.Objects.DTO;
using GymBooster.Common.Utils;
using GymBooster.DatabaseAccess.DbModel;

namespace GymBooster.Api.Infrastructure
{
    public static class Mapper
    {
        public static List<TrainingDTO> Map(IEnumerable<TrainingDbModel> allTrainings)
        {
            return allTrainings.Select(Map).ToList();
        }

        public static TrainingDTO Map(TrainingDbModel training)
        {
            return new TrainingDTO(training.Id.ToString(), training.Title, training.CreatedOnUtc.ToUniversalTime(), Map(training.Excercises));
        }

        private static List<ExerciseDTO> Map(List<ExerciseDbModel> exercises)
        {
            return exercises.NullOrEmpty() ? new List<ExerciseDTO>() : exercises.Select(Map).ToList();
        }

        private static ExerciseDTO Map(ExerciseDbModel exercise)
        {
            return new ExerciseDTO(exercise.Name, Map(exercise.Series));
        }

        private static List<SeriesDTO> Map(List<SeriesDbModel> series)
        {
            return series.NullOrEmpty() ? new List<SeriesDTO>() : series.Select(Map).ToList();
        }

        private static SeriesDTO Map(SeriesDbModel series)
        {
            return new SeriesDTO(series.RepsNo, series.Weight, series.Comment);
        }

        public static TrainingDbModel Map(TrainingDTO training)
        {
            return new TrainingDbModel(training.Id, training.Title, training.CreatedOnUtc, Map(training.Exercises));
        }

        private static List<ExerciseDbModel> Map(List<ExerciseDTO> trainingExercises)
        {
            return trainingExercises.NullOrEmpty()
                ? new List<ExerciseDbModel>()
                : trainingExercises.Select(Map).ToList();
        }

        private static ExerciseDbModel Map(ExerciseDTO exercise)
        {
            return new ExerciseDbModel(exercise.Name, Map(exercise.Series));
        }

        private static List<SeriesDbModel> Map(List<SeriesDTO> series)
        {
            return series.NullOrEmpty() ? new List<SeriesDbModel>() : series.Select(Map).ToList();
        }

        private static SeriesDbModel Map(SeriesDTO series)
        {
            return new SeriesDbModel(series.NumberOfReps, series.Weight, series.Comment);
        }
    }
}