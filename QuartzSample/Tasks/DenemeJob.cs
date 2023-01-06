using Quartz;

namespace QuartzSample.Tasks
{
    public class DenemeJob 
    {

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Denemeas");
            await Task.CompletedTask;
        }

        public void Register(IServiceProvider serviceProvider)
        {
            
        }
    }
}