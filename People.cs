using System;

namespace CustomTypes
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person(string f, string l, int a)
        {
            this.FirstName = f;
            this.LastName = l;
            this.Age = a;
        }

        public override string ToString()
        {
            return $"Person: {this.FirstName} {this.LastName} {this.Age}";
        }
    }
}