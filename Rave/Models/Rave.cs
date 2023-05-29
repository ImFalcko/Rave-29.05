using MySqlConnector;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Rave.Models
{
    public class Raves
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
        public string Categories { get; set; }
        public string Location { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
