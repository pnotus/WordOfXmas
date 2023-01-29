using System.Text.Json;

public class WordApprover
{
    private readonly string[] wordList1;
    private readonly string[] wordList2;
    private readonly FileInfo destinationFile;
    private readonly FileInfo checkpointFile;
    private readonly FileInfo tmpFile;
    private HashSet<string> approved = new HashSet<string>();
    private int checkpointSize = 50000000;
    private CheckpointData checkpointData;
    private readonly IEnumerable<PointCalculator> pointCalculators;

    public WordApprover(string[] wordList1, string[] wordList2, FileInfo destinationFile, int[] wordIndexes)
    {
        this.pointCalculators = wordIndexes.Select(index => new PointCalculator(index));
        this.destinationFile = destinationFile;
        this.tmpFile = new FileInfo(Path.Combine(destinationFile.DirectoryName, destinationFile.Name + ".tmp"));

        if (tmpFile.Exists)
        {
            approved = File.ReadLines(tmpFile.FullName).ToHashSet();
        }
        
        this.wordList1 = wordList1;
        this.wordList2 = wordList2;
        
        this.checkpointFile = new FileInfo(Path.Combine(destinationFile.DirectoryName,
                                                        $"{Path.GetFileNameWithoutExtension(destinationFile.Name)}.checkpoint"));
        if (this.checkpointFile.Exists)
        {
            this.checkpointData = JsonSerializer.Deserialize<CheckpointData>(File.ReadAllText(this.checkpointFile.FullName));
        }
        else
        {
            this.checkpointData = new CheckpointData();
        }
        
    }

    public async Task WriteApprovedAsync(CancellationToken token) 
    {
        var totalPermutations = (long)wordList1.Length * wordList2.Length;
        Console.WriteLine($"{DateTime.Now} - {destinationFile.Name} - Börjar leta giltiga ord bland {totalPermutations:N0} möjliga kombinationer. Tidigare körningar hade redan hittat {approved.Count:N0} ord på {this.checkpointData.Count:N0} iterationer.");
        
        long iterations = 0;
        
        var totalStartTime = DateTime.Now;
        var checkpointStartTime = totalStartTime;

        var oldTmpFile = new FileInfo(tmpFile.FullName + ".old");
        var newTmpFile = new FileInfo(tmpFile.FullName + ".new");

        foreach (var p0 in wordList1)
        {
            foreach (var p1 in wordList2)
            {
                iterations += 1;

                if (this.checkpointData.Count > 0)
                {
                    this.checkpointData.Count -= 1;
                    continue;
                }

                if (iterations % checkpointSize == 0)
                {
                    var checkpointStopTime = DateTime.Now;
                    var duration = checkpointStopTime - totalStartTime;
                    Console.WriteLine($"{checkpointStopTime} - {destinationFile.Name} - Gör {checkpointSize / (checkpointStopTime - checkpointStartTime).TotalSeconds:N0} test/sekund, har testat {iterations:N0} kombinationer ({((double)iterations / (double)totalPermutations):P2}) och hittat {approved.Count:N0} godkända efter {duration.Hours:D2}:{duration.Minutes:D2}:{duration.Seconds:D2}.");

                    tmpFile.Refresh();
                    if (tmpFile.Exists)
                    {
                        File.Move(tmpFile.FullName, oldTmpFile.FullName);
                    }
                    await File.WriteAllLinesAsync(newTmpFile.FullName, approved, token);
                    File.Move(newTmpFile.FullName, tmpFile.FullName);

                    oldTmpFile.Refresh();
                    if (oldTmpFile.Exists)
                    {
                        File.Delete(oldTmpFile.FullName);
                    }

                    await File.WriteAllTextAsync(this.checkpointFile.FullName, JsonSerializer.Serialize(new CheckpointData(iterations)), token);

                    checkpointStartTime = DateTime.Now;
                }

                var combination = CombineWords(p0, p1);

                if (combination == null)
                {
                    continue;
                }

                if (pointCalculators.All(calc => calc.IsApproved(combination)))
                {
                    approved.Add(combination);
                }
            }
        }
        Console.WriteLine($"{DateTime.Now} - {destinationFile.Name} - KLAR! Hittade {approved.Count()} gorkända ord.");
        File.Move(tmpFile.FullName, this.destinationFile.FullName);
    }

    private string? CombineWords(string word1, string word2)
    {
        var charCombinations = word1.ToCharArray().Zip(word2.ToCharArray(), (first, second) => (first, second));

        var result = new List<char>();

        foreach (var c in charCombinations)
        {
            if (c.Item1 == c.Item2)
            {
                result.Add(c.Item1);
            }
            else if (c.Item1 == '*')
            {
                result.Add(c.Item2);
            }
            else if (c.Item2 == '*')
                result.Add(c.Item1);
            else
            {
                return null;
            }
        }

        return new string(result.ToArray());
    }
}