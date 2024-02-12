using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03ListViewGridView
{
    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public static bool IsNameValid(string name, out string error)
        {
            if (name.Length < 2 || name.Length > 50 || name == null)
            {
                error = "Name is not valid.";
                return false;
            } else {
                error = "";
                return true;
            }
        }
        public static bool IsAgeValid(int age, out string error)
        {
            if (age < 0 ||  age > 100 )
            {
                error = "Age is not valid";
                return false;
            }
            else
            {
                error = "";
                return true;
            }
        }
    }
}
