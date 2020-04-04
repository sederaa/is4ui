using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class ClientPostLogoutRedirectUriType : ObjectType<ClientPostLogoutRedirectUri>
{
   protected override void Configure(IObjectTypeDescriptor<ClientPostLogoutRedirectUri> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}


