using System;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services;

public interface IEnvironmentService
{
    Task<EnvironmentResponse> CreateEnvironmentAsync(EnvironmentRequest request);

    Task<EnvironmentResponse?> UpdateEnvironmentAsync(int id, EnvironmentRequest request);

    Task<IEnumerable<EnvironmentResponse>> GetEnvironmentsAsync();

    Task<EnvironmentResponse?> GetEnvironmentAsync(int id);
}
