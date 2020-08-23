namespace Cake.Issues.Reporting.Sarif.Tests
{
    using System;
    using System.Collections.Generic;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Microsoft.CodeAnalysis.Sarif;
    using Newtonsoft.Json;
    using Shouldly;
    using Xunit;

    public sealed class SarifIssueReportGeneratorTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new SarifIssueReportGenerator(
                        null,
                        new SarifIssueReportFormatSettings()));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new SarifIssueReportGenerator(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheInternalCreateReportMethod
        {
            [Fact]
            public void Should_Generate_Report()
            {
                // Given
                var fixture = new SarifIssueReportFixture();
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 10)
                            .InProjectFile(@"src\Cake.Issues.Reporting.Sarif.Tests\Cake.Issues.Reporting.Sarif.Tests.csproj")
                            .OfRule("Rule Foo")
                            .WithPriority(IssuePriority.Error)
                            .Create(),

                        // Issue to exercise creation of rule metadata with helpUri, and messages
                        // in Markdown format.
                        IssueBuilder
                            .NewIssue("Message Bar.", "ProviderType Bar", "ProviderName Bar")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 12, 5)
                            .OfRule("Rule Bar", new Uri("https://www.example.come/rules/bar.html"))
                            .WithPriority(IssuePriority.Warning)
                            .WithMessageInMarkdownFormat("Message Bar -- now in **Markdown**!")
                            .Create(),

                        // Issue to exercise the corner case where ruleId is absent (so no rule
                        // metadata is created) but helpUri is present (so it is stored in the
                        // result's property bag.
                        IssueBuilder
                            .NewIssue("Message Bar 2.", "ProviderType Bar", "ProviderName Bar")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 23, 42, 5, 10)
                            .OfRule(null, new Uri("https://www.example.come/rules/bar2.html"))
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                    };

                // When
                var logContents = fixture.CreateReport(issues);

                // Then
                var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

                // There are two runs because there are two issue providers.
                sarifLog.Runs.Count.ShouldBe(2);

                Run run = sarifLog.Runs[0];
                run.Tool.Driver.Name.ShouldBe("ProviderType Foo");

                // This run doesn't have any rules that specify a help URI, so we didn't bother
                // adding rule metadata.
                run.Tool.Driver.Rules.ShouldBeNull();

                run.Results.Count.ShouldBe(1);

                Result result = run.Results[0];
                result.RuleId.ShouldBe("Rule Foo");
                result.RuleIndex.ShouldBe(-1); // because there's no rule metadata to point to.
                result.Message.Text.ShouldBe("Message Foo.");
                result.Message.Markdown.ShouldBeNull();
                result.Level.ShouldBe(FailureLevel.Error);
                result.Kind.ShouldBe(ResultKind.Fail);

                result.Locations.Count.ShouldBe(1);
                PhysicalLocation physicalLocation = result.Locations[0].PhysicalLocation;
                physicalLocation.ArtifactLocation.Uri.OriginalString.ShouldBe("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs");
                physicalLocation.Region.StartLine.ShouldBe(10);

                run.OriginalUriBaseIds.Count.ShouldBe(1);
                run.OriginalUriBaseIds[SarifIssueReportGenerator.RepoRootUriBaseId].Uri.LocalPath.ShouldBe(SarifIssueReportFixture.RepositoryRootPath);

                run = sarifLog.Runs[1];
                run.Tool.Driver.Name.ShouldBe("ProviderType Bar");

                // This run has a rule that specifies a help URI, so we added rule metadata.
                IList<ReportingDescriptor> rules = run.Tool.Driver.Rules;
                rules.Count.ShouldBe(1);

                ReportingDescriptor rule = rules[0];
                rule.Id.ShouldBe("Rule Bar");
                rule.HelpUri.OriginalString.ShouldBe("https://www.example.come/rules/bar.html");

                run.Results.Count.ShouldBe(2);

                result = run.Results[0];
                result.RuleId.ShouldBe("Rule Bar");
                result.RuleIndex.ShouldBe(0); // The index of the metadata for this rule in the rules array.
                result.Message.Text.ShouldBe("Message Bar.");
                result.Message.Markdown.ShouldBe("Message Bar -- now in **Markdown**!");
                result.Level.ShouldBe(FailureLevel.Warning);
                result.Kind.ShouldBe(ResultKind.Fail);

                result.Locations.Count.ShouldBe(1);
                physicalLocation = result.Locations[0].PhysicalLocation;
                physicalLocation.ArtifactLocation.Uri.OriginalString.ShouldBe("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs");
                physicalLocation.Region.StartLine.ShouldBe(12);
                physicalLocation.Region.StartColumn.ShouldBe(5);

                // This run also includes an issue with a rule URL but no rule name, so we'll find
                // the rule URL in the result's property bag.
                result = run.Results[1];
                result.RuleId.ShouldBeNull();
                result.RuleIndex.ShouldBe(-1);
                result.GetProperty(SarifIssueReportGenerator.RuleUrlPropertyName).ShouldBe("https://www.example.come/rules/bar2.html");
                result.Message.Text.ShouldBe("Message Bar 2.");
                result.Level.ShouldBe(FailureLevel.Warning);
                result.Kind.ShouldBe(ResultKind.Fail);

                result.Locations.Count.ShouldBe(1);
                physicalLocation = result.Locations[0].PhysicalLocation;
                physicalLocation.ArtifactLocation.Uri.OriginalString.ShouldBe("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs");
                physicalLocation.Region.StartLine.ShouldBe(23);
                physicalLocation.Region.EndLine.ShouldBe(42);
                physicalLocation.Region.StartColumn.ShouldBe(5);
                physicalLocation.Region.EndColumn.ShouldBe(10);

                run.OriginalUriBaseIds.Count.ShouldBe(1);
                run.OriginalUriBaseIds[SarifIssueReportGenerator.RepoRootUriBaseId].Uri.LocalPath.ShouldBe(SarifIssueReportFixture.RepositoryRootPath);
            }

            [Fact]
            public void Should_Have_Separate_Run_For_Every_Issue_Provider()
            {
                // Given
                var fixture = new SarifIssueReportFixture();
                var providerTypeA = "ProviderTypeA Foo";
                var providerTypeB = "ProviderTypeB Foo";
                var issues =
                     new List<IIssue>
                     {
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeA, "ProviderName Foo")
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeB, "ProviderName Foo")
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeA, "ProviderName Foo")
                            .Create(),
                     };

                // When
                var logContents = fixture.CreateReport(issues);

                // Then
                var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

                sarifLog.Runs.Count.ShouldBe(2);

                var runA = sarifLog.Runs[0];
                runA.Tool.Driver.Name.ShouldBe(providerTypeA);

                var runB = sarifLog.Runs[1];
                runB.Tool.Driver.Name.ShouldBe(providerTypeB);
            }

            [Fact]
            public void Should_Set_Driver_Name()
            {
                // Given
                var fixture = new SarifIssueReportFixture();
                var providerType = "ProviderType Foo";
                var issues =
                     new List<IIssue>
                     {
                        IssueBuilder
                            .NewIssue("Message Foo.", providerType, "ProviderName Foo")
                            .Create(),
                     };

                // When
                var logContents = fixture.CreateReport(issues);

                // Then
                var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

                sarifLog.Runs.Count.ShouldBe(1);
                var run = sarifLog.Runs[0];

                run.Tool.Driver.Name.ShouldBe(providerType);
            }

            [Fact]
            public void Should_Not_Set_Rules_If_No_RuleUrl()
            {
                // If run doesn't have any rules that specify a help URI, we shouldn't bother
                // adding rule metadata.

                // Given
                var fixture = new SarifIssueReportFixture();
                var providerType = "ProviderType Foo";
                var issues =
                     new List<IIssue>
                     {
                        IssueBuilder
                            .NewIssue("Message Foo.", providerType, "ProviderName Foo")
                            .OfRule("Rule Foo")
                            .Create(),
                     };

                // When
                var logContents = fixture.CreateReport(issues);

                // Then
                var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

                sarifLog.Runs.Count.ShouldBe(1);
                var run = sarifLog.Runs[0];

                run.Tool.Driver.Rules.ShouldBeNull();
            }

            [Fact]
            public void Should_Set_Rules_If_One_RuleUrl()
            {
                // Runs which have a  rule that specifies a help URI should have added rule metadata.

                // Given
                var fixture = new SarifIssueReportFixture();
                var ruleId = "Rule Bar";
                var ruleUrl = "https://www.example.come/rules/bar.html";
                var issues =
                     new List<IIssue>
                     {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .OfRule(ruleId, new Uri(ruleUrl))
                            .Create(),
                     };

                // When
                var logContents = fixture.CreateReport(issues);

                // Then
                var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

                sarifLog.Runs.Count.ShouldBe(1);
                var run = sarifLog.Runs[0];

                var rules = run.Tool.Driver.Rules;
                rules.Count.ShouldBe(1);

                var rule = rules[0];
                rule.Id.ShouldBe(ruleId);
                rule.HelpUri.OriginalString.ShouldBe(ruleUrl);
            }

            [Fact]
            public void Should_Set_Rules_If_Some_RuleUrl()
            {
                // Runs which have a  rule that specifies a help URI should have added rule metadata.

                // Given
                var fixture = new SarifIssueReportFixture();
                var ruleId = "Rule Bar";
                var ruleUrl = "https://www.example.come/rules/bar.html";
                var issues =
                     new List<IIssue>
                     {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .OfRule("Rule Foo")
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .OfRule(ruleId, new Uri(ruleUrl))
                            .Create(),
                     };

                // When
                var logContents = fixture.CreateReport(issues);

                // Then
                var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

                sarifLog.Runs.Count.ShouldBe(1);
                var run = sarifLog.Runs[0];

                var rules = run.Tool.Driver.Rules;
                rules.Count.ShouldBe(1);

                var rule = rules[0];
                rule.Id.ShouldBe(ruleId);
                rule.HelpUri.OriginalString.ShouldBe(ruleUrl);
            }

            [Fact]
            public void Should_Set_Rules_If_RuleUrl_Without_RuleName()
            {
                // Run that include an issue with a rule URL but no rule name, should have rule URL in the result's property bag.

                // Given
                var fixture = new SarifIssueReportFixture();
                var ruleUrl = "https://www.example.come/rules/bar.html";
                var issues =
                     new List<IIssue>
                     {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .OfRule(null, new Uri(ruleUrl))
                            .Create(),
                     };

                // When
                var logContents = fixture.CreateReport(issues);

                // Then
                var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

                sarifLog.Runs.Count.ShouldBe(1);
                var run = sarifLog.Runs[0];

                run.Tool.Driver.Rules.ShouldBeNull();

                run.Results.Count.ShouldBe(1);
                var result = run.Results[0];
                result.RuleId.ShouldBeNull();
                result.RuleIndex.ShouldBe(-1);
                result.GetProperty(SarifIssueReportGenerator.RuleUrlPropertyName).ShouldBe(ruleUrl);
            }
        }
    }
}
