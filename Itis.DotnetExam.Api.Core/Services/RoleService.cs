using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Itis.DotnetExam.Api.Core.Services;

/// <inheritdoc />
public class RoleService: IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="roleManager">Менеджер ролей</param>
    public RoleService(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    /// <inheritdoc />
    public async Task<bool> IsRoleExistAsync(string roleName)
        => await _roleManager.RoleExistsAsync(roleName);

    /// <inheritdoc />
    public async Task<Role?> GetRoleById(Guid roleId)
        => await _roleManager.FindByIdAsync(roleId.ToString());
}