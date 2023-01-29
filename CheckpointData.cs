public class CheckpointData
{
    public long Count { get; set; }

    public CheckpointData()
    {
        Count = 0;
    }

    public CheckpointData(long count)
    {
        Count = count;
    }
}