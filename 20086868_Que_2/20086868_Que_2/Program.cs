using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, string> extensions = new Dictionary<string, string>()   //Dictionary to store file extensions, key-value pair, key = extention and value = Full form
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
        }; // file types

        Console.WriteLine("=== File Extension Information System ===");  // heading 

        while (true)
        {
            Console.Write("\nEnter a file extension (e.g. .mp4) or type 'exit' to quit: ");
            string input = (Console.ReadLine() ?? "").ToLower();  // coverts user input to lower case for easy matching 

            if (input == "exit")
                break;

            if (!input.StartsWith(".")) // as most shortcuts start with   '.' it checks if user provided a extension with '.' if not it asks again
            {
                Console.WriteLine("Please include a dot, e.g. .mp4");
                continue;
            }

            if (extensions.ContainsKey(input))
            {
                Console.WriteLine($"{input} → {extensions[input]}");  // shows full form for extension provided by user 
            }
            else
            {
                Console.WriteLine($"Unknown extension: {input}");
                Console.WriteLine("No information available. Please try another extension.");   // else condition 
            }
        }
    }
}