using System.Net.Http.Json;
using Stockly.Client.Models;

namespace Stockly.Client.Services;

/// <summary>
/// Service that handles all HTTP communication with the Products API endpoints.
/// Registered as Scoped in Program.cs and injected into Blazor pages.
/// </summary>
public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    /// <summary>Get all products from the API.</summary>
    public async Task<List<ProductDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<ProductDto>>("api/products") ?? new();
    }

    /// <summary>Get a single product by its ID.</summary>
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<ProductDto>($"api/products/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    /// <summary>Search products by name (calls the search endpoint).</summary>
    public async Task<List<ProductDto>> SearchAsync(string name)
    {
        try
        {
            return await _http.GetFromJsonAsync<List<ProductDto>>(
                $"api/products/search?name={Uri.EscapeDataString(name)}") ?? new();
        }
        catch
        {
            return new();
        }
    }

    /// <summary>Create a new product. Returns the created product or null on failure.</summary>
    public async Task<ProductDto?> CreateAsync(CreateProductDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/products", dto);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        return null;
    }

    /// <summary>Update an existing product. Returns true on success.</summary>
    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/products/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    /// <summary>Delete a product. Returns true on success.</summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/products/{id}");
        return response.IsSuccessStatusCode;
    }
}
