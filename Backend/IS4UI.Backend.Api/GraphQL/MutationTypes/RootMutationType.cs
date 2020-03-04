using HotChocolate.Types;

public class RootMutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(m => m.CreateClient(default, default))
            .Type<ClientType>()
            .Argument("input", a => a.Type<CreateClientInputType>());

        descriptor.Field(m => m.UpdateClient(default, default))
            .Type<ClientType>()
            .Argument("input", a => a.Type<UpdateClientInputType>());

        descriptor.Field(m => m.DeleteClient(default, default))
            .Type<ClientType>()
            .Argument("input", a => a.Type<DeleteClientInputType>());

    }
}
