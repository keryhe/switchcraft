using System.Collections.Concurrent;

namespace Switchcraft.Client.Services.Default;

public class LocalCache: ILocalCache
{
    private ConcurrentDictionary<string, bool> _cache;

    public LocalCache()
    {
        _cache = new ConcurrentDictionary<string, bool>();
    }

    public void AddOrUpdate(string key, bool value)
    {
        _cache.AddOrUpdate(key, value, (_, oldValue) => value);
    }

    public bool TryGetValue(string key, out bool value)
    {
        return _cache.TryGetValue(key, out value);
    }
}