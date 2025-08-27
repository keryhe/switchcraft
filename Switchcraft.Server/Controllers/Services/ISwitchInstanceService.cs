using System;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services;

public interface ISwitchInstanceService
{
    Task<SwitchInstanceResponse?> UpdateSwitchInstanceAsync(int id, SwitchInstanceRequest request);
    Task<IEnumerable<SwitchInstanceResponse>> GetSwitchInstancesAsync(int environmentId, int applicationId);
    Task<SwitchInstanceResponse?> GetSwitchInstanceAsync(string name);
}
