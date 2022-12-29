using Quartz;
using QuartzSample.Tasks;

namespace QuartzSample.Helper
{
    public static class QuartzExtensions
    {
        public static void AddQuartzCustom<TJob>(this IServiceCollectionQuartzConfigurator quartz, List<QuartzSettings> quartzSettings) where TJob : ITaskBase
        {
            var jobName = typeof(TJob).Name;
            var existQuartz = quartzSettings.Where(x => x.JobName == jobName && x.TaskIsActive).FirstOrDefault();
            if (existQuartz != null)
            {
                quartz.AddJob<TJob>(options =>
                {
                    options.WithIdentity(existQuartz.JobName)
                            .Build();
                });

                quartz.AddTrigger(options =>
                {
                    options.WithIdentity($"{existQuartz.JobName}Triggger")
                            .ForJob(existQuartz.JobName)
                            .WithSimpleSchedule(s =>
                                s.WithIntervalInSeconds(5)
                                 .RepeatForever());
                });
            }
        }
    }
}