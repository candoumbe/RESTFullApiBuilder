using System;
using System.Collections.Generic;
using System.Linq;

namespace Candoumbe.RestfullApiBuilder.SwaggerAnalysis
{
    /// <summary>
    /// A set of endpoints
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Name of the feature
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Description of the feature
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Endpoints of the feature
        /// </summary>
        public IReadOnlyList<Endpoint> Endpoints => _endpoints.ToArray();

        private readonly List<Endpoint> _endpoints;

        public Feature(string name)
        {
            Name = name;
            _endpoints = new();
        }

        /// <summary>
        /// Adds <paramref name="endpoints"/> for the specified features.
        /// </summary>
        /// <param name="endpoints"></param>
        public void AddEndpoints(IEnumerable<Endpoint> endpoints)
        {
            endpoints ??= Array.Empty<Endpoint>();

            _endpoints.AddRange(endpoints.Distinct());
        }
    }
}
