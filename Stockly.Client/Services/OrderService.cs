using System.Net.Http.Json;
using Stockly.Client.Models;

namespace Stockly.Client.Services;

/// <summary>
/// Service that handles all HTTP communication with the Orders API endpoints.
/// Registered as Scoped in Program.cs and injected into Blazor pages.
/// </summary>
public class OrderService
{
    private readonly HttpClient _http;

    public OrderService(HttpClient http)
    {
        _http = http;
    }

    /// <summary>Get all orders from the API.</summary>
    public async Task<List<OrderDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<OrderDto>>("api/orders") ?? new();
    }

    /// <summary>Get a single order by ID (includes order items).</summary>
    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<OrderDto>($"api/orders/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    /// <summary>Create a new order. Returns the created order or null on failure.</summary>
    public async Task<OrderDto?> CreateAsync(CreateOrderDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/orders", dto);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<OrderDto>();
        return null;
    }
}
