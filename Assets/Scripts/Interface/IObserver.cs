namespace Orpul1
{
    public interface IObserver
    {
        public void Notify<T>(T t);
    }
}