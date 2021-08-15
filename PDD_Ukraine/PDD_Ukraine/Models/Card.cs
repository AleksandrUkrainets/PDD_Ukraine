using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

namespace PDD_Ukraine.Models
{
    public class Card : RealmObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string LinkToImage { get; set; }

        public int State { get; set; }

        public int Order { get; set; }
    }
}