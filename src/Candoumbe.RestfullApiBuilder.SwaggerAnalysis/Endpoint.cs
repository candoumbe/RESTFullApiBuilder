using System.Collections.Generic;

namespace Candoumbe.RestfullApiBuilder.SwaggerAnalysis
{
    public class Endpoint
    {
        public Endpoint(string route, string method)
        {
            Route = route;
            Method = method;
        }

        /// <summary>
        /// Description of the endpoint
        /// </summary>
        public string Description { get; internal set; }

        public bool Deprecated { get; internal set; }

        public string Route { get; }

        /// <summary>
        /// Name of the HTTP verb to use 
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// Set of responses
        /// </summary>
        public IReadOnlyList<Response> Responses { get; internal set; }

        /// <summary>
        /// Sets of tag
        /// </summary>
        public IReadOnlyList<string> Tags { get; internal set; }
    }

    public record RouteParameter
}
