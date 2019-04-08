using Dracula.Api.Resolvers;
using Dracula.Domain;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class FilmType : ObjectType<Film>
    {
        protected override void Configure(IObjectTypeDescriptor<Film> descriptor)
        {
            descriptor.BindFields(BindingBehavior.Explicit);

            descriptor.Field(a => a.Id)
                .Type<NonNullType<UuidType>>();

            descriptor.Field(a => a.Name)
                .Type<NonNullType<StringType>>();

            descriptor.Field(a => a.ReleaseYear)
                .Type<NonNullType<IntType>>();

            descriptor.Field(a => a.Country)
                .Type<NonNullType<CountryType>>()
                .Name("country");

            descriptor.Field<CastingResolver>(r => r.GetCast(default, default))
                .Type<NonNullType<ListType<NonNullType<CastingType>>>>()
                .Name("actors");

        }
    }
}