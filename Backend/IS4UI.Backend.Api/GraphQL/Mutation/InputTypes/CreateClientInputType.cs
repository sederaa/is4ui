using HotChocolate.Types;

public class CreateClientInputType : InputObjectType<CreateClientInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateClientInput> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(x => x.ClientId)
            .Type<NonNullType<StringType>>()
            ;

        descriptor.Field(x => x.ClientName)
            .Type<NonNullType<StringType>>()
            ;

        descriptor.Field(x => x.ClientSecrets)
            .Type<NonNullType<ListType<ClientSecretInputType>>>()
            ;
    }

}
