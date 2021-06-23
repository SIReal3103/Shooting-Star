namespace ANTs.Template
{
    [System.Serializable]
    public enum ProgressIdentifier
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
        ProgressIdentifier CurrentLevel { get; }
        ProgressIdentifier NextLevel { get; }
    }
}