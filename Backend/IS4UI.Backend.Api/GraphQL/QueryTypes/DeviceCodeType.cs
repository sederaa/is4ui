using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class DeviceCodeType : ObjectType<DeviceCode>
    {
        protected override void Configure(IObjectTypeDescriptor<DeviceCode> descriptor)
        {
            descriptor.Field(x => x.UserCode)
                      .Type<NonNullType<IdType>>();

        }
    }
}

