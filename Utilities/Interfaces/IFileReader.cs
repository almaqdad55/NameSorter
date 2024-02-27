namespace Utilities.Interfaces
{
    public interface IFileReader
    {
        List<string> ReadNamesFromFile(string filePath);
    }
}