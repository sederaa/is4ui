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
            public PropertyDescriptorClassifications Classification { get; set; }
        }



        public void OnGet()
        {
            List<EntityDescriptor> entitiesTree = BuildEntityTree();

            //var tree = RenderTree(entitiesTree);
            // Code = tree;

            var codeBuilder = new StringBuilder();

            codeBuilder.AppendLine("////////////// INPUTS //////////////");
            codeBuilder.AppendLine("// Add to Mutation\\Inputs folder.\n");

            RenderInputs(entitiesTree, codeBuilder);

            Code = codeBuilder.ToString();

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
                    codeBuilder.AppendLine($"{indentStr}   .{property.Name} : {property.TypeName} ({property.Classification.ToString()})");
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
                    var propertyDescriptor = new PropertyDescriptor()
                    {
                        Name = property.Name
                    };

                    var type = property.PropertyType;
                    if (type.Name == "Nullable`1")
                    {
                        var genericType = type?.GetTypeInfo()?.GenericTypeArguments.FirstOrDefault();
                        if (genericType != null)
                            propertyDescriptor.TypeName = genericType.Name + "?";
                        else
                            throw new Exception($"Failed to get type name for nullable property type: {entityType.Name}.{property.Name}: {type.Name}");
                    }
                    else if (type.GetInterface(nameof(IEnumerable)) != null && type != typeof(string))
                    {
                        var genericType = type?.GetTypeInfo()?.GenericTypeArguments.FirstOrDefault();
                        if (genericType != null)
                            propertyDescriptor.TypeName = $"List<{genericType.Name}>";
                        else
                            throw new Exception($"Failed to get type name for collection property type: {entityType.Name}.{property.Name}: {type.Name}");
                    }
                    else
                    {
                        propertyDescriptor.TypeName = type.Name;
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
