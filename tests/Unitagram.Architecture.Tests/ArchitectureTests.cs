using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Unitagram.Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Unitagram.Domain";
    private const string ApplicationNamespace = "Unitagram.Application";
    private const string WebAPINamespace = "Unitagram.WebAPI";
    private const string InfrastructureNamespace = "Unitagram.Infrastructure";
    private const string PersistenceNamespace = "Unitagram.Persistence";
    
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            ApplicationNamespace,
            WebAPINamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            WebAPINamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            WebAPINamespace,
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Persistence_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Persistence.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            WebAPINamespace,
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void WebAPI_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(WebAPI.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}