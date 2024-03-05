using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "/Users/bondarenkooleksandr/C#Projects/MaToSD/MaToSD/TestMarkDown.md";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return;
        }
        
        List<string> lines = File.ReadAllLines(filePath).ToList();
        
    }
}