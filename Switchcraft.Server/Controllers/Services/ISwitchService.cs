using System;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services;

public interface ISwitchService
{
    Task<SwitchResponse> CreateSwitchAsync(SwitchRequest request);

    Task<SwitchResponse?> UpdateSwitchAsync(int id, SwitchRequest request);

    Task<IEnumerable<SwitchResponse>> GetSwitchesAsync();

    Task<SwitchResponse?> GetSwitchAsync(int id);
}
