using Quartz;
using QuartzSample.Tasks;

namespace QuartzSample.Extensions
{
    public static class QuartzExtensions
    {
        public static void AddQuartzCustom(this IServiceCollection services, IConfiguration configuration)
        {
            var quartzSettings = configuration.GetSection(nameof(QuartzSettings)).Get<List<QuartzSettings>>();
            var type = typeof(ITaskBase)!;


            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(g => g.FullName!.Contains("QuartzSample.Tasks")) //ITaskBase implemente edildigi dosyanin yolu 
                .SelectMany(x => x.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            if (types != null)
                foreach (var jobType in types)
                {
                    var job = (ITaskBase)Activator.CreateInstance(jobType)!;
                    job.Register(services.BuildServiceProvider());

                    services.AddQuartz(config =>
                    {

                        var existQuartz = quartzSettings.Where(x => x.JobName == jobType.Name && x.TaskIsActive).FirstOrDefault();

                        if (existQuartz != null)
                        {

                            config.AddJob(jobType!, null, options =>
                            {
                                options.WithIdentity(existQuartz.JobName)
                                        .Build();
                            });

                            config.AddTrigger(options =>
                            {
                                options.WithIdentity($"{existQuartz.JobName}Triggger")
                                        .ForJob(existQuartz.JobName)
                                        .WithSimpleSchedule(s =>
                                            s.WithIntervalInSeconds(5)
                                             .RepeatForever());

                                // Cron eklemek icin asagidaki kodu kullan

                                //options.WithIdentity($"{existQuartz.JobName}Triggger")
                                //       .ForJob(existQuartz.JobName)
                                //       .WithCronSchedule(existQuartz.Cron);
                            });
                        }
                    });
                }




        }
    }
}