using System;
using System.Linq;
using Xunit;
using Cafeteria.Domain.Common;

namespace Cafeteria.ArchitectureTests;

public class LayeringTests
{
    [Fact]
    public void Domain_Should_Not_Depend_On_Application_Infrastructure_Api()
    {
        var assembly = typeof(AggregateRoot).Assembly;
        var forbidden = new[] { "Cafeteria.Application", "Cafeteria.Infrastructure", "Cafeteria.Api" };

        var referenced = assembly.GetReferencedAssemblies().Select(a => a.Name).ToArray();

        var violating = forbidden.Intersect(referenced, StringComparer.OrdinalIgnoreCase).ToArray();

        Assert.True(violating.Length == 0, $"Domain assembly has forbidden references: {string.Join(", ", violating)}");
    }
}
