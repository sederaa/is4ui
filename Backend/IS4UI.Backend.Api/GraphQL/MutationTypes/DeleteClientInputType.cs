using HotChocolate.Types;

public class DeleteClientInputType : InputObjectType<DeleteClientInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<DeleteClientInput> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(x => x.Id)
            .Type<NonNullType<IdType>>();
    }
}
