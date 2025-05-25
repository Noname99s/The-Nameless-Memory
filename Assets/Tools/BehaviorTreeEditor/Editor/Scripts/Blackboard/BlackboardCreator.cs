using SOBehaviorTree;
using UnityEditor;
using UnityEngine;

public class BlackboardCreator
{
    public enum EBlackboardkey
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
            EBlackboardkey.ScriptPathKey.ToString(),
            EBlackboardkey.BlackboardNameKey.ToString(), 
            EBlackboardkey.AssetPathKey.ToString(), 
            EBlackboardkey.Guid.ToString()
            );
    }
    
    public void SetTreePath(string path)
    {
        treePath = path;
    }
    public void CreateBlackboard(string className)
    {
        if (EditorPrefs.GetBool(EBlackboardkey.Guid.ToString())) return;
        blackboardFactory.Create(scriptPath, className, assetMenu, "",treePath);
    }
    public ScriptableObject CreateBlackboard(BehaviorTree tree)
    {
        if (!EditorPrefs.GetBool(EBlackboardkey.Guid.ToString())) return null;
        EditorPrefs.SetBool(EBlackboardkey.Guid.ToString(), false);
        object blackboardObj = classFactory.Create(
                                GetPrefsValue(EBlackboardkey.BlackboardNameKey),
                                EBlackboardkey.ScriptPathKey.ToString(),
                                EBlackboardkey.AssetPathKey.ToString()
                                );
        SetBlackboardPath(tree);
        ScriptableObject result = blackboardObj as ScriptableObject;
        return result;
    }
    private void SetBlackboardPath(BehaviorTree tree)
    {
        if (tree.BlackboardPath != "") return;
        tree.SetBlackboardPath(GetPrefsValue(EBlackboardkey.AssetPathKey));
    }
    string GetPrefsValue(EBlackboardkey type)
    {
        return EditorPrefs.GetString(type.ToString());
    }
}
