using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class ClientClaimType : ObjectType<ClientClaim>
{
   protected override void Configure(IObjectTypeDescriptor<ClientClaim> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}


