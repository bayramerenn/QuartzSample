using QuartzSample.Models;

namespace QuartzSample.Servives.Concerete
{
    public interface IPropertyOwnerService
    {
        Task<BaseResponse<List<PropertyOwner>>?> GetOwners();
        public string OperationId { get; }
    }
}