using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;
////////////// QUERY TYPES //////////////
// Add to QueryTypes folder.

public class ApiClaimType : ObjectType<ApiClaim>
{
   protected override void Configure(IObjectTypeDescriptor<ApiClaim> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}


