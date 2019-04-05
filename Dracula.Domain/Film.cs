using System;

namespace Dracula.Domain
{
    public class Film
    {
        private Film(){}

        public Film(string name, int releaseYear, Country country)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Name = name;
            ReleaseYear = releaseYear;
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int ReleaseYear { get; private set; }

        public Country Country { get; private set; }

        public void CorrectInformation(string name, int? releaseYear, Country country)
        {
            Name = name ?? Name;
            ReleaseYear = releaseYear ?? ReleaseYear;
            Country = country ?? Country;
        }
    }
}