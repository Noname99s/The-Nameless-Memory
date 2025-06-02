namespace Orpul1
{
    public interface ISetDataAble<T>
    {
        public void SetValue(T t);
    }
    public interface ISetDataAble<T1, T2>
    {
        public void SetValue(T1 t1, T2 t2);
    }
}