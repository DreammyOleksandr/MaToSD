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

        string[] lines = text.Split("\n\r".ToCharArray());
        for (int i = 0; i < lines.Length; i++)
        {
            if (Regex.IsMatch(lines[i], @"<pre>(.*?)</pre>"))
            {
                content.AppendLine(lines[i]);
                i++;
            }

            if (!IsMarkUp(lines[i]))
            {
                content.Append("<p>");
                while (i < lines.Length && !IsMarkUp(lines[i]))
                {
                    content.Append(lines[i]);
                    i++;
                }

                content.AppendLine("</p>");
            }

            if (i < lines.Length)
            {
                string convertedLine = Convert(lines[i]);
                content.AppendLine(convertedLine);
            }
        }

        var result = content.ToString();
        Console.WriteLine(content.ToString());
    }

    static string Convert(string line)
    {
        if (line.Equals(""))
            return null;

        //this is a Bold text 
        if (Regex.IsMatch(line, @"\*\*(.*?)\*\*"))
            line = Regex.Replace(line, @"\*\*(.*?)\*\*", "<b>$1</b>");

        //this is an Italic text 
        if (Regex.IsMatch(line, @"_(.*?)_"))
            line = Regex.Replace(line, @"_(.*?)_", "<i>$1</i>");

        //this is a Monospaced text 
        if (Regex.IsMatch(line, @"`([^`]+)`"))
            line = Regex.Replace(line, @"`([^`]+)`", "<tt>$1</tt>");


        line = "<p>" + line + "</p>";
        return line;
    }

    static string PrePartsConverter(string text)
    {
        if ((Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*") || Regex.IsMatch(text, @"\s*([\s\S]*?)\s*```")) &&
            !Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*```"))
            throw new Exception("pre part is not opened/closed correctly");

        if (Regex.IsMatch(text, @"```\s*([\s\S]*?)\s*```"))
            text = Regex.Replace(text, @"```\s*([\s\S]*?)\s*```", "<pre>$1</pre>");

        return text;
    }

    static bool IsMarkUp(string line) =>
        Regex.IsMatch(line, @"```\s*([\s\S]*?)\s*```") ||
        Regex.IsMatch(line, @"\*\*(.*?)\*\*") ||
        Regex.IsMatch(line, @"`([^`]+)`") ||
        Regex.IsMatch(line, @"_(.*?)_") ||
        line.Equals("");
}