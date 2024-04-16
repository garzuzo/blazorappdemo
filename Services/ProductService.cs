using System.Net.Http.Json;
using System.Text.Json;

namespace blazorappdemo;
public class ProductService : IProductService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;

    public ProductService(HttpClient httpClient)
    {
        client = httpClient;
        this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<Product>?> GetProduct()
    {
        var response = await client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Product>>(content, options);
    }

    public async Task<Product?> GetProductById(int id)
    {
        var response = await client.GetAsync($"v1/products/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<Product>(content, options);
    }
    public async Task Add(Product product)
    {
        var response = await client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task Update(Product product)
    {

        var response = await client.PutAsync($"v1/products/{product.Id}", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    public async Task Delete(int productId)
    {
        var response = await client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}

public interface IProductService
{
    Task<List<Product>?> GetProduct();
    Task<Product?> GetProductById(int id);
    Task Add(Product product);
    Task Update(Product product);
    Task Delete(int productId);

}