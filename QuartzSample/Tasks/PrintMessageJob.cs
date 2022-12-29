using Quartz;
using QuartzSample.Servives.Concerete;

namespace QuartzSample.Tasks
{
    public class PrintMessageJob : ITaskBase
    {
        private readonly IPropertyOwnerService _propertyOwnerService;

        public PrintMessageJob(IPropertyOwnerService propertyOwnerService)
        {
            _propertyOwnerService = propertyOwnerService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var property = await _propertyOwnerService.GetOwners();

            foreach (var item in property!.Data)
            {
                Console.WriteLine(item.FirstName);
            }
        }
    }
}