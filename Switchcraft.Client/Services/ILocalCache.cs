namespace Switchcraft.Client.Services;

public interface ILocalCache
{
    void AddOrUpdate(string key, bool value);
    bool TryGetValue(string key, out bool value);
}