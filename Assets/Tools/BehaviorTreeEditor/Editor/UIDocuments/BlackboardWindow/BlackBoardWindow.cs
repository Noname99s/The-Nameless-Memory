using System;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackBoardWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    private VisualElement root;
    private Label titleLabel;
    private TextField nameTextField;
    private Button confirmBtn, quitBtn;

    Action<string> setBlackboard;
    string titleName = "Blackboard Create";
    
    public BlackBoardWindow(Action<string> SetBlackboard)
    {
        this.setBlackboard = SetBlackboard;
        root = rootVisualElement;
        SetGUI();
    }
    private void SetGUI()
    {
        SetVisualTree();
        SetEditorComponent();
    }
    private void SetVisualTree()
    {
        BlackBoardWindow wnd = GetWindow<BlackBoardWindow>();
        wnd.titleContent = new GUIContent("BlackBoardWindow");
        var uxmlData = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Tools/BehaviorTreeEditor/Editor/UIDocuments/BlackboardWindow/BlackBoardWindow.uxml");
        uxmlData.CloneTree(root);
    }
    private void SetEditorComponent()
    {
        titleLabel = root.Q<Label>("TitleLabel");
        titleLabel.text = titleName;
        nameTextField = root.Q<TextField>("TextClassName");
        confirmBtn = root.Q<Button>("ConfirmBtn");
        quitBtn = root.Q<Button>("CancelBtn");
        confirmBtn.clicked += OnConfirmBtn;
        quitBtn.clicked += OnCancleBtn;
    }

    private void OnConfirmBtn()
    {
        setBlackboard(nameTextField.text);
        this.Close();
    }
    private void OnCancleBtn()
    {
        this.Close();
    }
}
