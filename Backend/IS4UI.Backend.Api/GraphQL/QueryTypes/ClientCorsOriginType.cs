using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class ClientCorsOriginType : ObjectType<ClientCorsOrigin>
{
   protected override void Configure(IObjectTypeDescriptor<ClientCorsOrigin> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}


