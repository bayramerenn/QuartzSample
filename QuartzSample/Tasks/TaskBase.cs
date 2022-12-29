using CrystalQuartz.Core.Domain.Activities;
using Quartz;

namespace QuartzSample.Tasks
{
    public static class TaskBase
    {
        public static IEnumerable<ITaskBase> EnumerateJobs()
        {
            yield return new PrintMessageJob();
            yield return new DenemeJob();
          
        }
    }
}