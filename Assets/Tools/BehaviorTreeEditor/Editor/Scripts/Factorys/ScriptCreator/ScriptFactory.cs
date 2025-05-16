using System.Collections;
using System.Collections.Generic;
using SOBehaviorTree;
using UnityEditor;
using UnityEngine;
using System.IO;
public class ScriptFactory : IScriptFactoryable
{
    string scriptPathKey;
    string classNameKey;
    string assetPathKey;
    string guidKey;
    public ScriptFactory(string scriptPath, string className, string assetPath, string guid)
    {
        scriptPathKey = scriptPath;
        classNameKey = className;
        assetPathKey = assetPath;
        this.guidKey = guid;
    }
    public void Create(string scriptPath, string className, string assetMenu, string blackboardName, string treePath = "")
    {
        string defaultPath = Application.dataPath + treePath;
        string path = EditorUtility.SaveFilePanel("파일 저장", defaultPath, className.ToString() + ".cs", ".cs");
        string script = File.ReadAllText(scriptPath);
        script = script.Replace("#FileName#", className.ToString());
        script = script.Replace("#MenuName#", assetMenu + className);
        script = script.Replace("#SCRIPTNAME#", className.ToString());
        script = script.Replace("#Blackboard#", blackboardName + " blackboard;");
        File.WriteAllText(path, script);

        string assetPath = path;
        assetPath = assetPath.Replace(Application.dataPath, "Assets/");
        assetPath = assetPath.Replace(".cs", ".asset");
        EditorPrefs.SetString(scriptPathKey, path);
        EditorPrefs.SetString(classNameKey, className.ToString());
        EditorPrefs.SetString(assetPathKey, assetPath);
        EditorPrefs.SetBool(guidKey, true);
        AssetDatabase.Refresh();
    }
}
