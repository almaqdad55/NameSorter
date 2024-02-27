using nameSorterLibrary.Interfaces;

namespace nameSorterLibrary;

public class OutputHandler : IOutputHandler
{
    public void PrintAndSaveSortedNames(List<string> sortedNames)
    {
        foreach (var name in sortedNames)
        {
            Console.WriteLine(name);
        }

        File.WriteAllLines("sorted-names-list.txt", sortedNames);
    }
}
