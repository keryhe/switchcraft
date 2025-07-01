using System;
using Microsoft.EntityFrameworkCore;
using Switchcraft.Data;
using Switchcraft.Data.Models;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services.Default;

public class ApplicationService : IApplicationService
{
    private readonly ISwitchcraftDbContext _dbContext;

    public ApplicationService(ISwitchcraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationResponse> CreateApplicationAsync(ApplicationRequest request)
    {
        var app = new Application()
        {
            Name = request.Name
        };

        await _dbContext.Applications.AddAsync(app);
        await _dbContext.SaveChangesAsync();  

        var result = await _dbContext.Applications.FirstOrDefaultAsync(c => c.Name == request.Name);

        if (result != null)
        {
            return new ApplicationResponse(result);
        }

        throw new Exception("Error creating Application: " + request.Name);     
    }

    public async Task<ApplicationResponse?> UpdateApplicationAsync(int id, ApplicationRequest request)
    {
        var app = await _dbContext.Applications.FindAsync(id);
        
        if(app != null)
        {
            app.Name = request.Name;
            await _dbContext.SaveChangesAsync();
            
            return new ApplicationResponse(app);
        }

        return default;
    }

    public async Task<IEnumerable<ApplicationResponse>> GetApplicationsAsync()
    {
        var apps = await _dbContext.Applications.ToListAsync();

        var result = new List<ApplicationResponse>();
        foreach (Application app in apps)
        {
            result.Add(new ApplicationResponse(app));
        }

        return result;
    }


    public async Task<ApplicationResponse?> GetApplicationAsync(int id)
    {
        var app = await _dbContext.Applications.FindAsync(id);

        if(app != null)
        {
            return new ApplicationResponse(app);
        }
        return default;
    }

    public async Task<IEnumerable<SwitchResponse>> GetSwitchesAsync(int id)
    {
         List<Switch> switches = await _dbContext.Switches
            .Include(f => f.Application)
            .Include(f => f.SwitchInstances)
            .ThenInclude(i => i.Environment)
            .Where(f => f.ApplicationId == id).ToListAsync();

        List<SwitchResponse> result = new List<SwitchResponse>();
        foreach (var s in switches)
        {
            result.Add(new SwitchResponse(s));
        }

        return result;
    }
}
