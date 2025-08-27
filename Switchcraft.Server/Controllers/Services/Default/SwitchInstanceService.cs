using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Switchcraft.Data;
using Switchcraft.Data.Models;
using Switchcraft.Server.Controllers.Dtos;
using Switchcraft.Server.Controllers.Hubs;

namespace Switchcraft.Server.Controllers.Services.Default;

public class SwitchInstanceService : ISwitchInstanceService
{
    private readonly ISwitchcraftDbContext _dbContext;
    private readonly INotificationService _notificationService;

    public SwitchInstanceService(ISwitchcraftDbContext dbContext, INotificationService notificationService)
    {
        _dbContext = dbContext;
        _notificationService = notificationService;
    }

    public async Task<SwitchInstanceResponse?> UpdateSwitchInstanceAsync(int id, SwitchInstanceRequest request)
    {
        var instance = await _dbContext.SwitchInstances
            .Include(f => f.Environment)
            .Include(f => f.Switch)
            .SingleOrDefaultAsync(f => f.Id == id);
        
        if(instance != null)
        {
            instance.IsEnabled = request.IsEnabled;
            await _dbContext.SaveChangesAsync();

            var response = new SwitchInstanceResponse(instance);
            await _notificationService.NotifyIsEnabledChanged(instance);
            
            return response;
        }

        return null;
     }

    public async Task<IEnumerable<SwitchInstanceResponse>> GetSwitchInstancesAsync(int environmentId, int applicationId)
    {
        var instances = await _dbContext.SwitchInstances
            .Include(f => f.Environment)
            .Include(f => f.Switch)
            .Where(f => f.EnvironmentId == environmentId && f.Switch!.ApplicationId == applicationId).ToListAsync();

        var result = new List<SwitchInstanceResponse>();
        foreach (SwitchInstance instance in instances)
        {
            result.Add(new SwitchInstanceResponse(instance));
        }

        return result;
    }

    public async Task<SwitchInstanceResponse?> GetSwitchInstanceAsync(string name)
    {
        var instance = await _dbContext.SwitchInstances
            .Include(f => f.Environment)
            .Include(f => f.Switch)
            .SingleOrDefaultAsync(f => f.Switch!.Name == name);

        if (instance != null)
        {
            return new SwitchInstanceResponse(instance);
        }
        return null;
    }
}
