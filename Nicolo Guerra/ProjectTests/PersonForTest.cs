using System;

namespace Project.Tests
{
    [Serializable()]
    public class PersonForTest : IEquatable<PersonForTest>
    {

        public string Name { get; }
        public string Surname { get; }
        public int Age { get; }
        public PersonForTest(string name, string surname, int age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
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
