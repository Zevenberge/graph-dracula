using System;
using System.Collections.Generic;
using Dracula.Api.Resolvers;
using Dracula.Domain;
using HotChocolate.Types;

namespace Dracula.Api.Schema
{
    public class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field<FilmResolver>(r => r.GetAll(default))
                .Type<ListType<FilmType>>()
                .Name("films");

            descriptor.Field<FilmResolver>(r => r.GetById(default, default))
                .Type<FilmType>()
                .Argument("id", d => d.Description("Identifier of the film").Type<NonNullType<UuidType>>())
                .Name("film");

            descriptor.Field<ActorResolver>(r => r.GetAll(default))
                .Type<ListType<ActorType>>()
                .Name("actors");

            descriptor.Field<ActorResolver>(r => r.GetById(default, default))
                .Type<ActorType>()
                .Argument("id", d => d.Description("Identifier of the actor").Type<NonNullType<UuidType>>())
                .Name("actor");
        }
        
    }
}