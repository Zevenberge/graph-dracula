using System;
using Dracula.Domain;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class CreateCasting
    {
        public Guid Actor { get; set; }
        public Guid Film { get; set; }
        public Role Role { get; set; }

        public class Type : InputObjectType<CreateCasting>
        {
            protected override void Configure(IInputObjectTypeDescriptor<CreateCasting> descriptor)
            {
                descriptor.Field(t => t.Actor).Type<NonNullType<UuidType>>();
                descriptor.Field(t => t.Film).Type<NonNullType<UuidType>>();
            }
        }
    }
}