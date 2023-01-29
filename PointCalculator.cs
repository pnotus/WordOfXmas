public class PointCalculator
{
    private string[] originalWords = {
        "HYACINTER", //    30 408
        "TOMTELUVA", //   638 078
        "PLASTGRAN", //   638 078
        "LUSSEBRUD", //   334 552
        "RENSKJUTS", // 2 424 953
        "KALLEANKA", //   214 295
        "LJUSSTAKE", // 2 250 381
    };

    private double[] originalWordPoints = {
        1.5,
        2.5,
        2.5,
        2.5,
        3.5,
        4,
        4,
    };

    private readonly int originalWordIdx;

    public PointCalculator(int originalWordIdx)
    {
        this.originalWordIdx = originalWordIdx;
    }

    public bool Calculate(string word)
    {
        var word1 = originalWords[this.originalWordIdx].ToList();
        var word2 = word.ToList();

        var points = 0.0;
        
        var identicalChars = word1.Zip(word2).Where((c1, c2) => c1.Equals(c2)).Select((c) => c.Item1);
        points += identicalChars.Count();

        foreach (var c in identicalChars)
        {
            word1.Remove(c);
            word2.Remove(c);
        }

        foreach (var c in word1)
        {
            if (word2.Contains(c))
            {
                points += 0.5;
                word2.Remove(c);
            }
        }
        
        return points == this.originalWordPoints[this.originalWordIdx];
    }   
}
