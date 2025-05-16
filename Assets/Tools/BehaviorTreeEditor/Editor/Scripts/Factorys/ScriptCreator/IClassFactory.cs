public interface IClassFactory
{
    public object Create(string className, string scriptPathKey, string assetPathKey);
}