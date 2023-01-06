using Quartz;

namespace QuartzSample.Tasks
{
    public interface ITaskBase : IJob
    {
        void Register(IServiceProvider serviceProvider);
    }
}