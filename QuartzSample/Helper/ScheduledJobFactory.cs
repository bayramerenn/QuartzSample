using Quartz;
using Quartz.Spi;

namespace QuartzSample.Helper
{
    public class ScheduledJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ScheduledJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;

            var job = (IJob)_serviceProvider.GetRequiredService(jobDetail.JobType);

            return job;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}