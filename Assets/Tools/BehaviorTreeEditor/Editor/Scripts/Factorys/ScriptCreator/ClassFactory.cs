using System.IO;
using System;
using SOBehaviorTree;
using UnityEditor;
using UnityEngine;
using System.Text;
using System.Reflection;
using PlasticPipe.PlasticProtocol.Messages;

public class ClassFactory : IClassFactory
{
    StringBuilder className = new StringBuilder();
    StringBuilder path = new StringBuilder();
    StringBuilder assetPath = new StringBuilder();
    public object Create(string className, string scriptPathKey, string assetPathKey)
    {
        SetNodeNameNPath(scriptPathKey, assetPathKey);
        this.className.Append(className);
        if (IsNullNameNPath(className.ToString(), path.ToString())) return null;
        return CreateNode();
    }
    private void SetNodeNameNPath(string scriptPathKey,string assetPathKey)
    {
        path.Clear();
        assetPath.Clear();
        path.Append(EditorPrefs.GetString(scriptPathKey, null));
        assetPath.Append(EditorPrefs.GetString(assetPathKey, null));

    }
    private bool IsNullNameNPath(string className, string path)
    {
        if (!string.IsNullOrEmpty(className)) return false;
        if (!string.IsNullOrEmpty(path)) return false;
        return true;
    }
    private object CreateNode()
    {
        Type executeType = FindTypebyName(className.ToString());
        if (executeType == null || !typeof(ScriptableObject).IsAssignableFrom(executeType)) return null;
        if (!typeof(ScriptableObject).IsAssignableFrom(executeType)) return null;

        object scriptClass = ScriptableObject.CreateInstance(executeType) as object;
        CreateNodeAsset(scriptClass);
        return scriptClass;
    }

    private void CreateNodeAsset(object nodeObj)
    {
        UnityEngine.Object assetObject = nodeObj as UnityEngine.Object;
        AssetDatabase.CreateAsset(assetObject, assetPath.ToString());
        AssetDatabase.SaveAssets();
    }
    private Type FindTypebyName(string className)
    {
        Assembly[] assembles = AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assembles.Length; i++)
        {
            Type type = assembles[i].GetType(className);
            if (type == null) continue;
            return type;
        }
        return null;
    }
}