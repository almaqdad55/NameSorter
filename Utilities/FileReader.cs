using Microsoft.Extensions.Logging;
using Utilities.Interfaces;
namespace Utilities;

public class FileReader : IFileReader
{
    private readonly ILogger<FileReader> _log;

    public FileReader(ILogger<FileReader> log)
    {
        _log = log;
    }

    // Read names from a file specified by the filePath
    public List<string> ReadNamesFromFile(string filePath)
    {
        try
        {
            _log.LogInformation("Reading {filePath}", filePath);
            return File.ReadAllLines(filePath).ToList();
        }
        catch (FileNotFoundException ex)
        {
            _log.LogError("Erorr File Not Found!!!", ex);
            throw;
        }
        catch (Exception ex)
        {
            _log.LogError("An error occurred while reading the file: ", ex.Message);
            throw;
        }
    }
}
