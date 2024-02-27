using Xunit;
using Moq;
using nameSorterLibrary;
using nameSorterLibrary.Interfaces;
using name_sorter;

public class NameSorterTests
{
    [Fact]
    public void FileReader_ShouldReturnListOfNames()
    {
        // Arrange
        var fileReader = new FileReader();
        string testFilePath = "test.txt"; // Ensure this file exists with test data

        // Act
        var result = fileReader.ReadNamesFromFile(testFilePath);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void NameSorter_ShouldSortNamesCorrectly()
    {
        // Arrange
        var nameSorter = new NameSorter();
        var names = new List<string> { "Jane Doe", "John Smith", "Alice Johnson" };

        // Act
        var result = nameSorter.Sort(names);

        // Assert
        var expected = new List<string> { "Jane Doe", "Alice Johnson", "John Smith" };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void OutputHandler_ShouldPrintAndSaveNames()
    {
        // Arrange
        var mockOutputHandler = new Mock<IOutputHandler>();
        var sortedNames = new List<string> { "Alice Johnson", "Jane Doe", "John Smith" };

        // Act
        mockOutputHandler.Object.PrintAndSaveSortedNames(sortedNames);

        // Assert
        // Here you would assert that the file was created and contains the expected content
        // This might involve reading the file and comparing its contents to `sortedNames`
    }

    [Fact]
    public void NameSorterApp_ShouldRunSuccessfully()
    {
        // Arrange
        var mockFileReader = new Mock<IFileReader>();
        var mockNameSorter = new Mock<INameSorter>();
        var mockOutputHandler = new Mock<IOutputHandler>();
        var testFilePath = "test.txt";
        var names = new List<string> { "Jane Doe", "John Smith" };

        mockFileReader.Setup(m => m.ReadNamesFromFile(testFilePath)).Returns(names);
        mockNameSorter.Setup(m => m.Sort(It.IsAny<List<string>>())).Returns(names);

        var app = new NameSorterApp(mockFileReader.Object, mockNameSorter.Object, mockOutputHandler.Object);

        // Act
        app.Run(testFilePath);

        // Assert
        mockFileReader.Verify(m => m.ReadNamesFromFile(testFilePath), Times.Once);
        mockNameSorter.Verify(m => m.Sort(names), Times.Once);
        mockOutputHandler.Verify(m => m.PrintAndSaveSortedNames(names), Times.Once);
    }
}
