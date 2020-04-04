using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class ClientRedirectUriType : ObjectType<ClientRedirectUri>
{
   protected override void Configure(IObjectTypeDescriptor<ClientRedirectUri> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}


