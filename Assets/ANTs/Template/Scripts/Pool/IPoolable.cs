namespace ANTs.Template
{
    public interface IPoolable
    {
        void WakeUp(object param);
        void Sleep();
    }
}