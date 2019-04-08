using System;
using Dracula.Domain;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class EditCasting
    {
        public Guid Id { get; set; }
        public Guid? Actor { get; set; }
        public Guid? Film { get; set; }
        public Role? Role { get; set; }

        public class Type : InputObjectType<EditCasting>
        {
            protected override void Configure(IInputObjectTypeDescriptor<EditCasting> descriptor)
            {
                descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();
                descriptor.Field(t => t.Actor).Type<UuidType>();
                descriptor.Field(t => t.Film).Type<UuidType>();
            }
        }

    }
}