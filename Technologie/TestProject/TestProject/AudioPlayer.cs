using System;
using System.Diagnostics;

class AudioPlayer
{
    public static void PlayAudio(string filePath)
    {
        try
        {
            // Use 'explorer.exe' to open the file with the default media player
            Process.Start("explorer.exe", filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error playing audio: {ex.Message}");
        }
    }
}
