#load nuget:?package=Cake.Recipe&version=3.0.1

//*************************************************************************************************
// Settings
//*************************************************************************************************

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context, 
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues.Reporting.Sarif",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues.Reporting.Sarif",
    appVeyorAccountName: "cakecontrib",
    shouldRunCoveralls: false, // Disabled because it's currently failing
    shouldPostToGitter: false, // Disabled because it's currently failing
    shouldGenerateDocumentation: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]* -[Cake.Issues.Testing]* -[Cake.Issues.Reporting]* -[Shouldly]* -[Newtonsoft.Json]* -[DiffEngine]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

//*************************************************************************************************
// Execution
//*************************************************************************************************

Build.RunDotNetCore();
