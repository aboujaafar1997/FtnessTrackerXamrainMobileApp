using System;
using System.Collections.Generic;
using System.Text;

namespace fitnessTraker.Model
{
    public class Utilisateur 
    {
        public string _id { get; set; }
        public string nom { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string sex { get; set; }
        public string topDistance { get; set; }
        public string date { get; set; }

    }
}
