using QuartzSample.Models;
using System.Text.Json;

namespace QuartzSample.Servives.Concerete
{
    public class PropertyOwnerConsumer
    {
        private readonly HttpClient _httpClient;

        public PropertyOwnerConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PropertyOwner>?> GetOwners()
        {
            var response = await _httpClient.GetAsync("api/PropertyOwners/List");

            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<PropertyOwner>>(responseStream!);
        }
    }
}
