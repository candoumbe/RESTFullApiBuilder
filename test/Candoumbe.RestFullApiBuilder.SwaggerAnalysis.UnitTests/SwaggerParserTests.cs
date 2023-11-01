using Candoumbe.RestfullApiBuilder.SwaggerAnalysis;

using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit.Categories;

using static VerifyXunit.Verifier;

namespace Candoumbe.RestFullApiBuilder.SwaggerAnalysis.UnitTests;

[UnitTest]
[UsesVerify]
public class SwaggerParserTests
{
    [Theory]
    [InlineData(OpenApiSpecVersion.OpenApi2_0)]
    [InlineData(OpenApiSpecVersion.OpenApi3_0)]
    public Task Given_api_description_contains_only_one_path_with_one_operation_Then_Parse_should_return_expected_feature(OpenApiSpecVersion version)
    {
        // Arrange
        OpenApiDocument document = new OpenApiDocument()
        {
            Tags = new[] {
                new OpenApiTag{ Name = "Pets" }
            },
            Paths = new OpenApiPaths
            {
                ["/pets"] = new OpenApiPathItem
                {
                    Operations = new Dictionary<OperationType, OpenApiOperation>
                    {
                        [OperationType.Get] = new OpenApiOperation
                        {
                            Description = "Returns all pets from the system that the user has access to",
                            Responses = new OpenApiResponses
                            {
                                ["200"] = new OpenApiResponse
                                {
                                    Description = "OK"
                                }
                            }
                        }
                    }
                }
            }
        };
        string json = document.SerializeAsJson(version);

        // Act
        IReadOnlyList<Feature> features = SwaggerParser.Parse(json);

        // Assert
        return Verify(features)
            .UseParameters(version);
    }

    [Theory]
    [InlineData(OpenApiSpecVersion.OpenApi2_0)]
    [InlineData(OpenApiSpecVersion.OpenApi3_0)]
    public Task Given_api_description_contains_two_paths_with_one_operation_each_Then_Parse_should_return_expected_features(OpenApiSpecVersion version)
    {
        // Arrange
        OpenApiDocument document = new OpenApiDocument()
        {
            Paths = new()
            {
                ["/one"] = new()
                {
                    Description = "Path one",
                    Operations = new Dictionary<OperationType, OpenApiOperation>
                    {
                        [OperationType.Get] = new OpenApiOperation
                        {
                            Description = "Get operations for one",
                            Responses = new OpenApiResponses
                            {
                                ["200"] = new OpenApiResponse
                                {
                                    Description = "OK"
                                }
                            }
                        }
                    },
                },
                ["/two"] = new()
                {
                    Description = "Path two",
                    Operations = new Dictionary<OperationType, OpenApiOperation>
                    {
                        [OperationType.Get] = new OpenApiOperation
                        {
                            Description = "Get operations for two",
                            Responses = new OpenApiResponses
                            {
                                ["200"] = new OpenApiResponse
                                {
                                    Description = "OK"
                                }
                            }
                        }
                    }
                }
            }
        };
        string json = document.SerializeAsJson(version);

        // Act;
        IReadOnlyList<Feature> features = SwaggerParser.Parse(json);

        // Assert
        return Verify(features)
            .UseParameters(version);
    }

    [Theory]
    [InlineData(OpenApiSpecVersion.OpenApi2_0)]
    [InlineData(OpenApiSpecVersion.OpenApi3_0)]
    public Task Given_api_description_contains_one_path_with_two_distinct_operations_Then_Parse_should_return_expected_result(OpenApiSpecVersion version)
    {
        // Arrange
        OpenApiDocument document = new OpenApiDocument()
        {
            Tags = new[]
            {
                new OpenApiTag{ Name = "Pets" }
            },
            Paths = new OpenApiPaths
            {
                ["/pets"] = new OpenApiPathItem
                {
                    Operations = new Dictionary<OperationType, OpenApiOperation>
                    {
                        [OperationType.Get] = new OpenApiOperation
                        {
                            Description = "Returns all pets from the system that the user has access to",
                            Responses = new OpenApiResponses
                            {
                                ["200"] = new OpenApiResponse
                                {
                                    Description = "OK"
                                }
                            }
                        },
                        [OperationType.Delete] = new OpenApiOperation
                        {
                            Description = "Deletes all pets",
                            Responses = new OpenApiResponses
                            {
                                ["204"] = new OpenApiResponse
                                {
                                    Description = "Every pet was deleted"
                                }
                            }
                        }
                    }
                }
            }
        };
        string json = document.SerializeAsJson(version);

        // Act
        IReadOnlyList<Feature> features = SwaggerParser.Parse(json);

        // Assert
        return Verify(features).UseParameters(version);
    }

    [Theory]
    [InlineData(OpenApiSpecVersion.OpenApi2_0)]
    [InlineData(OpenApiSpecVersion.OpenApi3_0)]
    public Task Given_api_description_contains_one_path_with_one_operation_with_parameters_Then_Parse_should_return_expected_result(OpenApiSpecVersion version)
    {
        // Arrange
        OpenApiDocument document = new OpenApiDocument()
        {
            Tags = new[]
            {
                new OpenApiTag{ Name = "One" }
            },
            Paths = new OpenApiPaths
            {
                ["/one/{id}"] = new OpenApiPathItem
                {
                    Operations = new Dictionary<OperationType, OpenApiOperation>
                    {
                        [OperationType.Get] = new OpenApiOperation
                        {
                            Description = "Returns all pets from the system that the user has access to",
                            Parameters = new List<OpenApiParameter>()
                            {
                                new OpenApiParameter
                                {
                                    Name = "id",
                                    In = ParameterLocation.Path
                                }
                            },
                            Responses = new OpenApiResponses
                            {
                                ["200"] = new OpenApiResponse
                                {
                                    Description = "OK"
                                }
                            }
                        }
                    }
                }
            }
        };
        string json = document.SerializeAsJson(version);

        // Act
        IReadOnlyList<Feature> features = SwaggerParser.Parse(json);

        // Assert
        return Verify(features).UseParameters(version);
    }
}
