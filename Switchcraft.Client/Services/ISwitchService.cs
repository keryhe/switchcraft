using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Switchcraft.Client.Dtos;

namespace Switchcraft.Client.Services;

public interface ISwitchService
{
    Task<bool> GetSwitchAsync(string name);
}
