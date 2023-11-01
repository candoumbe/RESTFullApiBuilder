using System.Runtime.CompilerServices;

using VerifyTests;
using VerifyTests.DiffPlex;

namespace Candoumbe.RestFullApiBuilder.SwaggerAnalysis.UnitTests;

public static class VerifyInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifyDiffPlex.Initialize(OutputType.Compact);
        VerifierSettings.UseSplitModeForUniqueDirectory();
    }
}
