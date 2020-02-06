using HotChocolate.Types;

public class RootQueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field<ClientResolvers>(x => x.GetClients()).Type<ListType<NonNullType<ClientType>>>();
        descriptor.Field<ClientResolvers>(x => x.GetClient(default)).Type<NonNullType<ClientType>>().Argument("id", a => a.Type<IntType>());
    }
}
