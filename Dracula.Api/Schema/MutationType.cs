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

            descriptor.Field<FilmResolver>(f => f.Edit(default, default, default))
                .Type<NonNullType<FilmType>>()
                .Argument("data", a => a.Type<NonNullType<EditFilm.Type>>())
                .Name("editFilm");

            descriptor.Field<ActorResolver>(a => a.Create(default, default, default))
                .Type<NonNullType<ActorType>>()
                .Argument("data", a => a.Type<NonNullType<CreateActor.Type>>())
                .Name("createActor");

            descriptor.Field<ActorResolver>(g => g.Edit(default, default, default))
                .Type<NonNullType<ActorType>>()
                .Argument("data", a => a.Type<NonNullType<EditActor.Type>>())
                .Name("editActor");

            descriptor.Field<CastingResolver>(g => g.Create(default, default, default, default))
                .Type<NonNullType<CastingType>>()
                .Argument("data", a => a.Type<NonNullType<CreateCasting.Type>>())
                .Name("createCasting");

            descriptor.Field<CastingResolver>(g => g.Edit(default, default, default, default))
                .Type<NonNullType<CastingType>>()
                .Argument("data", a => a.Type<NonNullType<EditCasting.Type>>())
                .Name("editCasting");

            descriptor.Field<CastingResolver>(g => g.Delete(default, default))
                .Type<NonNullType<UuidType>>()
                .Argument("id", a => a.Type<NonNullType<UuidType>>())
                .Name("deleteCasting");
        }

    }
}