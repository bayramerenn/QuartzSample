using Quartz;
using Quartz.Impl.AdoJobStore.Common;
using QuartzSample.Servives.Concerete;

namespace QuartzSample.Tasks
{
    public class PrintMessageJob : ITaskBase
    {
        private static IPropertyOwnerService _propertyOwnerService;

      

        public async  Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(_propertyOwnerService.OperationId);
            var property = await _propertyOwnerService.GetOwners();

            //foreach (var item in property!.Data)
            //{
            //    Console.WriteLine(item.FirstName);
            //}
        }

        public void Register(IServiceProvider serviceProvider)
        {
            _propertyOwnerService = serviceProvider.GetRequiredService<IPropertyOwnerService>();
        }
    }
}