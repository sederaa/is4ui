using HotChocolate.Types;

public class RootQueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field<ClientResolvers>(x => x.GetClients(default))
            .Type<ListType<NonNullType<ClientType>>>()
            .UseFiltering();
        descriptor.Field<ClientResolvers>(x => x.GetClient(default,default, default))
            .Type<ClientType>()
            .Argument("id", a => a.Type<IntType>());
    }
}
