using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class IdentityResourceType : ObjectType<IdentityResource>
{
   protected override void Configure(IObjectTypeDescriptor<IdentityResource> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

      descriptor.Field<IdentityClaimResolvers>(x => x.GetIdentityClaimsByParentId(default, default))
                .Type<ListType<NonNullType<IdentityClaimType>>>()
                .Name("identityClaims");

      descriptor.Field<IdentityPropertyResolvers>(x => x.GetIdentityPropertiesByParentId(default, default))
                .Type<ListType<NonNullType<IdentityPropertyType>>>()
                .Name("identityProperties");

   }
}


