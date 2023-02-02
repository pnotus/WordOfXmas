public class PointCalculator
{
    private readonly string[] words = {
        "HYACINTER", //    30 408
        "TOMTELUVA", //   638 078
        "PLASTGRAN", //   638 078
        "LUSSEBRUD", //   334 552
        "RENSKJUTS", // 2 424 953
        "KALLEANKA", //   214 295
        "LJUSSTAKE", // 2 250 381
    };

    private readonly double[] points = {
        1.5,
        2.5,
        2.5,
        2.5,
        3.5,
        4,
        4,
    };

    private readonly string originalWord;

    public string OriginalWord { get { return originalWord; } }
    public double ExcpectedPoints { get; }

    public PointCalculator(int wordIndex)
    {
        this.originalWord = words[wordIndex];
        this.ExcpectedPoints = points[wordIndex];
    }

    public bool IsApproved(string wordToEvaluate)
    {
        var word1 = OriginalWord.ToList();
        var word2 = wordToEvaluate.ToList();

        var calculatedPoints = 0.0;
        
        var identicalChars = word1.Zip(word2).Where(c => c.Item1 == c.Item2).Select(c => c.Item1).ToArray();
        calculatedPoints += identicalChars.Count();

        foreach (var c in identicalChars)
        {
            word1.Remove(c);
            word2.Remove(c);
        }

        foreach (var c in word1)
        {
            if (word2.Contains(c))
            {
                calculatedPoints += 0.5;
                word2.Remove(c);
            }
        }
        
        return calculatedPoints == this.ExcpectedPoints;
    }   
}
