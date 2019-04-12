using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class CreateFilm
    {
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public string Country { get; set; }
        public class Type : InputObjectType<CreateFilm>
        {
            protected override void Configure(IInputObjectTypeDescriptor<CreateFilm> descriptor)
            {
                descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
                descriptor.Field(t => t.Country).Type<NonNullType<StringType>>();
            }
        }
    }
}