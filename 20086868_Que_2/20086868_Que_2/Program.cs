using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, string> extensions = new Dictionary<string, string>()
        {
            { ".mp4", "Video File (MPEG-4)" },
            { ".mov", "Apple QuickTime Video" },
            { ".avi", "Audio Video Interleave" },
            { ".mkv", "Matroska Video File" },
            { ".webm", "Web Media Video File" },
            { ".mp3", "Audio File (MPEG-3)" },
            { ".wav", "Waveform Audio File" },
            { ".flac", "Lossless Audio File" },
            { ".png", "Portable Network Graphics Image" },
            { ".jpg", "JPEG Image File" },
            { ".gif", "Graphics Interchange Format" },
            { ".pdf", "Portable Document Format" },
            { ".txt", "Plain Text File" },
            { ".docx", "Microsoft Word Document" },
            { ".xlsx", "Microsoft Excel Spreadsheet" },
            { ".pptx", "Microsoft PowerPoint Presentation" },
            { ".zip", "Compressed Archive File" },
            { ".rar", "RAR Archive File" },
            { ".exe", "Windows Executable File" },
            { ".html", "Webpage File (HTML)" },
        };

        Console.WriteLine("=== File Extension Information System ===");

        while (true)
        {
            Console.Write("\nEnter a file extension (e.g. .mp4) or type 'exit' to quit: ");
            string input = (Console.ReadLine() ?? "").ToLower();

            if (input == "exit")
                break;

            if (!input.StartsWith("."))
            {
                Console.WriteLine("Please include a dot, e.g. .mp4");
                continue;
            }

            if (extensions.ContainsKey(input))
            {
                Console.WriteLine($"{input} → {extensions[input]}");
            }
            else
            {
                Console.WriteLine($"Unknown extension: {input}");
                Console.WriteLine("No information available. Please try another extension.");
            }
        }
    }
}