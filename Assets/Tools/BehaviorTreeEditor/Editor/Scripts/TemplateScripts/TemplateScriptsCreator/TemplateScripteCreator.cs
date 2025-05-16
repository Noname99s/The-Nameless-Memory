using SOBehaviorTree;
using System.Text;
using TreeEditor;
public abstract class TemplateScripteCreator
{
    protected StringBuilder className = new StringBuilder();
    StringBuilder blackboardName = new StringBuilder();
    protected IScriptFactoryable nodeFactory;
    public virtual void CreateClass(StringBuilder className,StringBuilder blackboardName, string treePath)
    {
        this.className = className;
        this.blackboardName = blackboardName;
    }
    protected virtual void WriteFile(string scriptPath, string menuPath, string treePath)
    {
        SetFactoryKey();
        nodeFactory.Create(scriptPath, className.ToString(), menuPath, blackboardName.ToString(), treePath);
    }
    public void SetFactoryKey()
    {
        nodeFactory = new ScriptFactory(
            NodeKeyType.NodeScriptPathKey.ToString(),
            NodeKeyType.NodeClassNameKey.ToString(),
            NodeKeyType.NodeAssetPathKey.ToString(),
            NodeKeyType.NodeGuid.ToString()
            );
    }
}