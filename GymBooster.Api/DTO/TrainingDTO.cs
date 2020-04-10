using System;
using System.Diagnostics;
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
        [JsonProperty(Required = Required.Always)]
        public string Content { get; set; }

        protected TrainingDTO()
        {
        }

        public TrainingDTO(string id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
        }

        public bool Equals(TrainingDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Title == other.Title && Content == other.Content;
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
            return HashCode.Combine(Id, Title, Content);
        }

        public override string ToString()
        {
            return $"{Id} | {Title} | {Content}";
        }
    }
}
