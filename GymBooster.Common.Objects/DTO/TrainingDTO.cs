using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GymBooster.Common.Objects.DTO
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class TrainingDTO : IEquatable<TrainingDTO>
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        public List<ExerciseDTO> Exercises { get; set; }


        public TrainingDTO()
        {
        }

        public TrainingDTO(string id, string title, List<ExerciseDTO> exercises)
        {
            Id = id;
            Title = title;
            Exercises = exercises;
        }

     

        public override string ToString()
        {
            return $"{Id} | {Title} | {Exercises.Count}";
        }

        public bool Equals(TrainingDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Title == other.Title && Equals(Exercises, other.Exercises);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TrainingDTO) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Exercises != null ? Exercises.GetHashCode() : 0);
                return hashCode;
            }
        }

    }
}
