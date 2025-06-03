using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateMVPScript : EditorWindow
{
    [MenuItem("UIEditor/Create MVP")]
    public static void ShowWindow()
    {
        GetWindow<CreateMVPScript>("Create MVP");
    }
    string className = "";
    private void OnGUI()
    {
        GUILayout.Label("Create MVP Script", EditorStyles.boldLabel);
        className = EditorGUILayout.TextField("Class Name", className);
        if (GUILayout.Button("Create"))
        {
            if (string.IsNullOrEmpty(className))
            {
                EditorUtility.DisplayDialog("Error", "Class name cannot be empty.", "OK");
                return;
            }
            CreateScript();
        }
    }
    void CreateScript()
    {
        string folderPath = EditorUtility.OpenFolderPanel(
            "Select Folder for MVP Script",
            "Assets/Scripts/UI/UIControl",
            ""
        );
        if (string.IsNullOrEmpty(folderPath))
        {
            Debug.LogError("No folder selected.");
            return;
        }

        //Create classname folder
        string classFolderPath = Path.Combine(folderPath, className);
        if (!Directory.Exists(classFolderPath))
        {
            Directory.CreateDirectory(classFolderPath);
        }
        string modelPath = Path.Combine(classFolderPath, className + "Model.cs");
        string presenterPath = Path.Combine(classFolderPath, className + "Presenter.cs");
        string viewInterfacePath = Path.Combine(classFolderPath, "I" + className + "View.cs");
        string viewPath = Path.Combine(classFolderPath, className + "View.cs");
        string compositionPath = Path.Combine(classFolderPath, className + "Composition.cs");

        File.WriteAllText(modelPath, GetModelTemplate(className));
        File.WriteAllText(presenterPath, GetPresenterTemplate(className));
        File.WriteAllText(viewInterfacePath, GetViewInterfaceTemplate(className));
        File.WriteAllText(viewPath, GetViewTemplate(className));
        File.WriteAllText(compositionPath, GetCompositionTemplate(className));

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", "MVP scripts created successfully.", "OK");
        className = "";
    }
    #region Templates
    string GetModelTemplate(string name)
    {
        return
$@"using System.Collections.Generic;
using UnityEngine;

public class {name}Model
{{
    public {name}Model()
    {{
    }}
}}";
    }
    string GetPresenterTemplate(string name)
    {
        return
$@"using UnityEngine;

public class {name}Presenter
{{
    I{name}View view;
    {name}Model model;

    public {name}Presenter(I{name}View view)
    {{

    }}
    public void Dispose()
    {{
        
    }}
}}";
    }
    string GetViewInterfaceTemplate(string name)
    {
        return
$@"using System;
using UnityEngine;

public interface I{name}View
{{

}}";
    }
    string GetViewTemplate(string name)
    {
        return
$@"using UnityEngine;
using System;
using System.Collections.Generic;

public class {name}View : MonoBehaviour , I{name}View
{{
    void Start()
    {{
    }}
    void OnDestroy()
    {{
    }}
}}";
    }
    string GetCompositionTemplate(string name)
    {
        return
$@"using UnityEngine;

public class {name}Composition : UIPopup
{{
    [SerializeField] private {name}View view;
    private {name}Presenter presenter;
    private void Awake()
    {{
        presenter = new {name}Presenter(view);
    }}
    private void OnDestroy()
    {{
        presenter.Dispose();
    }}
}}";
    }
    #endregion
}
