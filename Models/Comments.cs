using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FacebookAPI.Models
{
    public class Comments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; }

        public string PublicationId { get; set; }

        public string Text { get; set; }
    }
}
