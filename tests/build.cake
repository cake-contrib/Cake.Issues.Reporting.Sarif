#addin "Cake.Markdownlint"
#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.MsBuild&prerelease"
#addin "Cake.Issues.Markdownlint&prerelease"
#addin "Cake.Issues.InspectCode&prerelease"
#addin "Cake.Issues.Reporting&prerelease"
#addin "Cake.Issues.Reporting.Sarif&prerelease"

#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools"

#load build/build/build.cake
#load build/analyze/analyze.cake
#load build/create-reports/create-reports.cake

var target = Argument("target", "Default");

public class BuildData
{
    public DirectoryPath RepoRootFolder { get; }
    public DirectoryPath TestRootFolder { get; }
    public DirectoryPath SourceFolder { get; }
    public DirectoryPath DocsFolder { get; }
    public DirectoryPath OutputFolder { get; }
    public List<IIssue> Issues { get; }

    public BuildData(ICakeContext context)
    {
        this.TestRootFolder = context.MakeAbsolute(context.Directory("./"));
        this.RepoRootFolder = this.TestRootFolder.Combine("..");
        this.SourceFolder = this.TestRootFolder.Combine("src");
        this.DocsFolder = this.TestRootFolder.Combine("docs");
        this.OutputFolder = this.TestRootFolder.Combine("output");

        this.Issues = new List<IIssue>();
    }
}

Setup<BuildData>(setupContext =>
{
    return new BuildData(setupContext);
});

Task("Default")
    .IsDependentOn("Create-Reports");

RunTarget(target);
