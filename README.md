# Pencil Durability Kata

This is a code sample written by Dainen Bugh, for submission to Pillar Technology, developed with C# using test-driven development. The requirements can be found at https://github.com/PillarTechnology/kata-pencil-durability.

## Prerequisites

### Build: .NET Core

This project was developed using .NET Core 2.1.302 in Visual Studio Code (VSCode) running on Windows 10. The only requirement to build is the .NET Core SDK, which may be found [here](https://www.microsoft.com/net/download).

### Test: MSTest

Three packages are referenced in the test project as required by MSTest:

* Microsoft.NET.Test.Sdk
* MSTest.TestAdapter
* MSTest.TestFramework

If these are not already installed, they should download automatically when the project is built. If there are any issues with dependencies, please contact the author (see below).

### Repository: Git

If git is not installed, it may be downloaded [here](https://git-scm.com/download/win).


## Obtaining the Source Code

Open the Windows command prompt or VSCode terminal, navigate to a project directory, and enter the following command:

```
git clone https://github.com/DABugh/pillar-pencil-durability.git
```

## Execution

### Building

From the pillar-pencil-durability parent directory, run the following command:

```
dotnet build
```

### Executing

A basic executable has been written for this library. To execute it, navigate to the .../pillar-pencil-durability/PencilDurability/ project directory, and run the following command:

```
dotnet run
```

### Running the tests

To execute the automated unit tests, navigate to the .../pillar-pencil-durability/PencilDurability.Tests/ project directory, and run the following command:

```
dotnet test
```


## Contributors

### Authors

* **Dainen Bugh** (*Dainen.Bugh@gmail.com*) - *Complete source code, except what was auto-generated by VSCode*


### Acknowledgments

This README.md used this [template](https://gist.github.com/PurpleBooth/109311bb0361f32d87a2) as a starting point.
