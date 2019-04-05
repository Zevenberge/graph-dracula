using System;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class CreateActor
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public class Type : InputObjectType<CreateActor>
        {
            protected override void Configure(IInputObjectTypeDescriptor<CreateActor> descriptor)
            {
                descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
                descriptor.Field(t => t.DateOfBirth).Type<NonNullType<DateType>>();
                descriptor.Field(t => t.Nationality).Type<NonNullType<StringType>>()
                    .Description("Iso code of the nationality of the actor");
            }
        }
    }
}