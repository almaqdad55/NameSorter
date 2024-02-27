using nameSorterLibrary.Interfaces;

namespace name_sorter;

public class NameSorterApp
{
    private readonly IFileReader _fileReader;
    private readonly INameSorter _nameSorter;
    private readonly IOutputHandler _outputHandler;

    public NameSorterApp(IFileReader fileReader, INameSorter nameSorter, IOutputHandler outputHandler)
    {
        _fileReader = fileReader;
        _nameSorter = nameSorter;
        _outputHandler = outputHandler;
    }

    public void Run(string filePath)
    {
        List<string> names = _fileReader.ReadNamesFromFile(filePath);

        if (names == null)
        {
            Console.WriteLine("Error reading names from the file.");
            return;
        }

        List<string> sortedNames = _nameSorter.Sort(names);
        _outputHandler.PrintAndSaveSortedNames(sortedNames);

        Console.WriteLine("Names sorted successfully!");

    }
}
