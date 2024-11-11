using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using OllamaAPI;


namespace OllamaAPI
{
    public class StreamResponeFromPrompt
    {
        private static readonly string url = "http://172.22.192.1:11434/api/generate";
        public static string model = "llama3.2:3b";

        public class OllamaRequest
        {
            public string model { get; set; }
            public string prompt { get; set; }
        }

        public class OllamaResponse
        {
            public string response { get; set; }
        }
        public static async Task<string> SendPromptStream(string prompt)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = SetupRequest(prompt);
                var fullResponse = new StringBuilder();

                try
                {
                    HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                        using (var reader = new StreamReader(responseStream))
                        {
                            string line;
                            while ((line = await reader.ReadLineAsync()) != null)
                            {
                                try
                                {
                                    OllamaResponse data = JsonSerializer.Deserialize<OllamaResponse>(line);
                                    if (data != null && !string.IsNullOrEmpty(data.response))
                                    {
                                        fullResponse.Append(data.response); // Accumulate response text
                                        Console.Write(data.response); // Print each segment as it arrives
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error deserializing line: {ex.Message}");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to get a response");
                        return "Failed to get a response"; // Return failure message
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred: {ex.Message}");
                    return $"Error: {ex.Message}"; // Return error message
                }

                return fullResponse.ToString(); // Return the accumulated response
            }
        }

        public static HttpRequestMessage SetupRequest(string prompt)
        {
            OllamaRequest payload = new OllamaRequest
            {
                model = model,
                prompt = prompt
            };

            string jsonPayload = JsonSerializer.Serialize(payload);

            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
            return request;
        }

         public static string GetOllamaModelList()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c ollama list";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        public static string[] GetOllamaModelNames()
        {
            string output = GetOllamaModelList();
            string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var modelNames = lines.Skip(1)
                                  .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0])
                                  .ToArray();

            return modelNames;
        }
    }
}
