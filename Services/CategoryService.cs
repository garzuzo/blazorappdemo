using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace blazorappdemo;
public class CategoryService : ICategoryService
{
    private HttpClient httpClient { get; set; }
    private JsonSerializerOptions options { get; set; }
    public CategoryService(HttpClient client)
    {
        httpClient = client;
        this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Category>?> GetCategory()
    {
        var response = await httpClient.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Category>>(content, options);
    }
}

public interface ICategoryService
{
    Task<List<Category>?> GetCategory();
}