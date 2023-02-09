using System.Text.Json;

namespace TaskManager.IntegrationTests.Extensions;

public static class SerializerExtentions
{
    public static async Task<T> ReadAsJsonAsync<T>(this HttpContent httpContent)
    {
        var content = await httpContent.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content);
    }
}
