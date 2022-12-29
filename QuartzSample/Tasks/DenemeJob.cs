using Quartz;

namespace QuartzSample.Tasks
{
    public class DenemeJob : ITaskBase
    {
        public JobKey Key { get; set; } = new JobKey(nameof(DenemeJob), "Print");

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Denemeas");
            await Task.CompletedTask;
        }
    }
}