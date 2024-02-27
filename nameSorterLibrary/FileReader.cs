
using nameSorterLibrary.Interfaces;

namespace nameSorterLibrary;

public class FileReader : IFileReader
{
    public List<string> ReadNamesFromFile(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath).ToList();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found: " + filePath);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while reading the file: " + ex.Message);
            return null;
        }
    }
}
