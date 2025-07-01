using System;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services;

public interface IApplicationService
{
    Task<ApplicationResponse> CreateApplicationAsync(ApplicationRequest request);

    Task<ApplicationResponse?> UpdateApplicationAsync(int id, ApplicationRequest request);

    Task<IEnumerable<ApplicationResponse>> GetApplicationsAsync();

    Task<ApplicationResponse?> GetApplicationAsync(int id);

    Task<IEnumerable<SwitchResponse>> GetSwitchesAsync(int id);
}
