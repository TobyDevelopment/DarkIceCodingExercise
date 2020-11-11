using System;
using System.Collections.Generic;
using System.Text;

namespace DarkIceCodingExercise.Model
{
    public class AccountGroup : List<Account>
    {
        public string Group { get; set; }

        public AccountGroup(string group)
        {
            Group = group;
        }
    }
}
