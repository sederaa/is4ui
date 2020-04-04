using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

public class ApiPropertyType : ObjectType<ApiProperty>
{
   protected override void Configure(IObjectTypeDescriptor<ApiProperty> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}


