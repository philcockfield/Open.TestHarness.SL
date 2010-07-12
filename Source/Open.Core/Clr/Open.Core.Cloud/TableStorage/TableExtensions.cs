using System;
using System.Data.Services.Client;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.TableStorage.CodeGeneration;
using Open.Core.Common;

namespace Open.Core.Cloud.TableStorage
{
    public static class TableExtensions
    {
        /// <summary>Gets the cloud storate acount.</summary>
        /// <param name="settings">The current cloud settings.</param>
        public static CloudStorageAccount GetStorageAccount(this ICloudSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            return CloudStorageAccount.Parse(settings.DataConnectionString);
        }

        /// <summary>Creates an instance of the CloudTableClient using the connection string in the CloudSettings.</summary>
        /// <param name="settings">The current cloud settings.</param>
        public static CloudTableClient CreateTableClient(this ICloudSettings settings)
        {
            return settings.GetStorageAccount().CreateCloudTableClient();
        }

        /// <summary>Retrieves all the types decorated with the [PersistClass] attribute within the given assembly.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        public static IEnumerable<Type> GetTableModelTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            return assembly.GetTypes().Where(m => m.GetPersistAttribute() != null);
        }

        /// <summary>Retrieves the [PersistClass] attribute from the given type.</summary>
        /// <param name="type">The type to examine.</param>
        /// <returns>The attribute, or null if the class is not decorated with the [PersistClass] attribute.</returns>
        public static PersistClassAttribute GetPersistAttribute(this Type type)
        {
            return type.GetCustomAttributes(typeof(PersistClassAttribute), true).FirstOrDefault() as PersistClassAttribute;
        }

        /// <summary>Retrieves the [PersistProperty] attribute from the given property.</summary>
        /// <param name="property">The property to examine.</param>
        /// <returns>The attribute, or null if the property is not decorated with the [PersistProperty] attribute.</returns>
        public static PersistPropertyAttribute GetPersistAttribute(this PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(PersistPropertyAttribute), true).FirstOrDefault() as PersistPropertyAttribute;
        }

        /// <summary>Generates all Table Entity code for models defined with [PersistClass] in the given assembly.</summary>
        /// <param name="assembly">The assembly containing the models.</param>
        /// <param name="outputFolder">The folder to write to.</param>
        /// <param name="fileName">The name of the output file.</param>
        public static void GenerateCode(this Assembly assembly, string outputFolder, string fileName = "TableEntities.g.cs")
        {
            var generator = new TableEntitiesTemplate();
            generator.ModelTypes.Add(assembly);
            var code = generator.TransformText();
            code.WriteToProjectFile(outputFolder, fileName);
        }


        /// <summary>Adds a Where clause to the query matching the given partition/row keys.</summary>
        /// <typeparam name="T">The type of entity being queryied</typeparam>
        /// <param name="query">The starting query.</param>
        /// <param name="keyQueryType">Flag indicating how to perform the matching.</param>
        /// <param name="partitionKey">The partition key to look for.</param>
        /// <param name="rowKey">The row key to look for.</param>
        public static IQueryable<T> WhereKeysMatch<T>(
                                                    this DataServiceQuery<T> query, 
                                                    KeyQueryType keyQueryType, 
                                                    string partitionKey,
                                                    string rowKey) where T : ITableServiceEntity
        {
            switch (keyQueryType)
            {
                case KeyQueryType.Literal: return query.Where(m => m.PartitionKey == partitionKey && m.RowKey == rowKey);
                case KeyQueryType.StartsWith: 
                    return query.Where(m => 
                        m.PartitionKey.CompareTo(partitionKey) >= 0 &&
                        m.RowKey.CompareTo(rowKey) >= 0);

                default: throw new NotSupportedException(keyQueryType.ToString());
            }
        }

    }
}
