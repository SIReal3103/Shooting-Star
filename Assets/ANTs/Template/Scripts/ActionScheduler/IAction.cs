namespace ANTs.Template
{
    public interface IAction
    {
        bool IsActionActive { get; }
        void ActionStart();
        void ActionStop();
    }
}