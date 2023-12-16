# SARIF export for Cake.Issues Addin

This addin for the Cake Build Automation System allows you to create [SARIF](https://sarifweb.azurewebsites.net/) compatible
files from issues found using any code analyzer or linter using the [Cake Issues addin](https://github.com/cake-contrib/Cake.Issues).

For more information about this add-in see the [Cake.Issues website](https://cakeissues.net)
and for general information about the Cake build automation system see the [Cake website](http://cakebuild.net)

[![License](http://img.shields.io/:license-mit-blue.svg)](https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif/blob/feature/build/LICENSE)

## Information

| | Stable | Pre-release |
|:--:|:--:|:--:|
|GitHub Release|-|[![GitHub release](https://img.shields.io/github/release/cake-contrib/Cake.Issues.Reporting.Sarif.svg)](https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif/releases/latest)|
|NuGet|[![NuGet](https://img.shields.io/nuget/v/Cake.Issues.Reporting.Sarif.svg)](https://www.nuget.org/packages/Cake.Issues.Reporting.Sarif)|[![NuGet](https://img.shields.io/nuget/vpre/Cake.Issues.Reporting.Sarif.svg)](https://www.nuget.org/packages/Cake.Issues.Reporting.Sarif)|

## Build & Test Status

| | Develop | Master |
|:--:|:--:|:--:|
|AppVeyor Windows|[![Build status](https://ci.appveyor.com/api/projects/status/a5t3j7p51r581jk0/branch/develop?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-issues-reporting-sarif/branch/develop)|[![Build status](https://ci.appveyor.com/api/projects/status/a5t3j7p51r581jk0/branch/master?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-issues-reporting-sarif/branch/master)|
|Azure DevOps Windows|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Build)](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Build)](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Tests Windows Server 2019 (.NET tool)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20Windows%20Server%202019%20(.NET%20tool))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20Windows%20Server%202019%20(.NET%20tool))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Tests Windows Server 2019 (Frosting .NET 6)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20Windows%20Server%202019%20(Frosting%20.NET%206))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20Windows%20Server%202019%20(Frosting%20.NET%206))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Tests Windows Server 2022 (Frosting .NET 7)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20Windows%20Server%202022%20(Frosting%20.NET%207))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20Windows%20Server%202022%20(Frosting%20.NET%207))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Tests macOS 11 (.NET tool)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20macOS%2011%20(.NET%20tool))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20macOS%2011%20(.NET%20tool))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Tests macOS 11 (Frosting .NET 6)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20macOS%2011%20(Frosting%20.NET%206))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20macOS%2011%20(Frosting%20.NET%206))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Tests macOS 13 (Frosting .NET 7)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20macOS%2013%20(Frosting%20.NET%207))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20macOS%2013%20(Frosting%20.NET%207))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Ubuntu 20.04 (.NET tool)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20Ubuntu%2020.04%20(.NET%20tool))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20Ubuntu%2020.04%20(.NET%20tool))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Ubuntu 20.04 (Frosting .NET 6)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20Ubuntu%2020.04%20(Frosting%20.NET%206))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20Ubuntu%2020.04%20(Frosting%20.NET%206))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|
|Integration Ubuntu 22.04 (Frosting .NET 7)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=develop&jobName=Integration%20Tests%20Ubuntu%2022.04%20(Frosting%20.NET%207))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_apis/build/status/cake-contrib.Cake.Issues.Reporting.Sarif?branchName=master&jobName=Integration%20Tests%20Ubuntu%2022.04%20(Frosting%20.NET%207))](https://dev.azure.com/cake-contrib/Cake.Issues.Reporting.Sarif/_build/latest?definitionId=32&branchName=master)|

## Code Coverage

[![Coverage Status](https://coveralls.io/repos/github/cake-contrib/Cake.Issues.Reporting.Sarif/badge.svg?branch=develop)](https://coveralls.io/github/cake-contrib/Cake.Issues.Reporting.Sarif?branch=develop)

## Quick Links

- [Documentation](https://cakeissues.net)

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Contributing

Contributions are welcome. See [Contribution Guidelines](CONTRIBUTING.md).