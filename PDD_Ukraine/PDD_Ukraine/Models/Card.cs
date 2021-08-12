using MongoDB.Bson;
using Realms;

namespace PDD_Ukraine.Models
{
    public class Card : RealmObject
    {
        [PrimaryKey]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string LinkToImage { get; }

        public int State { get; set; }
    }
}