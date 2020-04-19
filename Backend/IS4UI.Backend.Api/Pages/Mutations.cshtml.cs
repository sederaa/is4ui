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

            codeBuilder.AppendLine("////////////// INPUT VALIDATORS //////////////");
            codeBuilder.AppendLine("// Add to Mutation\\Validators folder.\n");

            RenderInputValidators(entitiesTree, codeBuilder);

            Code = codeBuilder.ToString();

        }

        private static String RenderInputValidators(List<EntityDescriptor> entitiesTree, StringBuilder codeBuilder = null)
        {
            codeBuilder = codeBuilder ?? new StringBuilder();
            foreach (var entity in entitiesTree)
            {
                static String GenerateRules(EntityDescriptor entity, string verb)
                {
                    var ignoredBuilder = new StringBuilder();
                    var rulesBuilder = new StringBuilder();
                    foreach (var property in entity.Properties)
                    {
                        if (property.Classification == PropertyDescriptorClassifications.Regular)
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
                        else if (property.Classification == PropertyDescriptorClassifications.Children)
                        {
                            rulesBuilder.AppendLine($"      RuleForEach(m => m.{property.Name}).SetValidator(new {verb}{property.GenericTypeName}InputValidator());");
                        }
                        else
                        {
                            ignoredBuilder.AppendLine($"   // {property.TypeName} {property.Name} - {property.Classification}");
                        }
                    }

                    if (ignoredBuilder.Length > 0)
                    {
                        rulesBuilder.AppendLine($"   // Ignored: ");
                        rulesBuilder.Append(ignoredBuilder.ToString());
                    }

                    return rulesBuilder.ToString();
                }


                // Create
                codeBuilder.AppendLine($"public class Create{entity.Name}InputValidator : AbstractValidator<Create{entity.Name}Input>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"   public Create{entity.Name}InputValidator()");
                codeBuilder.AppendLine("    {");
                codeBuilder.Append(GenerateRules(entity, "Create"));
                codeBuilder.AppendLine("    }\n");
                codeBuilder.AppendLine("}\n");

                // Update
                codeBuilder.AppendLine($"public class Update{entity.Name}InputValidator : AbstractValidator<Update{entity.Name}Input>");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"   public Update{entity.Name}InputValidator()");
                codeBuilder.AppendLine("    {");
                codeBuilder.Append(GenerateRules(entity, "Update"));
                codeBuilder.AppendLine("    }\n");
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
                var ignoredBuilder = new StringBuilder();
                var propertiesBuilder = new StringBuilder();
                foreach (var property in entity.Properties)
                {
                    if (property.Classification == PropertyDescriptorClassifications.Regular
                        || property.Classification == PropertyDescriptorClassifications.Children)
                    {
                        propertiesBuilder.AppendLine($"   public {property.TypeName} {property.Name} {{ get; set; }}");
                    }
                    else
                    {
                        ignoredBuilder.AppendLine($"   // {property.TypeName} {property.Name} - {property.Classification}");
                    }
                }

                if (ignoredBuilder.Length > 0)
                {
                    propertiesBuilder.AppendLine($"   // Ignored: ");
                    propertiesBuilder.Append(ignoredBuilder.ToString());
                }

                // Create
                codeBuilder.AppendLine($"public class Create{entity.Name}Input");
                codeBuilder.AppendLine("{");
                codeBuilder.Append(propertiesBuilder.ToString());
                codeBuilder.AppendLine("}\n");

                // Update
                codeBuilder.AppendLine($"public class Update{entity.Name}Input");
                codeBuilder.AppendLine("{");
                codeBuilder.Append(propertiesBuilder.ToString());
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
