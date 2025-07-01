using System;
using Microsoft.EntityFrameworkCore;
using Switchcraft.Data;
using Switchcraft.Data.Models;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services.Default;

public class SwitchInstanceService : ISwitchInstanceService
{
    private readonly ISwitchcraftDbContext _dbContext;

    public SwitchInstanceService(ISwitchcraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InstanceSwitchResponse?> UpdateSwitchInstanceAsync(int id, SwitchInstanceRequest request)
     {
        var instance = await _dbContext.SwitchInstances.FindAsync(id);
        
        if(instance != null)
        {
            instance.IsEnabled = request.IsEnabled;
            await _dbContext.SaveChangesAsync();
            
            return new InstanceSwitchResponse(instance);
        }

        return default;
     }

    public async Task<IEnumerable<InstanceSwitchResponse>> GetSwitchInstancesAsync(int environmentId, int applicationId)
    {
        var instances = await _dbContext.SwitchInstances
            .Include(f => f.Environment)
            .Include(f => f.Switch)
            .Where(f => f.EnvironmentId == environmentId && f.Switch!.ApplicationId == applicationId).ToListAsync();
        
        var result = new List<InstanceSwitchResponse>();
        foreach (SwitchInstance instance in instances)
        {
            result.Add(new InstanceSwitchResponse(instance));
        }
        
        return result;
    }
}
