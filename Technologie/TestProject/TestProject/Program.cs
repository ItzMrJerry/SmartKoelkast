using OllamaAPI;

StreamResponeFromPrompt streamResponeFromPrompt = new StreamResponeFromPrompt();

string response = await StreamResponeFromPrompt.SendPromptStream("Give my 3 different recipes names that i cant create with these ingredients(Tomato, cheese, pasta, potatos, mayonaise,egg,butter, cream)");

Console.WriteLine(response);
