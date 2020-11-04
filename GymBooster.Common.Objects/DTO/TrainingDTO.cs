using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GymBooster.Common.Objects.DTO
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class TrainingDTO
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedOnUtc { get; set; }

        public List<ExerciseDTO> Exercises { get; set; }


        public TrainingDTO()
        {
        }

        public TrainingDTO(string id, string title, DateTime createdOnUtc, List<ExerciseDTO> exercises)
        {
            Id = id;
            Title = title;
            CreatedOnUtc = createdOnUtc;
            Exercises = exercises;
        }

        public override string ToString()
        {
            return $"{Id} | {Title} | {CreatedOnUtc} | {Exercises.Count}";
        }
    }
}
