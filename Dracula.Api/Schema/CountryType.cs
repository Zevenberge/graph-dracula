using Dracula.Domain;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class CountryType : ObjectType<Country>
    {
        protected override void Configure(IObjectTypeDescriptor<Country> descriptor)
        {
            descriptor.BindFields(BindingBehavior.Explicit);

            descriptor.Field(a => a.Name)
                .Type<NonNullType<StringType>>();

            descriptor.Field(a => a.Iso)
                .Type<NonNullType<StringType>>();
        }
    }
}