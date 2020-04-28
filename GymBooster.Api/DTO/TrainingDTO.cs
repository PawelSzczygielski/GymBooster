using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace GymBooster.Api.DTO
{
    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class TrainingDTO : IEquatable<TrainingDTO>
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }
        
        public List<ExcerciseDTO> Excercises { get; set; }

        protected TrainingDTO()
        {
        }

        public TrainingDTO(string id, string title)
        {
            Id = id;
            Title = title;
            Excercises = new List<ExcerciseDTO>();
        }

     

        public override string ToString()
        {
            return $"{Id} | {Title} | {Excercises.Count}";
        }

        public bool Equals(TrainingDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Title == other.Title && Excercises.SequenceEqual(other.Excercises);
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
            return HashCode.Combine(Id, Title, Excercises);
        }
    }
}
