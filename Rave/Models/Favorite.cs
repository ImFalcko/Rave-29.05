using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Rave.Models
{
    public class Favorite
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StablishmentId { get; set; }
    }
}
