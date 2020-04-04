using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class PersistedGrantType : ObjectType<PersistedGrant>
{
   protected override void Configure(IObjectTypeDescriptor<PersistedGrant> descriptor)
   {
      descriptor.Field(x => x.Key)
                .Type<NonNullType<IdType>>();

   }
}


