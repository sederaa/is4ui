using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class ClientSecretType : ObjectType<ClientSecret>
{
   protected override void Configure(IObjectTypeDescriptor<ClientSecret> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}


