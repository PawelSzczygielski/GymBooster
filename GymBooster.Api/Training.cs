using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymBooster.Api
{
    public class Training
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; } //todo: to be dropped? or not.
        public string Title { get; set; }
        public string Content { get; set; }
    }
}