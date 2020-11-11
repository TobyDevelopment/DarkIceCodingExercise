using System;
using System.Collections.Generic;
using System.Text;

namespace DarkIceCodingExercise.Model
{
    public class Name
    {
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Account
    {
        public string _id { get; set; }
        public bool isActive { get; set; }
        public string accountBalance { get; set; }
        public string picture { get; set; }
        public int age { get; set; }
        public string eyeColor { get; set; }
        public string color { get; set; }
        public string gender { get; set; }
        public Name name { get; set; }
        public string fullName { get; set; }
        public string registered { get; set; }
    }
}
