using System.Collections.Generic;
using Xamarin.Forms;

namespace Rave.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public List<string> Preferences { get; set; }
        public Image ProfileImage { get; set; }
    }
}