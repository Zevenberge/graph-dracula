using Dracula.Api.Resolvers;
using Dracula.Domain;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class ActorType : ObjectType<Actor>
    {
        protected override void Configure(IObjectTypeDescriptor<Actor> descriptor)
        {
            descriptor.BindFields(BindingBehavior.Explicit);

            descriptor.Field(a => a.Id)
                .Type<NonNullType<UuidType>>();

            descriptor.Field(a => a.Name)
                .Type<NonNullType<StringType>>();

            descriptor.Field(a => a.DateOfBirth)
                .Type<NonNullType<DateType>>();

            descriptor.Field(a => a.Nationality)
                .Type<NonNullType<CountryType>>()
                .Name("nationality");

            descriptor.Field<CastingResolver>(r => r.GetPlays(default, default))
                .Type<NonNullType<ListType<NonNullType<CastingType>>>>()
                .Name("films");
        }
    }
}