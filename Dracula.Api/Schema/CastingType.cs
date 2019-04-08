using Dracula.Api.Resolvers;
using Dracula.Domain;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class CastingType : ObjectType<Casting>
    {
        protected override void Configure(IObjectTypeDescriptor<Casting> descriptor)
        {
            descriptor.BindFields(BindingBehavior.Explicit);

            descriptor.Field(c => c.Id)
                .Type<NonNullType<UuidType>>();

            descriptor.Field(c => c.Actor)
                .Type<NonNullType<ActorType>>();

            descriptor.Field(c => c.Film)
                .Type<NonNullType<FilmType>>();

            descriptor.Field(c => c.Role)
                .Type<NonNullType<RoleType>>();
        }
    }
}