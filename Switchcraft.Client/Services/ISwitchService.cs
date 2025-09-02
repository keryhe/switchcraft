using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Switchcraft.Client.Dtos;

namespace Switchcraft.Client.Services;

public interface ISwitchService
{
    bool GetSwitch(string name, bool defaultValue);
}
