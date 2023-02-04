public class CheckpointData
{
    public long Count { get; set; }
    public double SecondsSinceLastCheckpoint { get; }

    public CheckpointData()
    {
        Count = 0;
        SecondsSinceLastCheckpoint = -1;
    }

    public CheckpointData(long count, double secondsSinceLastCheckpoint)
    {
        Count = count;
        SecondsSinceLastCheckpoint = secondsSinceLastCheckpoint;
    }
}