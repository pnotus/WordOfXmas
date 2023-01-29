public class WordApprover
{
    private readonly string[] wordList1;
    private readonly string[] wordList2;

    public int Size { get; }

    private readonly DirectoryInfo dataDirectory;
    private readonly string fileName;
    private readonly int[] wordNumbers;

    public WordApprover(string[] wordList1, string[] wordList2, int noSplits, string dataDirectory, string fileName, int[] wordNumbers)
    {
        this.wordList1 = wordList1;
        this.wordList2 = wordList2;
        Size = this.wordList2.Length / noSplits + 1;
        this.dataDirectory = new DirectoryInfo(dataDirectory);
        this.fileName = fileName;
        this.wordNumbers = wordNumbers;
    }

    public void WriteApproved() 
    {
        Parallel.ForEach(wordList2.Chunk(Size).Select((words, index) => new {Index = index, Words = words}), chunk => 
        {
            var totalPermutations = chunk.Words.Length * (long)wordList1.Length;
            Console.WriteLine($"{DateTime.Now} - Börjar leta giltiga ord bland {totalPermutations:N} möjliga kombinationer");
            var approved = new HashSet<string>();
            long iterations = 0;
            var resultFile = new FileInfo(Path.Combine(dataDirectory.FullName, $"{Path.GetFileNameWithoutExtension(fileName)}_Part{chunk.Index:D2}{Path.GetExtension(fileName)}"));
            Console.WriteLine(resultFile);
            var checkpointSize = 25000000;
            
            var totalStartTime = DateTime.Now;
            var checkpointStartTime = totalStartTime;

            foreach (var p0 in wordList1)
            {
                foreach (var p1 in chunk.Words)
                {
                    iterations += 1;
                    if (iterations % checkpointSize == 0)
                    {
                        var checkpointStopTime = DateTime.Now;
                        var duration = checkpointStopTime - totalStartTime;
                        Console.WriteLine($"{checkpointStopTime} - {resultFile.Name} - Gör {checkpointSize / (checkpointStopTime - checkpointStartTime).TotalSeconds:N0} test/sekund, har testat {iterations:N} kombinationer ({((double)iterations / (double)totalPermutations):P2}) och hittat {approved.Count:N} godkända efter {duration.Hours:D2}:{duration.Minutes:D2}:{duration.Seconds:D2}.");

                        checkpointStartTime = DateTime.Now;
                    }

                    var combination = CombineWords(p0, p1);

                    if (combination == null)
                    {
                        continue;
                    }

                    // Lägg till poängkontroll
                    foreach (var number in wordNumbers)
                    {

                    }

                    approved.Add(combination);
                }
            }
        });
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