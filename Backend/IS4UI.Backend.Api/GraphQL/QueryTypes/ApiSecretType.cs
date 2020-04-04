using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class ApiSecretType : ObjectType<ApiSecret>
    {
        protected override void Configure(IObjectTypeDescriptor<ApiSecret> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

