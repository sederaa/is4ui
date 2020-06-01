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
using System.ComponentModel.DataAnnotations;

namespace IS4UI.Backend.Api.Pages
{
    public class MutationsModel : PageModel
    {
        private readonly ILogger<MutationsModel> _logger;

        public string Code { get; set; }

        public MutationsModel(ILogger<MutationsModel> logger)
        {
            _logger = logger;
        }

        public enum PropertyDescriptorClassifications
        {
            Regular,
            Pk,
            ParentId,
            ParentRef,
            Created,
            Updated,
            Children
        }

        public class EntityDescriptor
        {
            public string Name { get; set; }
            public List<PropertyDescriptor> Properties { get; set; } = new List<PropertyDescriptor>();
            public List<EntityDescriptor> Children { get; set; } = new List<EntityDescriptor>();
        }

        public class PropertyDescriptor
        {
            public string Name { get; set; }
            public string TypeName { get; set; }
            public string GenericTypeName { get; set; }
            public bool IsRequired { get; set; }
            public int StringLength { get; set; }
            public PropertyDescriptorClassifications Classification { get; set; }
        }



        public void OnGet()
        {
            List<EntityDescriptor> entitiesTree = BuildEntityTree();

            // var tree = RenderTree(entitiesTree);
            // Code = tree;
            // return;

            var codeBuilder = new StringBuilder();

            codeBuilder.AppendLine("////////////// INPUTS //////////////");
            codeBuilder.AppendLine("// Add to Mutation\\Inputs folder.\n");

            RenderInputs(entitiesTree, codeBuilder);

            codeBuilder.AppendLine("////////////// INPUT TYPES //////////////");
            codeBuilder.AppendLine("// Add to Mutation\\InputTypes folder.\n");

            RenderInputTypes(entitiesTree, codeBuilder);

            codeBuilder.AppendLine("////////////// INPUT VALIDATORS //////////////");
            codeBuilder.AppendLine("// Add to Mutation\\Validators folder.\n");

            RenderInputValidators(entitiesTree, codeBuilder);

            codeBuilder.AppendLine("////////////// MUTATION SERVICE CLASS //////////////");
            codeBuilder.AppendLine("// Add to Mutation\\Services folder.\n");

            RenderMutationServiceClass(entitiesTree, codeBuilder);

            Code = codeBuilder.ToString();

        }

