using System;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class EditFilm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? ReleaseYear { get; set; }
        public string Country { get; set; }
        public class Type : InputObjectType<EditFilm>
        {
            protected override void Configure(IInputObjectTypeDescriptor<EditFilm> descriptor)
            {
                descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();
            }
        }
    }
}