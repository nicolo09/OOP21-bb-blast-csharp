using System;

namespace Project.Tests
{
    public class PersonForTest : IEquatable<PersonForTest>
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public PersonForTest(string name, string surname, int age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
        }
        private PersonForTest()
        {

        }
        public override bool Equals(object obj)
        {
            return Equals(obj as PersonForTest);
        }
        public bool Equals(PersonForTest other)
        {
            return other != null &&
                   Name == other.Name &&
                   Surname == other.Surname &&
                   Age == other.Age;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Age);
        }
    }
}
