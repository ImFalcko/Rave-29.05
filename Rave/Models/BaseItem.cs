using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rave.Models
{
    [Table("Favorites")]
    public class BaseItem
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProfileImage { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
