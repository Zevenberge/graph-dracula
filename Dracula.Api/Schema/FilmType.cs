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
                .Type<NonNullType<IdType>>();

            descriptor.Field(a => a.Name)
                .Type<NonNullType<StringType>>();

            descriptor.Field(a => a.ReleaseYear)
                .Type<NonNullType<IntType>>();

            //descriptor.Field(a => a.Country.Name)
            //    .Type<NonNullType<StringType>>()
            //    .Name("country");

            descriptor.Field<ActorResolver>(r => r.GetActors(default, default))
                .Type<ListType<ActorType>>()
                .Name("films");

        }
    }

    public class CreateFilm
    {
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public string CountryIso { get; set; }
        public class Type : InputObjectType<CreateFilm>
        {
            protected override void Configure(IInputObjectTypeDescriptor<CreateFilm> descriptor)
            {
                descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
                descriptor.Field(t => t.CountryIso).Type<NonNullType<StringType>>();
            }
        }
    }
}