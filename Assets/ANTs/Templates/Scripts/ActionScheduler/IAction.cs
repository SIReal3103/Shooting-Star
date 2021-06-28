namespace ANTs.Template
{
    public interface IAction
    {
        bool IsActionStart { get; set; }
        void ActionStart();
        void ActionStop();
    }
}