        private static string RenderMutationServiceClass(List<EntityDescriptor> entitiesTree, StringBuilder codeBuilder = null)
        {
            codeBuilder = codeBuilder ?? new StringBuilder();
            foreach (var entity in entitiesTree)
            {
                codeBuilder.AppendLine($"// Filename: {entity.Name}MutationService.cs");
                codeBuilder.AppendLine($"public partial class Mutation");
                codeBuilder.AppendLine("{");

                // Create
                var entityVariableName = entity.Name.FirstCharacterToLower();
                codeBuilder.AppendLine($"   public async Task<{entity.Name}> Create{entity.Name}([Service] ApplicationDbContext db, Create{entity.Name}Input input)");
                codeBuilder.AppendLine("   {");
                codeBuilder.AppendLine($"      var {entityVariableName} = new {entity.Name}");
                codeBuilder.AppendLine("      {");

                void GenerateEntityProperties_Create(EntityDescriptor entity, string indent)
                {
                    foreach (var property in entity.Properties)
                    {
                        if (property.Classification == PropertyDescriptorClassifications.Regular)
                        {
                            codeBuilder.AppendLine($"{indent}{property.Name} = input.{property.Name},");
                        }
                        else if (property.Classification == PropertyDescriptorClassifications.Created)
                        {
                            codeBuilder.AppendLine($"{indent}{property.Name} = DateTime.Now,");
                        }
                        else if (property.Classification == PropertyDescriptorClassifications.Children)
                        {
                            var childEntity = entity.Children.FirstOrDefault(c => c.Name == property.GenericTypeName);
                            if (childEntity == null)
                                throw new Exception($"{entity.Name}: failed to find child entity for property name '{property.GenericTypeName}'.");
                            codeBuilder.AppendLine($"{indent}{property.Name} = input.{property.Name}.Select(x => new {property.GenericTypeName}()");
                            codeBuilder.AppendLine($"{indent}{{");
                            GenerateEntityProperties_Create(childEntity, $"{indent}   ");
                            codeBuilder.AppendLine($"{indent}}}).ToList(),");
                        }
                    }
                }

                GenerateEntityProperties_Create(entity, "         ");

                codeBuilder.AppendLine("      };");
                codeBuilder.AppendLine($"      db.{entity.Name}s.Add({entityVariableName});");
                codeBuilder.AppendLine($"      await db.SaveChangesAsync();");
                codeBuilder.AppendLine($"      return {entityVariableName};");
                codeBuilder.AppendLine("   }");
                codeBuilder.AppendLine("");

                // Update
                codeBuilder.AppendLine($"   public async Task<{entity.Name}> Update{entity.Name}([Service] ApplicationDbContext db, Update{entity.Name}Input input)");
                codeBuilder.AppendLine("   {");
                codeBuilder.AppendLine($"      var {entityVariableName} = await db.FindAsync<{entity.Name}>(input.Id);");

                void GenerateEntityProperties_Update(EntityDescriptor entity, string indent, string inputName = "input")
                {
                    var entityVariableName = entity.Name.FirstCharacterToLower();
                    foreach (var property in entity.Properties)
                    {
                        if (property.Classification == PropertyDescriptorClassifications.Regular)
                        {
                            codeBuilder.AppendLine($"{indent}{entityVariableName}.{property.Name} = {inputName}.{property.Name};");
                        }
                        else if (property.Classification == PropertyDescriptorClassifications.Children)
                        {
                            var childEntity = entity.Children.FirstOrDefault(c => c.Name == property.GenericTypeName);
                            if (childEntity == null)
                                throw new Exception($"{entity.Name}: failed to find child entity for property name '{property.GenericTypeName}'.");
                            var childEntityVariableName = childEntity.Name.FirstCharacterToLower();

                            codeBuilder.AppendLine($"");
                            codeBuilder.AppendLine($"{indent}// start {property.Name} property");
                            codeBuilder.AppendLine($"{indent}foreach (var {childEntityVariableName}Input in {inputName}.{property.Name})");
                            codeBuilder.AppendLine($"{indent}{{");
                            codeBuilder.AppendLine($"{indent}    var {childEntityVariableName} = await db.FindAsync<{property.GenericTypeName}>({childEntityVariableName}Input.Id)");
                            codeBuilder.AppendLine($"{indent}                            ?? new {property.GenericTypeName}");
                            codeBuilder.AppendLine($"{indent}                            {{");
                            // TODO: child entity properties that should be set only on create, e.g. Created and FK
                            codeBuilder.AppendLine($"{indent}                                Created = DateTime.Now,");
                            codeBuilder.AppendLine($"{indent}                                //ClientId = client.Id");
                            codeBuilder.AppendLine($"{indent}                            }};");

                            GenerateEntityProperties_Update(childEntity, $"{indent}    ", $"{childEntityVariableName}Input");

                            codeBuilder.AppendLine($"{indent}    if ({childEntityVariableName}Entity.Id == 0)");
                            codeBuilder.AppendLine($"{indent}    {{");
                            codeBuilder.AppendLine($"{indent}        db.{property.GenericTypeName}s.Add({childEntityVariableName}Entity);");
                            codeBuilder.AppendLine($"{indent}    }}");
                            codeBuilder.AppendLine($"{indent}}}");
                            codeBuilder.AppendLine($"{indent}var {childEntityVariableName}IdsInInput = {inputName}.{property.Name}.Select(x => x.Id).ToArray();");
                            codeBuilder.AppendLine($"{indent}var {childEntityVariableName}EntitiesToDelete = db.{property.GenericTypeName}s.Where(x => x.ClientId == {entityVariableName}.Id && !{childEntityVariableName}IdsInInput.Contains(x.Id)).ToList();");
                            codeBuilder.AppendLine($"{indent}db.{property.GenericTypeName}s.RemoveRange({childEntityVariableName}EntitiesToDelete);");
                            codeBuilder.AppendLine($"{indent}// end {property.Name} property");
                            codeBuilder.AppendLine($"");
                        }
                    }
                }

                GenerateEntityProperties_Update(entity, "         ");

                codeBuilder.AppendLine($"      await db.SaveChangesAsync();");
                codeBuilder.AppendLine($"      return {entityVariableName};");
                codeBuilder.AppendLine("   }");

                // TODO: Delete



                codeBuilder.AppendLine("}\n");

            }
            return codeBuilder.ToString();


        }

