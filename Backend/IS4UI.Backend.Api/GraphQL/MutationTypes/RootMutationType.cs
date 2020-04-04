using HotChocolate.Types;
using IS4UI.Backend.Api.GraphQL.QueryTypes;
using IS4UI.Backend.Data.Entities;

public class RootMutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        base.Configure(descriptor);
/*
        descriptor.Field<Mutation>(m => m.CreateClient(default, default))
            .Type<NonNullType<ClientType>>()
            .Argument("input", a => a.Type<CreateClientInputType>());

        descriptor.Field<Mutation>(m => m.UpdateClient(default, default))
            .Type<NonNullType<ClientType>>()
            .Argument("input", a => a.Type<UpdateClientInputType>());

        descriptor.Field<Mutation>(m => m.DeleteClient(default, default))
            .Type<NonNullType<ClientType>>()
            .Argument("input", a => a.Type<DeleteClientInputType>());
*/
    }
}
