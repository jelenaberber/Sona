using Application;

namespace API.Core
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex, IApplicationActor actor);
    }
}
