using Quartz;

namespace QuartzSample.Tasks
{
    public class PrintMessageJob : ITaskBase
    {
        public JobKey Key { get; set; } = new JobKey(nameof(PrintMessageJob), "Print");

        public async Task Execute(IJobExecutionContext context)
        {
     
            Console.WriteLine("Greetings from HelloJob!");
          
            await Task.CompletedTask;
        }
    }
}