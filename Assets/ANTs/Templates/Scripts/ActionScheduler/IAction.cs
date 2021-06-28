namespace ANTs.Template
{
    public interface IAction
    {
        bool IsActionActive { get; set; }
        void ActionStart();
        void ActionStop();
    }
}