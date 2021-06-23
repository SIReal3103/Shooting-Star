namespace ANTs.Template
{
    [System.Serializable]
    public enum ProgressId
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        None
    }
    public interface IProgressable
    {
        ProgressId CurrentLevel { get; set; }
        ProgressId NextLevel { get; set; }
    }
}