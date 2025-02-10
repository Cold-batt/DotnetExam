using Itis.DotnetExam.Api.Web.Models;
using Itis.DotnetExam.Api.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Itis.DotnetExam.Api.Web.Configurators;

/// <summary>
/// Конфигуратор политик
/// </summary>
public static class PolicyConfigurator
{
    /// <summary>
    /// Добавить и настроить политики
    /// </summary>
    /// <param name="opt">AuthorizationOptions</param>
    public static void PolicyConfigure(this AuthorizationOptions opt)
    {
        opt.ApplyPolicy(
            new PolicyModel(PolicyConstants.IsAdministrator)
                .AddRoles(
                    Roles.Administrator),
            
            new PolicyModel(PolicyConstants.IsDefaultUser)
                .AddRoles(
                    Roles.Administrator,
                    Roles.User)
            );
    }

    private static void ApplyPolicy(
        this AuthorizationOptions opt,
        params PolicyModel[] items)
    {
        foreach (var item in items)
        {
            opt.AddPolicy(item.Policy, builder =>
            {
                builder.RequireAssertion(
                    x =>
                        item.Roles.Any(y => x.User.IsInRole(y))
                );
            });
        }
    }
}