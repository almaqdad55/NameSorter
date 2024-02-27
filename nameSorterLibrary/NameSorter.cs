
using nameSorterLibrary.Interfaces;
namespace nameSorterLibrary;

public class NameSorter : INameSorter
{

    public List<string> Sort(List<string> names)
    {
        var sortedNames = names
            .Select(name =>
            {
                var lastName = name.Split(' ').Last();
                var firstNames = name.Substring(0, name.Length - lastName.Length).Trim();
                return new
                {
                    FullName = name,
                    LastName = lastName,
                    FirstNames = firstNames
                };
            })
            .OrderBy(nameInfo => nameInfo.LastName)
            .ThenBy(nameInfo => nameInfo.FirstNames)
            .Select(nameInfo => nameInfo.FullName)
            .ToList();

        return sortedNames;
    }
}

