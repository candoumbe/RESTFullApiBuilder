using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Readers.Interface;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Candoumbe.RestfullApiBuilder.SwaggerAnalysis
{
    public static class SwaggerParser
    {
        public const string UriSegmentSeparator = "/";

        public static IReadOnlyList<Feature> Parse(string input)
        {
            IOpenApiReader<string, OpenApiDiagnostic> reader = new OpenApiStringReader();

            OpenApiDocument document = reader.Read(input, out OpenApiDiagnostic diagnostic);

            Dictionary<string, Feature> resources = new Dictionary<string, Feature>(document.Tags.Count + 1);

            foreach (KeyValuePair<string, OpenApiPathItem> item in document.Paths)
            {
                string path = item.Key;

                Endpoint[] endpoints = item.Value.Operations.Select(op => new Endpoint(route: path, method: op.Key.ToString())
                {
                    Tags = op.Value.Tags.Select(tag => tag.Name).ToArray(),
                    Description = op.Value.Description,
                    Deprecated = op.Value.Deprecated,
                    Responses = op.Value.Responses.Select(response => new Response
                    {
                        StatusCode = int.Parse(response.Key),
                        Description = response.Value.Description
                    })
                    .ToArray()
                })
                .ToArray();

                string resourceName = path?.Trim('/')?.ToPascalCase() ?? string.Empty;

                if (!resources.TryGetValue(resourceName, out Feature resource))
                {
                    resource = new Feature(resourceName);
                    resources.Add(resourceName, resource);
                }
                resource.AddEndpoints(endpoints);

            }

            return resources.Values.ToArray();
        }
    }
}
