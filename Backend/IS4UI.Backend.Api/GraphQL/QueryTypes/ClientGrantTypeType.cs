using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class ClientGrantTypeType : ObjectType<ClientGrantType>
{
   protected override void Configure(IObjectTypeDescriptor<ClientGrantType> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}


