using System;
using System.Collections;

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

    public class Pizza
    {
        public PizzaSize Size { get; set; }
        public ArrayList Toppings { get; set; }

        public Pizza(PizzaSize s, ArrayList t)
        {
            this.Size = s;
            this.Toppings = t;
        }

        public override string ToString()
        {
            return $"Pizza Size: {this.Size} Toppings: {string.Join(',', this.Toppings.ToArray())}";
        }
    }

    public enum PizzaSize
    {
        S = 1,
        M = 2,
        L = 3
    }
}