using OllamaAPI;
using TestProject;


//BluetoothTest bluetoothTest = new BluetoothTest();
//bluetoothTest.ConnectToDevice("COM3");

BluetoothHandler bluetoothHandler = new BluetoothHandler();
bluetoothHandler.ConnectToDevice("00220100052C");


return;
StreamResponeFromPrompt streamResponeFromPrompt = new StreamResponeFromPrompt();

//string response = await StreamResponeFromPrompt.SendPromptStream("Give my 3 different recipes names that i cant create with these ingredients(Tomato, cheese, pasta, potatos, mayonaise,egg,butter, cream)");
string test = "Give my 3 different recipes names that i cant create with these ingredients(Tomato, cheese, pasta, potatos, mayonaise,egg,butter, cream)";
string audioUrl = await Local_TTS_API.GenerateTTSAsync(test);
Console.WriteLine(audioUrl);
AudioPlayer.PlayAudio(audioUrl);    