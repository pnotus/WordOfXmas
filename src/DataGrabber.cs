using Microsoft.Extensions.FileSystemGlobbing;

public class DataGrabber 
{
    public DirectoryInfo DataDirectoryPath { get; }
    public string FileNamePattern { get; }

    public DataGrabber(string dataDirectoryPath, string fileNamePattern)
    {
        DataDirectoryPath = new DirectoryInfo(dataDirectoryPath);
        FileNamePattern = fileNamePattern;
    }

    public void CombineFiles(string fileName) 
    {
        Matcher matcher = new();
        matcher.AddInclude(FileNamePattern);

        IEnumerable<string> matchingFiles = matcher.GetResultsInFullPath(DataDirectoryPath.FullName);
        
        var data = new List<string>();

        foreach (var file in matchingFiles)
        {
            data.AddRange(File.ReadAllLines(file));
        }

        File.WriteAllLines(Path.Combine(DataDirectoryPath.FullName, fileName), data.Distinct());
    }
}