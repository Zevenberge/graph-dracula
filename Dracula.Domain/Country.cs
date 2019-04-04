using System;

namespace Dracula.Domain
{
    public class Country
    {
        private Country() {}

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Iso { get; private set; }
    }
}