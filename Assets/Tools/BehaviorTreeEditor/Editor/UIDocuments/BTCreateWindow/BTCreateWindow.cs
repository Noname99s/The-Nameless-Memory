using Codice.CM.Common.Tree;
using Unity.VisualScripting;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text;


public class BTCreateWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    StringBuilder titleName = new StringBuilder();
    VisualElement root;
    Label titleLabel;
    Button confirmBtn, quitBtn;
    TextField className;
    Action<Type, string> createNode;
    Type classType;
    
    public BTCreateWindow(Type classType, Action<Type, string> createNode)
    {
        this.createNode = createNode;
        this.classType = classType;
        root = rootVisualElement;
        titleName.Append(classType.Name);

        SetGUI();
    }
    private void SetGUI()
    {
        SetVisualTree();
        SetEditorComponent();
    }
    private void SetVisualTree()
    {
        BTCreateWindow wnd = GetWindow<BTCreateWindow>();
        wnd.titleContent = new GUIContent("BTCreateWindow");
        var uxmlData = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Tools/BehaviorTreeEditor/Editor/UIDocuments/BTCreateWindow/BTCreateWindow.uxml");
        uxmlData.CloneTree(root);
    }
    private void SetEditorComponent()
    {
        titleLabel = root.Q<Label>("TitleLabel");
        titleLabel.text = titleName.ToString();
        className = root.Q<TextField>("TextClassName");
        confirmBtn = root.Q<Button>("ConfirmBtn");
        quitBtn = root.Q<Button>("CancelBtn");
        confirmBtn.clicked += OnConfirmBtn;
        quitBtn.clicked += OnCancleBtn;

        titleName.Clear();
    }
    private void OnConfirmBtn()
    {
        createNode(classType, className.text);
        this.Close();
    }
    private void OnCancleBtn()
    {
        this.Close();

    }
}
