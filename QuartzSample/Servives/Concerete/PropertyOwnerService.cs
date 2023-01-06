using QuartzSample.Models;
using System.Text.Json;

namespace QuartzSample.Servives.Concerete
{
    public class PropertyOwnerService : IPropertyOwnerService
    {
        private readonly HttpClient _httpClient;

        public string OperationId { get; }
        public PropertyOwnerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            OperationId = Guid.NewGuid().ToString()[^4..];
        }

        public async Task<BaseResponse<List<PropertyOwner>>?> GetOwners()
        {
            var response = await _httpClient.GetAsync("api/PropertyOwners/List");

            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return await JsonSerializer.DeserializeAsync<BaseResponse<List<PropertyOwner>>>(responseStream!, options);
        }
    }
}

public class BaseResponse<T>
{
    public int HttpStatusCode { get; set; }
    public string HttpStatusCodeName { get; set; }
    public int ResponseCode { get; set; }
    public string ResponseCodeName { get; set; }
    public object ExternalResponseCodeName { get; set; }
    public object Message { get; set; }
    public T Data { get; set; }


}