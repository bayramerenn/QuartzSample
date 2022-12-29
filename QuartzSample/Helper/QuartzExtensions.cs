using Quartz;
using QuartzSample.Tasks;

namespace QuartzSample.Helper
{
    public static class QuartzExtensions
    {
        public static void AddQuartzCustom<TJob>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration configuration, TJob job) where TJob : ITaskBase
        {
            var taskConfig = configuration.GetSection(nameof(QuartzSettings)).Get<List<QuartzSettings>>();

            if (taskConfig.Where(x => x.JobName == job.Key.Name && x.TaskIsActive).Any())
            {
                quartz.AddJob<TJob>(options =>
                {
                    options.WithIdentity(job.Key)
                            .Build();
                });

                quartz.AddTrigger(options =>
                {
                    options.WithIdentity($"{job.Key.Name}Triggger")
                            .ForJob(job.Key)
                            .WithSimpleSchedule(s =>
                                s.WithIntervalInSeconds(5)
                                 .RepeatForever());
                });
            }
        }
    }
}