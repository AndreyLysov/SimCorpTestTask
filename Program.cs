using System.Reflection;
using System.Text.RegularExpressions;


class Program
{
    private static string fileName;

    private static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Please enter file name including extension.\n" +
                "File should be placed inside SimCorpTestTask folder");

            fileName = Console.ReadLine();

            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("File name should not be null or empty string");

            var text = GetTextFromFile();

            foreach (var item in GetUniqueWords(text))
            {
                Console.WriteLine($"{item.Value}: {item.Key}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static Dictionary<string, int> GetUniqueWords(string text)
    {
        Regex pattern = new Regex("[;.,\t]|- ");

        return pattern.Replace(text, string.Empty)
            .ToLower()
            .Split(" ")
            .Select(word => word.Trim())
            .Where(word => !string.IsNullOrEmpty(word))
            .GroupBy(c => c)
            .ToDictionary(c => c.Key, c => c.Count());
    }

    private static string GetTextFromFile()
    {
        return File.ReadAllText(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Split("bin/")[0] + fileName);
    }
}