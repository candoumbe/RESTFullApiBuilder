using Candoumbe.Pipelines.Components;

using Nuke.Common;
using Nuke.Common.ProjectModel;

using System.Collections.Generic;
using System.Linq;

public class Build : NukeBuild
    , IHaveSolution
    , IHaveSourceDirectory
    , IClean
    , IRestore
    , ICompile
    , IUnitTest

{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => ((ICompile)x).Compile);

    [Solution]
    [Required]
    public Solution Solution;

    ///<inheritdoc/>
    Solution IHaveSolution.Solution => Solution;

    ///<inheritdoc/>
    IEnumerable<Project> IUnitTest.UnitTestsProjects => Solution.AllProjects.Where(csproj => csproj.Name.EndsWith("UnitTests"));
}
