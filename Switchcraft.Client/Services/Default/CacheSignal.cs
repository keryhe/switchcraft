namespace Switchcraft.Client.Services.Default;

public class CacheSignal : ICacheSignal
{
    private static Semaphore _semaphore = new Semaphore(1, 1);

    public void Wait()
    {
        _semaphore.WaitOne();
    }

    public int Release()
    {
        return _semaphore.Release();
    }
}