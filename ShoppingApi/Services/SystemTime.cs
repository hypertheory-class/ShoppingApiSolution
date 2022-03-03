namespace ShoppingApi.Services;

public class SystemTime : ISystemTime
{
    public SystemTime()
    {
     // Really bad. Don't do this   Thread.Sleep(1000);
    }
    public DateTime GetCurrent()
    {
        return DateTime.Now;
    }
}

public interface ISystemTime
{
    DateTime GetCurrent();
}
