
using Utilities.Interfaces;
namespace Utilities;

public class NameSorter : INameSorter
{
    public List<string> Sort(List<string> names)
    {
        var sortedNames = names
            .Select(name =>
            {
                // Split the name into last name and first names
                var lastName = name.Split(' ').Last();
                var firstNames = name.Substring(0, name.Length - lastName.Length).Trim();

                // Create new anonymous object containing full name, last name, and first names
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