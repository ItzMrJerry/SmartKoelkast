using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestProject
{
    class Local_TTS_API
    {
        public static async Task<string> GenerateTTSAsync(string text)
        {
            string url = "http://127.0.0.1:8010/generate";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var payload = new { text = text };
                    string json = JsonSerializer.Serialize(payload);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        // Parse JSON response to get the audio URL
                        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(result);
                        if (jsonResponse.TryGetProperty("audio_url", out JsonElement audioUrlElement))
                        {
                            return audioUrlElement.GetString();
                        }

                        return "No audio URL found in response.";
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Exception: {ex.Message}";
                }
            }
        }
    }
}
