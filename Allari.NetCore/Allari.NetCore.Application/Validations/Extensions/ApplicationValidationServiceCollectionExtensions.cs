using System.Reflection;
using Allari.NetCore.Application.Validations.Middleware;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Allari.NetCore.Application.Validations.Extensions;

public static class ApplicationValidationServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRApplicationValidation(
        this IServiceCollection services,
        params Assembly[] validatorsAssembly)
    {
        services.TryAddScoped<ValidationExceptionHandlingMiddleware>();
        services.AddFluentValidation((IEnumerable<Assembly>) validatorsAssembly, ServiceLifetime.Scoped);
        return services;
    }
}