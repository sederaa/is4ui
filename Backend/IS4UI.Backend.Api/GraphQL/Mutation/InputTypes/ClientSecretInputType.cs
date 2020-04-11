using HotChocolate.Types;

public class ClientSecretInputType : InputObjectType<CreateClientSecretInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateClientSecretInput> descriptor)
    {
        base.Configure(descriptor);
    }

}