namespace Switchcraft.Client.Services;

public interface ICacheSignal
{
    void Wait();
    int Release();
}