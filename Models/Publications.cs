using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FacebookAPI.Models
{
    public class Publications
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; } // Assuming the ID of the user who posted the publication

        public string Text { get; set; }

        public List<string> ImageUrls { get; set; }

        public List<string> VideoUrls { get; set; }

        public DateTime DatePosted { get; set; }
    }

}
