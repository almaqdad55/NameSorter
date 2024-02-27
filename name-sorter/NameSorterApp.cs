using Microsoft.Extensions.Logging;
using Utilities.Interfaces;
namespace name_sorter;

public class NameSorterApp
{
    private readonly IFileReader _fileReader;
    private readonly INameSorter _nameSorter;
    private readonly IOutputHandler _outputHandler;
    private readonly ILogger<NameSorterApp> _log;

    public NameSorterApp(IFileReader fileReader, INameSorter nameSorter, IOutputHandler outputHandler, ILogger<NameSorterApp> log)
    {
        _fileReader = fileReader;
        _nameSorter = nameSorter;
        _outputHandler = outputHandler;
        _log = log;

    }

    // The entry point for executing the name sorting logic
    public void Run(string filePath)
    {

        // Read names from a file specified by the filePath
        List<string> names = _fileReader.ReadNamesFromFile(filePath);

        if (names == null)
        {
            _log.LogError("Error reading names from the file.");
            return;
        }

        // Sort names based lastName -> Firstname
        List<string> sortedNames = _nameSorter.Sort(names);

        // Print and save sorted names
        _outputHandler.PrintAndSaveSortedNames(sortedNames);
    }
}
