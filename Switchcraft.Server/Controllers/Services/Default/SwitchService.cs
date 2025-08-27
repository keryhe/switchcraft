using System;
using Microsoft.EntityFrameworkCore;
using Switchcraft.Data;
using Switchcraft.Data.Models;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services.Default;

public class SwitchService : ISwitchService
{
    private readonly ISwitchcraftDbContext _dbContext;
    private readonly INotificationService _notificationService;

    public SwitchService(ISwitchcraftDbContext dbContext, INotificationService notificationService)
    {
        _dbContext = dbContext;
        _notificationService = notificationService;
    }

    public async Task<SwitchResponse> CreateSwitchAsync(SwitchRequest request)
    {
        var application = await _dbContext.Applications.FindAsync(request.ApplicationId);
        if (application == null)
        {
            throw new Exception("Application: " + request.ApplicationId + " not found");
        }

        var flag = new Switch()
        {
            Name = request.Name,
            ApplicationId = application.Id
        };

        var envs = await _dbContext.Environments.ToListAsync();
        foreach(Data.Models.Environment env in envs)
        {
            var switchInstance = new SwitchInstance()
            {
                IsEnabled = false,
                EnvironmentId = env.Id,
                Switch = flag
            };
            flag.SwitchInstances.Add(switchInstance);
        }

        await _dbContext.Switches.AddAsync(flag);
        await _dbContext.SaveChangesAsync();  

        var result = await _dbContext.Switches.FirstOrDefaultAsync(c => c.Name == request.Name);

        if (result != null)
        {
            await _notificationService.NotifyIsEnabledChanged(result.SwitchInstances);
            return new SwitchResponse(result);
        }

        throw new Exception("Error creating Switch: " + request.Name);
    }

    public async Task<SwitchResponse?> UpdateSwitchAsync(int id, SwitchRequest request)
    {
        var flag = await _dbContext.Switches.FindAsync(id);
        
        if(flag != null)
        {
            flag.Name = request.Name;
            await _dbContext.SaveChangesAsync();
            
            return new SwitchResponse(flag);
        }

        return default;
    } 

    public async Task<IEnumerable<SwitchResponse>> GetSwitchesAsync()
    {
        var switches = await _dbContext.Switches
            .Include(f => f.Application)
            .Include(f => f.SwitchInstances)
            .ThenInclude(i => i.Environment)
            .ToListAsync();

        var result = new List<SwitchResponse>();
        foreach (Switch s in switches)
        {
            result.Add(new SwitchResponse(s));
        }

        return result;
    }

    public async Task<SwitchResponse?> GetSwitchAsync(int id)
    {
        var s = await _dbContext.Switches
            .Include(f => f.Application)
            .Include(f => f.SwitchInstances)
            .ThenInclude(i => i.Environment)
            .FirstOrDefaultAsync(f => f.Id == id);

        if(s != null)
        {
            return new SwitchResponse(s);
        }
        return default;
    }
}
