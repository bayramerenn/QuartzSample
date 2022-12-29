using Quartz;

namespace QuartzSample.Tasks
{
    public class DenemeJob : ITaskBase
    {

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Denemeas");
            await Task.CompletedTask;
        }
    }
}