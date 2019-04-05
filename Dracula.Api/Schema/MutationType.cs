using Dracula.Api.Resolvers;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class MutationType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field<FilmResolver>(f => f.Create(default, default, default))
                .Type<NonNullType<FilmType>>()
                .Argument("data", a => a.Type<NonNullType<CreateFilm.Type>>())
                .Name("createFilm");
        }

    }
}