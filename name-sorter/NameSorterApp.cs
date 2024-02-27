using Microsoft.Extensions.Logging;
using nameSorterLibrary;
using nameSorterLibrary.Interfaces;

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

    public void Run(string filePath)
    {
        List<string> names = _fileReader.ReadNamesFromFile(filePath);

        if (names == null)
        {
            _log.LogError("Error reading names from the file.");
            return;
        }

        List<string> sortedNames = _nameSorter.Sort(names);
        _outputHandler.PrintAndSaveSortedNames(sortedNames);


        _log.LogInformation("Names sorted successfully!");

    }
}
