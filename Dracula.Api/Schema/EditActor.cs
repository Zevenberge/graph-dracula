using System;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class EditActor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public class Type : InputObjectType<EditActor>
        {
            protected override void Configure(IInputObjectTypeDescriptor<EditActor> descriptor)
            {
                descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();
                descriptor.Field(t => t.DateOfBirth).Type<DateType>();
            }
        }
    }
}