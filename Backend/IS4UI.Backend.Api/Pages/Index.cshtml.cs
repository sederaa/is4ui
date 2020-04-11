using System;
using System.Linq;
using System.Threading.Tasks;
using IS4UI.Backend.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IS4UI.Backend.Api;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace IS4UI.Backend.Api.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string Code { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var codeBuilder = new StringBuilder();
            var dataAssembly = typeof(Client).Assembly;
            var allTypes = dataAssembly.GetTypes();
            var entityTypes = allTypes.Where(t => t.Namespace == "IS4UI.Backend.Data.Entities");
            var entityTypesFullNames = entityTypes.Select(t => t.FullName).ToArray();

            codeBuilder.AppendLine("////////////// QUERY TYPES //////////////");
            codeBuilder.AppendLine("// Add to QueryTypes folder.\n");

            foreach (var entityType in entityTypes)
            {
                codeBuilder.AppendLine($"public class {entityType.Name}Type : ObjectType<{entityType.Name}>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"   protected override void Configure(IObjectTypeDescriptor<{entityType.Name}> descriptor)");
                codeBuilder.AppendLine("   {");

                var properties = entityType.GetProperties().OrderBy(p => p.Name);
                foreach (var property in properties)
                {
                    var type = property.PropertyType;
                    if (property.Name == "Id" || (property.Name == "Key" && entityType.Name == "PersistedGrant")
                        || (property.Name == "UserCode" && entityType.Name == "DeviceCode"))
                    {
                        codeBuilder.AppendLine($"      descriptor.Field(x => x.{property.Name})");
                        codeBuilder.AppendLine($"                .Type<NonNullType<IdType>>();\n");
                    }
                    else if (new[] { "Int32", "String", "Boolean", "DateTime", "Nullable`1" }.Contains(type.Name))
                    {
                        // Ignore these as the property and it's type will be handled correctly by ObjectType<>
                    }
                    else if (type.IsClass)
                    {
                        // Ignore these as they are references back to the parent entity. They will be handled by the parent class and the corresponding collections.
                    }
                    else if (type.GetInterface(nameof(IEnumerable)) != null)
                    {
                        var genericType = type.GetTypeInfo().GenericTypeArguments[0];
                        codeBuilder.AppendLine($"      descriptor.Field<{genericType.Name}Resolvers>(x => x.Get{property.Name}ByParentId(default, default))");
                        codeBuilder.AppendLine($"                .Type<ListType<NonNullType<{genericType.Name}Type>>>()");
                        codeBuilder.AppendLine($"                .Name(\"{property.Name.FirstCharacterToLower()}\");\n");
                    }
                    else
                    {
                        codeBuilder.AppendLine($"\n      !!! Unhandled property: {property.Name}: {type.Name} !!!");
                    }
                }

                codeBuilder.AppendLine("   }");
                codeBuilder.AppendLine("}\n");
            }

            codeBuilder.AppendLine("////////////// RESOLVERS //////////////");
            codeBuilder.AppendLine("// Add to Resolvers folder.\n");

            foreach (var entityType in entityTypes)
            {
                var referenceProperty = entityType.GetProperties().FirstOrDefault(p => p.PropertyType.IsClass && entityTypesFullNames.Contains(p.PropertyType.FullName));
                var isRootEntity = referenceProperty == null;

                var pluralEntityTypeName = entityType.Name.EndsWith("Property") ? entityType.Name.Replace("Property", "Properties") : entityType.Name + "s";

                codeBuilder.AppendLine($"public class {entityType.Name}Resolvers");
                codeBuilder.AppendLine("{");

                if (isRootEntity)
                {
                    codeBuilder.AppendLine($"   public IQueryable<{entityType.Name}> Get{pluralEntityTypeName}([Service] ApplicationDbContext db)");
                    codeBuilder.AppendLine("   {");
                    codeBuilder.AppendLine($"      return db.{pluralEntityTypeName};");
                    codeBuilder.AppendLine("   }\n");
                    codeBuilder.AppendLine($"   public async Task<{entityType.Name}> Get{entityType.Name}([Service] ApplicationDbContext db, IResolverContext context, int id)");
                    codeBuilder.AppendLine("   {");
                    codeBuilder.AppendLine($"       var dataLoader = context.BatchDataLoader<int, {entityType.Name}>(nameof(Get{entityType.Name}), async ids => await db.{pluralEntityTypeName}.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));");
                    codeBuilder.AppendLine($"       return await dataLoader.LoadAsync(id, context.RequestAborted);");
                    codeBuilder.AppendLine("   }");
                }
                else
                {
                    codeBuilder.AppendLine($"   public List<{entityType.Name}> Get{pluralEntityTypeName}ByParentId([Service] ApplicationDbContext db, [Parent]{referenceProperty.Name} {referenceProperty.Name.FirstCharacterToLower()})");
                    codeBuilder.AppendLine("   {");
                    codeBuilder.AppendLine($"       return db.{pluralEntityTypeName}.Where(x => x.{referenceProperty.Name}Id == {referenceProperty.Name.FirstCharacterToLower()}.Id).ToList();");
                    codeBuilder.AppendLine("   }");
                }
                codeBuilder.AppendLine("}\n");
            }


            codeBuilder.AppendLine("////////////// ROOT QUERY TYPES //////////////");
            codeBuilder.AppendLine("// Add to RootQueryType class.\n");

            var rootQueryTypes = entityTypes.Where(t => !t.GetProperties().Any(p => p.PropertyType.IsClass && entityTypesFullNames.Contains(p.PropertyType.FullName)));

            codeBuilder.AppendLine($"public class RootQueryType : ObjectType");
            codeBuilder.AppendLine("{");
            codeBuilder.AppendLine($"    protected override void Configure(IObjectTypeDescriptor descriptor)");
            codeBuilder.AppendLine("    {");

            foreach (var rootQueryType in rootQueryTypes)
            {
                codeBuilder.AppendLine($"      descriptor.Field<{rootQueryType.Name}Resolvers>(x => x.Get{rootQueryType.Name}s(default))");
                codeBuilder.AppendLine($"                .Type<ListType<NonNullType<{rootQueryType.Name}Type>>>()");
                codeBuilder.AppendLine($"                .UseFiltering();\n");
                codeBuilder.AppendLine($"      descriptor.Field<{rootQueryType.Name}Resolvers>(x => x.Get{rootQueryType.Name}(default,default, default))");
                codeBuilder.AppendLine($"                .Type<{rootQueryType.Name}Type>()");
                codeBuilder.AppendLine($"                .Argument(\"id\", a => a.Type<IntType>());\n");
            }

            codeBuilder.AppendLine("   }");
            codeBuilder.AppendLine("}");

            Code = codeBuilder.ToString();
        }
    }
}
