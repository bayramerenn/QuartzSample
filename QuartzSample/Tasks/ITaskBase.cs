using Quartz;

namespace QuartzSample.Tasks
{
    public interface ITaskBase : IJob
    {
        public JobKey Key { get; set; }
    }
}