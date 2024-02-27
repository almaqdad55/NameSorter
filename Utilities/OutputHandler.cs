using Microsoft.Extensions.Logging;
using Utilities.Interfaces;

namespace Utilities;

public class OutputHandler : IOutputHandler
{
    private readonly ILogger<OutputHandler> _log;

    public OutputHandler(ILogger<OutputHandler> log)
    {
        _log = log;
    }

    // Print and save sorted names
    public void PrintAndSaveSortedNames(List<string> sortedNames)
    {
        foreach (var name in sortedNames)
        {
            Console.WriteLine(name);
        }

        try
        {
            File.WriteAllLines("sorted-names-list.txt", sortedNames);
        }
        catch (Exception ex)
        {
            _log.LogError("An error occurred while saving the names to a file: " + ex.Message);
        }

    }
}