using System;
using Xamarin.Forms;
using SQLite;
using System.Collections.Generic;

namespace Rave.Models
{
    public class Stablishment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image ProfileImage { get; set; }
        public string Categories { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreationDate { get; set; }
        public string FavoriteSource { get; set; }
    }
}