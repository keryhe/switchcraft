using System;
using Microsoft.EntityFrameworkCore;
using Switchcraft.Data;
using Switchcraft.Server.Controllers.Dtos;

namespace Switchcraft.Server.Controllers.Services.Default;

public class EnvironmentService : IEnvironmentService
{
    private readonly ISwitchcraftDbContext _dbContext;
    
    public EnvironmentService(ISwitchcraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EnvironmentResponse> CreateEnvironmentAsync(EnvironmentRequest request)
    {
        var env = new Data.Models.Environment()
        {
            Name = request.Name
        };

        await _dbContext.Environments.AddAsync(env);
        await _dbContext.SaveChangesAsync();  

        var result = await _dbContext.Environments.FirstOrDefaultAsync(c => c.Name == request.Name);

        if (result != null)
        {
            return new EnvironmentResponse(result);
        }

        throw new Exception("Error creating Environment: " + request.Name);
    }

    public async Task<EnvironmentResponse?> UpdateEnvironmentAsync(int id, EnvironmentRequest request)
    {
        var env = await _dbContext.Environments.FindAsync(id);
        
        if(env != null)
        {
            env.Name = request.Name;
            await _dbContext.SaveChangesAsync();
            
            return new EnvironmentResponse(env);
        }

        return default;
    }

    public async Task<IEnumerable<EnvironmentResponse>> GetEnvironmentsAsync()
    {
        var envs = await _dbContext.Environments.ToListAsync();

        var result = new List<EnvironmentResponse>();
        foreach (Data.Models.Environment env in envs)
        {
            result.Add(new EnvironmentResponse(env));
        }

        return result;
    }

    public async Task<EnvironmentResponse?> GetEnvironmentAsync(int id)
    {
        var env = await _dbContext.Environments.FindAsync(id);

        if(env != null)
        {
            return new EnvironmentResponse(env);
        }
        return default;
    }
}
