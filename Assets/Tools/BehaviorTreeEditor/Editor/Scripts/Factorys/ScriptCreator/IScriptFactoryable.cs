
namespace SOBehaviorTree
{
    public interface IScriptFactoryable
    {
        public void Create(string path, string className, string assetMenu, string blackboardName,string treepath="");
    }
}
