using System;

namespace GymBooster.Api.DTO
{
    public class TrainingDTO : IEquatable<TrainingDTO>
    {
        public long Id { get;  }
        public string Title { get;  }
        public string Content { get; }

        protected TrainingDTO()
        {
        }

        public TrainingDTO(long id, string title, string content)
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
    }
}
