using Candoumbe.Forms;

namespace RestFullApiBuilder.ResourceAnalysis;

/// <summary>
/// 
/// </summary>
public record ApiRepresentation
{
    public string ResourceName { get; init; }

    public string Description { get; init; }

    public IEnumerable<Form> Forms { get; init; }
}

