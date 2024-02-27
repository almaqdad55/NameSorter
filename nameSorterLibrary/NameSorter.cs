
using nameSorterLibrary.Interfaces;

namespace nameSorterLibrary;

public class NameSorter : INameSorter
{
    public List<string> Sort(List<string> names)
    {
        return names
            .OrderBy(name => name.Split(' ').Last())
            .ThenBy(name => string.Join(" ", name.Split(' ').TakeWhile(part => part != name.Split(' ').Last())))
            .ToList();
    }
}

