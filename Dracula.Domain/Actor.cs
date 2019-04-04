using System;

namespace Dracula.Domain
{
    public class Actor
    {
        private Actor() {}

        public Actor(string name, DateTime dateOfBirth, Country nationality)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Name = name;
            DateOfBirth = dateOfBirth;
            Nationality = nationality ?? throw new ArgumentNullException(nameof(nationality));
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Country Nationality { get; private set; }

        public void Naturalise(Country newNationality)
        {
            Nationality = newNationality;
        }
    }
}