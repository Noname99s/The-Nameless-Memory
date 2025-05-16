using SOBehaviorTree;
using UnityEditor;
using UnityEngine;

public class BlackboardCreator
{
    enum key
    {
        ScriptPathKey,
        BlackboardNameKey,
        AssetPathKey,
        Guid,
    }
    string treePath = Application.dataPath;
    string scriptPath = "Assets/Tools/BehaviorTreeEditor/Editor/TemplateScripts/BlackboardScript.txt";
    string assetMenu = "BehaviorNodes/Blackboard/";

    IScriptFactoryable blackboardFactory;
    IClassFactory classFactory= new ClassFactory();

    public BlackboardCreator() 
    {
        blackboardFactory = new ScriptFactory(
            key.ScriptPathKey.ToString(),
            key.BlackboardNameKey.ToString(), 
            key.AssetPathKey.ToString(), 
            key.Guid.ToString()
            );
    }
    
    public void SetTreePath(string path)
    {
        treePath = path;
    }
    public void CreateBlackboard(string className)
    {
        if (EditorPrefs.GetBool(key.Guid.ToString())) return;
        blackboardFactory.Create(scriptPath, className, assetMenu, "",treePath);
    }
    public ScriptableObject GetBlackboard()
    {
        if (!EditorPrefs.GetBool(key.Guid.ToString())) return null;
        EditorPrefs.SetBool(key.Guid.ToString(), false);
        object blackboardObj = classFactory.Create(
                                GetPrefsValue(key.BlackboardNameKey),
                                key.ScriptPathKey.ToString(),
                                key.AssetPathKey.ToString()
                                );

        ScriptableObject result = blackboardObj as ScriptableObject;
        return result;
    }
    string GetPrefsValue(key type)
    {
        return EditorPrefs.GetString(type.ToString());
    }
}
