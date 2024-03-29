﻿using Moq;
using Utilities;
using Utilities.Interfaces;
using name_sorter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

public class NameSorterTests
{
    private readonly string expectedOutputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "expectedOutput.txt");

    [Fact]
    public void FileReader_ShouldReturnListOfNames()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<FileReader>>();
        var fileReader = new FileReader(mockLogger.Object);
        string testFilePath = "test.txt";

        // Act
        var result = fileReader.ReadNamesFromFile(testFilePath);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

       Console.WriteLine(expectedOutputFilePath);
    }

    [Fact]
    public void NameSorter_ShouldSortNamesCorrectly()
    {
        // Arrange
        var nameSorter = new NameSorter();
        var names = new List<string> { "Beau Tristan Bentley", "Marin Alvarez", "Adonis Julius Archer" };

        // Act
        var result = nameSorter.Sort(names);

        // Assert
        var expected = new List<string> { "Marin Alvarez", "Adonis Julius Archer", "Beau Tristan Bentley" };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void PrintAndSaveSortedNames_SavesCorrectContent()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<OutputHandler>>();
        var outputHandler = new OutputHandler(mockLogger.Object);
        var sortedNames = new List<string> { "Jane Doe", "John Doe" };
        var expectedFilePath = "sorted-names-list.txt";

        // Act
        outputHandler.PrintAndSaveSortedNames(sortedNames);

        // Assert
        var writtenContent = File.ReadAllLines(expectedFilePath);
        Assert.Equal(sortedNames, writtenContent);

        // Cleanup
        if (File.Exists(expectedFilePath))
        {
            File.Delete(expectedFilePath);
        }
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

        var mockLogger = new Mock<ILogger<NameSorterApp>>();
        var app = new NameSorterApp(mockFileReader.Object, mockNameSorter.Object, mockOutputHandler.Object, mockLogger.Object);

        // Act
        app.Run(testFilePath);

        // Assert
        mockFileReader.Verify(m => m.ReadNamesFromFile(testFilePath), Times.Once);
        mockNameSorter.Verify(m => m.Sort(names), Times.Once);
        mockOutputHandler.Verify(m => m.PrintAndSaveSortedNames(names), Times.Once);
    }
}
