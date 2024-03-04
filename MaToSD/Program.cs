using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        // if (args.Length == 0)
        // {
        //     Console.WriteLine("Usage: markdownToHtml <input_file>");
        //     return;
        // }

        string inputFile = "/Users/bondarenkooleksandr/C#Projects/MaToSD/MaToSD/TestMarkDown.md";

        if (!File.Exists(inputFile))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        string markdownText = File.ReadAllText(inputFile);
        string htmlText = ConvertMarkdownToHtml(markdownText);
        string outputFilePath = Path.ChangeExtension(inputFile, ".html");

        File.WriteAllText(outputFilePath, htmlText);

        Console.WriteLine($"HTML file successfully created: {outputFilePath}");
    }


    static string ConvertMarkdownToHtml(string markdownText)
    {
        if (Regex.IsMatch(markdownText, @"\*\*.*_.*\*\*|\*\*.*`.*\*\*|_.*\*\*.*_|_.*`.*_|`.*\*\*.*`|`.*_.*`"))
        {
            throw new InvalidOperationException("Nested markup detected. Cannot convert.");
        }

        string htmlText = markdownText;
        
        htmlText = Regex.Replace(htmlText, @"\*\*(.*?)\*\*", "<b>$1</b>");
        htmlText = Regex.Replace(htmlText, @"_(.*?)_", "<i>$1</i>");
        htmlText = Regex.Replace(htmlText, @"```([\s\S]*?)```", "<pre>$1</pre>");
        htmlText = Regex.Replace(htmlText, @"`(.*?)`", "<tt>$1</tt>");
        htmlText = Regex.Replace(htmlText, @"\n{2,}", "</p>\n<p>");
        htmlText = "<p>" + htmlText + "</p>";
        
        return htmlText;
    }
}