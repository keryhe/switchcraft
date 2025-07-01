using System;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services;

public interface ISwitchInstanceService
{
    Task<InstanceSwitchResponse?> UpdateSwitchInstanceAsync(int id, SwitchInstanceRequest request);
    Task<IEnumerable<InstanceSwitchResponse>> GetSwitchInstancesAsync(int environmentId, int applicationId);
}