        private static String RenderInputTypes(List<EntityDescriptor> entitiesTree, StringBuilder codeBuilder = null)
        {
            codeBuilder = codeBuilder ?? new StringBuilder();
            foreach (var entity in entitiesTree)
            {
                string GenerateProperties(bool includePrimarykey, bool primarykeyOnly, string operation)
                {
                    var propertiesBuilder = new StringBuilder();
                    var ignoredBuilder = new StringBuilder();
                    foreach (var property in entity.Properties)
                    {
                        if (property.Classification == PropertyDescriptorClassifications.Regular && !primarykeyOnly)
                        {
                            var typeName = property.TypeName.Replace("?", "");
                            if (typeName == "Int32") typeName = "Int";
                            typeName = $"{typeName}Type";
                            if (property.IsRequired)
                                typeName = $"NonNullType<{typeName}>";
                            propertiesBuilder.AppendLine($"      descriptor.Field(x => x.{property.Name}).Type<{typeName}>();");
                        }
                        else if (property.Classification == PropertyDescriptorClassifications.Children && !primarykeyOnly)
                        {
                            propertiesBuilder.AppendLine($"      descriptor.Field(x => x.{property.Name}).Type<NonNullType<ListType<{operation}{property.Name}InputType>>>();");
                        }
                        else if (property.Classification == PropertyDescriptorClassifications.Pk && includePrimarykey)
                        {
                            propertiesBuilder.AppendLine($"      descriptor.Field(x => x.{property.Name}).Type<NonNullType<IdType>>();");
                        }
                        else if (!primarykeyOnly)
                        {
                            ignoredBuilder.AppendLine($"   // {property.TypeName} {property.Name} - {property.Classification}");
                        }
                    }
                    if (ignoredBuilder.Length > 0)
                    {
                        propertiesBuilder.AppendLine($"   // Ignored: ");
                        propertiesBuilder.Append(ignoredBuilder.ToString());
                    }
                    return propertiesBuilder.ToString();
                }

                // Create
                codeBuilder.AppendLine($"public class Create{entity.Name}InputType : InputObjectType<Create{entity.Name}Input>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"   protected override void Configure(IInputObjectTypeDescriptor<Create{entity.Name}Input> descriptor)");
                codeBuilder.AppendLine("   {");
                codeBuilder.AppendLine("      base.Configure(descriptor);");
                codeBuilder.Append(GenerateProperties(false, false, "Create"));
                codeBuilder.AppendLine("   }");
                codeBuilder.AppendLine("}\n");

                // Update
                codeBuilder.AppendLine($"public class Update{entity.Name}InputType : InputObjectType<Update{entity.Name}Input>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"   protected override void Configure(IInputObjectTypeDescriptor<Update{entity.Name}Input> descriptor)");
                codeBuilder.AppendLine("   {");
                codeBuilder.AppendLine("      base.Configure(descriptor);");
                codeBuilder.Append(GenerateProperties(true, false, "Update"));
                codeBuilder.AppendLine("   }");
                codeBuilder.AppendLine("}\n");

                // Delete
                codeBuilder.AppendLine($"public class Delete{entity.Name}InputType : InputObjectType<Delete{entity.Name}Input>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"   protected override void Configure(IInputObjectTypeDescriptor<Delete{entity.Name}Input> descriptor)");
                codeBuilder.AppendLine("   {");
                codeBuilder.AppendLine("      base.Configure(descriptor);");
                codeBuilder.Append(GenerateProperties(true, true, "Delete"));
                codeBuilder.AppendLine("   }");
                codeBuilder.AppendLine("}\n");

                // Recurse
                if (entity.Children.Any())
                {
                    RenderInputTypes(entity.Children, codeBuilder);
                }
            }
            return codeBuilder.ToString();

        }

        private static String RenderInputValidators(List<EntityDescriptor> entitiesTree, StringBuilder codeBuilder = null)
        {
            codeBuilder = codeBuilder ?? new StringBuilder();
            foreach (var entity in entitiesTree)
            {
                static String GenerateRules(EntityDescriptor entity, string operation, bool includePrimarykey, bool primarykeyOnly)
                {
                    var ignoredBuilder = new StringBuilder();
                    var rulesBuilder = new StringBuilder();
                    foreach (var property in entity.Properties)
                    {
                        if (property.Classification == PropertyDescriptorClassifications.Regular && !primarykeyOnly)
                        {
                            if (property.IsRequired || (property.TypeName == "String" && property.StringLength > 0))
                            {
                                rulesBuilder.Append($"      RuleFor(m => m.{property.Name})");
                                if (property.IsRequired)
                                    rulesBuilder.Append($".NotEmpty()");
                                if (property.TypeName == "String" && property.StringLength > 0)
                                    rulesBuilder.Append($".Length({(property.IsRequired ? "1" : "0")}, {property.StringLength})");
                                rulesBuilder.AppendLine($";");
                            }
                            else
                            {
                                ignoredBuilder.AppendLine($"   // {property.TypeName} {property.Name} - not required nor has string length");
                            }
                        }
                        else if (property.Classification == PropertyDescriptorClassifications.Pk && includePrimarykey)
                        {
                            rulesBuilder.AppendLine($"      RuleFor(m => m.{property.Name}).NotEmpty();");
                        }
                        else if (property.Classification == PropertyDescriptorClassifications.Children && !primarykeyOnly)
                        {
                            rulesBuilder.AppendLine($"      RuleForEach(m => m.{property.Name}).SetValidator(new {operation}{property.GenericTypeName}InputValidator());");
                        }
                        else if (!primarykeyOnly)
                        {
                            ignoredBuilder.AppendLine($"      // {property.TypeName} {property.Name} - {property.Classification}");
                        }
                    }

                    if (ignoredBuilder.Length > 0)
                    {
                        rulesBuilder.AppendLine($"      // Ignored: ");
                        rulesBuilder.Append(ignoredBuilder.ToString());
                    }

                    return rulesBuilder.ToString();
                }


                // Create
                codeBuilder.AppendLine($"public class Create{entity.Name}InputValidator : AbstractValidator<Create{entity.Name}Input>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"   public Create{entity.Name}InputValidator()");
                codeBuilder.AppendLine("   {");
                codeBuilder.Append(GenerateRules(entity, "Create", false, false));
                codeBuilder.AppendLine("   }\n");
                codeBuilder.AppendLine("}\n");

                // Update
                codeBuilder.AppendLine($"public class Update{entity.Name}InputValidator : AbstractValidator<Update{entity.Name}Input>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"  public Update{entity.Name}InputValidator()");
                codeBuilder.AppendLine("    {");
                codeBuilder.Append(GenerateRules(entity, "Update", true, false));
                codeBuilder.AppendLine("   }\n");
                codeBuilder.AppendLine("}\n");

                // Delete
                codeBuilder.AppendLine($"public class Delete{entity.Name}InputValidator : AbstractValidator<Delete{entity.Name}Input>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"   public Delete{entity.Name}InputValidator()");
                codeBuilder.AppendLine("   {");
                codeBuilder.Append(GenerateRules(entity, "Delete", true, true));
                codeBuilder.AppendLine("   }\n");
                codeBuilder.AppendLine("}\n");

                // Recurse
                if (entity.Children.Any())
                {
                    RenderInputValidators(entity.Children, codeBuilder);
                }
            }
            return codeBuilder.ToString();
        }

        private static String RenderInputs(List<EntityDescriptor> entitiesTree, StringBuilder codeBuilder = null)
        {
            codeBuilder = codeBuilder ?? new StringBuilder();
            foreach (var entity in entitiesTree)
            {
                string GenerateProperties(bool includePrimarykey, bool primarykeyOnly)
                {
                    var ignoredBuilder = new StringBuilder();
                    var propertiesBuilder = new StringBuilder();
                    foreach (var property in entity.Properties)
                    {
                        if (!primarykeyOnly
                            && (property.Classification == PropertyDescriptorClassifications.Regular
                                || property.Classification == PropertyDescriptorClassifications.Children))
                        {
                            propertiesBuilder.AppendLine($"   public {property.TypeName} {property.Name} {{ get; set; }}");
                        }
                        else if (includePrimarykey && property.Classification == PropertyDescriptorClassifications.Pk)
                        {
                            propertiesBuilder.AppendLine($"   public {property.TypeName} {property.Name} {{ get; set; }}");
                        }
                        else if (!primarykeyOnly)
                        {
                            ignoredBuilder.AppendLine($"   // {property.TypeName} {property.Name} - {property.Classification}");
                        }
                    }

                    if (ignoredBuilder.Length > 0)
                    {
                        propertiesBuilder.AppendLine($"   // Ignored: ");
                        propertiesBuilder.Append(ignoredBuilder.ToString());
                    }

                    return propertiesBuilder.ToString();
                }

                // Create
                codeBuilder.AppendLine($"public class Create{entity.Name}Input");
                codeBuilder.AppendLine("{");
                codeBuilder.Append(GenerateProperties(false, false));
                codeBuilder.AppendLine("}\n");

                // Update
                codeBuilder.AppendLine($"public class Update{entity.Name}Input");
                codeBuilder.AppendLine("{");
                codeBuilder.Append(GenerateProperties(true, false));
                codeBuilder.AppendLine("}\n");

                // Delete
                codeBuilder.AppendLine($"public class Delete{entity.Name}Input");
                codeBuilder.AppendLine("{");
                codeBuilder.Append(GenerateProperties(true, true));
                codeBuilder.AppendLine("}\n");

                // Recurse
                if (entity.Children.Any())
                {
                    RenderInputs(entity.Children, codeBuilder);
                }
            }
            return codeBuilder.ToString();


        }


        private static EntityDescriptor FindEntityDescriptorByName(List<EntityDescriptor> entitiesTree, string name)
        {
            if (entitiesTree.Count == 0) return null;
            var result = entitiesTree.FirstOrDefault(e => e.Name == name);
            if (result != null) return result;
            foreach (var entity in entitiesTree)
            {
                result = FindEntityDescriptorByName(entity.Children, name);
                if (result != null) return result;
            }
            return null;
        }

        private static String RenderTree(List<EntityDescriptor> entitiesTree, StringBuilder codeBuilder = null, int level = 0)
        {
            var indentStr = new string(' ', level * 3);
            codeBuilder = codeBuilder ?? new StringBuilder();
            foreach (var entity in entitiesTree)
            {
                codeBuilder.AppendLine($"{indentStr}* {entity.Name}:");
                foreach (var property in entity.Properties)
                {
                    codeBuilder.AppendLine($"{indentStr}   .{property.Name} : {property.TypeName} {(property.StringLength > 0 ? $"({property.StringLength})" : "")} {(property.IsRequired ? "Required" : "")} [{property.Classification.ToString()}]");
                }
                if (entity.Children.Any())
                {
                    RenderTree(entity.Children, codeBuilder, level + 1);
                }
            }
            return codeBuilder.ToString();
        }

        private static List<EntityDescriptor> BuildEntityTree()
        {
            var entitiesTree = new List<EntityDescriptor>();

            var dataAssembly = typeof(Client).Assembly;
            var allTypes = dataAssembly.GetTypes();
            var entityTypes = allTypes.Where(t => t.Namespace == "IS4UI.Backend.Data.Entities");
            var entityTypesFullNames = entityTypes.Select(t => t.FullName).ToArray();

            foreach (var entityType in entityTypes)
            {
                var entityDescriptor = new EntityDescriptor()
                {
                    Name = entityType.Name
                };
                var properties = entityType.GetProperties().OrderBy(p => p.Name);
                var parentNames = properties.Where(p => p.PropertyType.IsClass).Select(p => p.Name);

                foreach (var property in properties)
                {
                    var type = property.PropertyType;
                    var propertyDescriptor = new PropertyDescriptor()
                    {
                        Name = property.Name,
                        IsRequired = type.IsPrimitive
                    };

                    if (type.Name == "Nullable`1")
                    {
                        var genericType = type?.GetTypeInfo()?.GenericTypeArguments.FirstOrDefault();
                        if (genericType != null)
                        {
                            propertyDescriptor.TypeName = genericType.Name + "?";
                            propertyDescriptor.IsRequired = false;
                        }
                        else
                            throw new Exception($"Failed to get type name for nullable property type: {entityType.Name}.{property.Name}: {type.Name}");
                    }
                    else if (type.GetInterface(nameof(IEnumerable)) != null && type != typeof(string))
                    {
                        var genericType = type?.GetTypeInfo()?.GenericTypeArguments.FirstOrDefault();
                        if (genericType != null)
                        {
                            propertyDescriptor.TypeName = $"List<{genericType.Name}>";
                            propertyDescriptor.GenericTypeName = genericType.Name;
                        }
                        else
                            throw new Exception($"Failed to get type name for collection property type: {entityType.Name}.{property.Name}: {type.Name}");
                    }
                    else
                    {
                        propertyDescriptor.TypeName = type.Name;
                        if (type.Name == "String")
                        {
                            var stringLength = property.GetCustomAttribute<StringLengthAttribute>()?.MaximumLength;
                            propertyDescriptor.StringLength = stringLength ?? 0;
                            var isRequired = property.GetCustomAttribute<RequiredAttribute>() != null;
                            propertyDescriptor.IsRequired = isRequired;
                        }
                    }

                    if (parentNames.Select(n => n + "Id").Contains(property.Name))
                    {
                        propertyDescriptor.Classification = PropertyDescriptorClassifications.ParentId;
                    }
                    else if (property.Name == "Id")
                    {
                        propertyDescriptor.Classification = PropertyDescriptorClassifications.Pk;

                    }
                    else if (property.Name == "Created" && (type == typeof(DateTime) || type == typeof(DateTime?)))
                    {
                        propertyDescriptor.Classification = PropertyDescriptorClassifications.Created;
                    }
                    else if (property.Name == "Updated" && (type == typeof(DateTime) || type == typeof(DateTime?)))
                    {
                        propertyDescriptor.Classification = PropertyDescriptorClassifications.Updated;
                    }
                    else if (type.IsClass && type != typeof(string))
                    {
                        propertyDescriptor.Classification = PropertyDescriptorClassifications.ParentRef;
                    }
                    else if (type.GetInterface(nameof(IEnumerable)) != null && type != typeof(string))
                    {
                        propertyDescriptor.Classification = PropertyDescriptorClassifications.Children;
                    }
                    else
                    {
                        propertyDescriptor.Classification = PropertyDescriptorClassifications.Regular;
                    }
                    entityDescriptor.Properties.Add(propertyDescriptor);
                }
                entitiesTree.Add(entityDescriptor);
            }

            // Move child entities into parents
            var childEntities = entitiesTree.Where(e => e.Properties.Any(p => p.Classification == PropertyDescriptorClassifications.ParentRef)).ToList();
            foreach (var entity in childEntities)
            {
                var parentRefName = entity.Properties.First(p => p.Classification == PropertyDescriptorClassifications.ParentRef).Name;
                var parentEntity = FindEntityDescriptorByName(entitiesTree, parentRefName);
                if (parentEntity == null)
                    throw new Exception($"Can't find entity {parentRefName} to be parent of {entity.Name}.");
                parentEntity.Children.Add(entity);
                entitiesTree.Remove(entity);
            }

            return entitiesTree;
        }
    }
}
