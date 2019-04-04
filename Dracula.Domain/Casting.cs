using System;

namespace Dracula.Domain
{
    public class Casting
    {
        private Casting(){}

        public Casting(Film film, Actor actor, Role role)
        {
            Film = film ?? throw new ArgumentNullException(nameof(film));
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));
            Role = role;
        }

        public Guid Id { get; private set; }
        public Film Film { get; private set; }
        public Actor Actor { get; private set; }
        public Role Role { get; private set; }
    }
}