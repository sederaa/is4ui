using HotChocolate.Types;

public class UpdateClientInputType : InputObjectType<UpdateClientInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdateClientInput> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(x => x.Id)
            .Type<NonNullType<IdType>>();

        descriptor.Field(x => x.ClientId)
            .Type<NonNullType<StringType>>();

        descriptor.Field(x => x.ClientName)
            .Type<NonNullType<StringType>>();
    }
}
