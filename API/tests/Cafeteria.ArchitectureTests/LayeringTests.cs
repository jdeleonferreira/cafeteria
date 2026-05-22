using System;
using System.Linq;
using NetArchTest.Rules;
using Xunit;
using Cafeteria.Domain.Common;

namespace Cafeteria.ArchitectureTests;

public class LayeringTests
{
    [Fact]
    public void Domain_Should_Not_Depend_On_Application_Infrastructure_Api()
    {
        var assembly = typeof(AggregateRoot).Assembly;

        var result = Types.InAssembly(assembly)
            .That()
            .ResideInNamespace("Cafeteria.Domain", true)
            .ShouldNotHaveDependencyOn("Cafeteria.Application")
            .AndShouldNotHaveDependencyOn("Cafeteria.Infrastructure")
            .AndShouldNotHaveDependencyOn("Cafeteria.Api")
            .GetResult();

        Assert.True(result.IsSuccessful, "Types in Domain have forbidden dependencies: " + string.Join(", ", result.FailingTypeNames));
    }
}
