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
        
        this.wordList2 = wordList2;
        
        this.checkpointFile = new FileInfo(Path.Combine(destinationFile.DirectoryName,
                                                        $"{Path.GetFileNameWithoutExtension(destinationFile.Name)}.checkpoint"));
        if (this.checkpointFile.Exists)
        {
            this.checkpointData = JsonSerializer.Deserialize<CheckpointData>(File.ReadAllText(this.checkpointFile.FullName));

            var quotient = Math.DivRem(this.checkpointData.Count, wordList1.Length, out long remainder);
            
            if (quotient > Int32.MaxValue) throw new Exception("Oväntat stort värde på variabeln");
            int toSkipInWordList1 = (int)quotient;
            this.wordList1 = wordList1[toSkipInWordList1..].ToArray();
            this.checkpointData.Count = remainder;
        }
        else
        {
            this.checkpointData = new CheckpointData();
            this.wordList1 = wordList1;
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
                    var secondsSinceLastCheckpoint = (checkpointStopTime - checkpointStartTime).TotalSeconds;
                    Console.WriteLine($"{checkpointStopTime} - {destinationFile.Name} - Gör {checkpointSize / secondsSinceLastCheckpoint:N0} test/sekund, har testat {iterations:N0} kombinationer ({((double)iterations / (double)totalPermutations):P2}) och hittat {approved.Count:N0} godkända efter {duration.Hours:D2}:{duration.Minutes:D2}:{duration.Seconds:D2}.");

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

                    await File.WriteAllTextAsync(this.checkpointFile.FullName, JsonSerializer.Serialize(new CheckpointData(iterations, secondsSinceLastCheckpoint)), token);

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
        Console.WriteLine($"{DateTime.Now} - {destinationFile.Name} - KLAR! Hittade {approved.Count()} godkända ord.");
        File.Move(tmpFile.FullName, this.destinationFile.FullName);
    }

    private string? CombineWords(string word1, string word2)
    {
        var result = new List<char>();

        var w1 = word1.ToCharArray();
        var w2 = word2.ToCharArray();
        for (int i = 0; i < word1.Length; i++)
        {
            if (w1[i] == w2[i])
            {
                result.Add(w1[i]);
            }
            else if (w1[i] == '*')
            {
                result.Add(w2[i]);
            }
            else if (w2[i] == '*')
            {
                result.Add(w1[i]);
            }
            else
            {
                return null;
            }           
        }
        
        return new string(result.ToArray());
    }
}