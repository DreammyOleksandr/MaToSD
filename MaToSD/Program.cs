using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "/Users/bondarenkooleksandr/C#Projects/MaToSD/MaToSD/TestMarkDown.md";
        StringBuilder content = new();
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        string text = File.ReadAllText(filePath);
        text = PrePartsConverter(text);
        Console.WriteLine(text);
        
        // foreach (var line in lines)
        // {
        //     // string convertedLine = Convert(line);
        //     // content.AppendLine(convertedLine);
        // }
        
        Console.WriteLine(content.ToString());
    }

    static string Convert(string line)
    {
        if(line.Equals("")) line = Regex.Replace(line, @"", "</br></br>");

        if (Regex.IsMatch(line, @"\*\*(.*?)\*\*"))
        {
            //this is a Bold text 
            line = Regex.Replace(line, @"\*\*(.*?)\*\*", "<b>$1</b>");
        }
        if (Regex.IsMatch(line, @"_(.*?)_"))
        {
            //this is an Italic text 
            line = Regex.Replace(line, @"_(.*?)_", "<i>$1</i>");
        }
        if (Regex.IsMatch(line, @"`([^`]+)`"))
        {
            //this is a Monospaced text 
            line = Regex.Replace(line, @"`([^`]+)`", "<tt>$1</tt>");
        }
        
        if (Regex.IsMatch(line, @"```(.*?)```"))
        {
            //this is a Monospaced text 
            line = Regex.Replace(line, @"```(.*?)```", "<pre>$1</pre>");
            line = "<p>" + line + "</p>";
        }
        return line;
    }

    static string PrePartsConverter(string text)
    {
        if ((Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*") || Regex.IsMatch(text, @"\s*([\s\S]*?)\s*```")) && !Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*```"))
        {
            throw new Exception("pre part is not opened/closed correctly");
        }
        if (Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*```"))
        {
            text = Regex.Replace(text, @"```\s*([\s\S]*?)\s*```", "<pre>$1</pre>");
        }
        return text;
    }
}