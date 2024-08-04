using System.Text.Json;
//API de bromas 
public class APIChistes
{
    private class ChisteResponse
    {
        public bool error { get; set; }
        public string ? category { get; set; }
        public string ? type { get; set; }
        public string ? joke { get; set; }
        public string ? setup { get; set; }
        public string ? delivery { get; set; }
        public Flags ? flags { get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string ? lang { get; set; }
    }

    public class Flags
    {
        public bool nsfw { get; set; }
        public bool religious { get; set; }
        public bool political { get; set; }
        public bool racist { get; set; }
        public bool sexist { get; set; }
        public bool @explicit { get; set; }
    }

    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> GetChiste()
    {
        var url = "https://v2.jokeapi.dev/joke/Any?lang=es";
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        var chisteData = JsonSerializer.Deserialize<ChisteResponse>(responseBody);

        if (chisteData != null && !chisteData.error)
        {
            return chisteData.type == "twopart" ? $"{chisteData.setup} - {chisteData.delivery}" : chisteData.joke;
        }
        else
        {
            return "¡Chiste no disponible!";
        }
    }
}
