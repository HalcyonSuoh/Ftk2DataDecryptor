using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using CommandLine;

public class Program
{
    static char XorChar(char pChar, int pIndex, string xorkey)
    {
        return (char)(pChar ^ xorkey[pIndex % xorkey.Length]);
    }

    static string XorString(string content, string xorkey)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < content.Length; i++)
        {
            stringBuilder.Append(XorChar(content[i], i, xorkey));
        }
        return stringBuilder.ToString();
    }
    

    static void Main(string[] args)
    {
        ParserResult<ArgOptions> result = Parser.Default.ParseArguments<ArgOptions>(args).WithParsed(options =>
        {
            if (options.SaveData != null && options.OutputLoc != null)
            {
                Console.WriteLine("Using key " + options.XorKey);
                
                string xorKey = options.XorKey;
                string outputLoc = options.OutputLoc;
                string inputLoc = options.SaveData;
                string content = File.ReadAllText(inputLoc);
                
                File.WriteAllText(outputLoc, XorString(content, xorKey));
            }
            if (options.UserData != null && options.OutputLoc != null)
            {
                Console.WriteLine("Using key " + options.XorKey);
                
                string xorKey = options.XorKey;
                string outputLoc = options.OutputLoc;
                string inputLoc = options.UserData;
                string content = File.ReadAllText(inputLoc);
                
                File.WriteAllText(outputLoc, XorString(content, xorKey));
            }
        });
    }
}

public class ArgOptions
{
    [Option('u', "user", HelpText = "User data directory", SetName = "userdata")]
    public string? UserData { get; set; }
    
    [Option('s', "save", HelpText = "Save data/GameRun directory", SetName = "savedata")]
    public string? SaveData { get; set; }

    [Option('o', "output", HelpText = "output file directory")]
    public string? OutputLoc { get; set; }

    [Option('k', "xorkey", HelpText = "xorkey for encryption/decryption", Default = "21398xa2")]
    public string XorKey { get; set; }
}