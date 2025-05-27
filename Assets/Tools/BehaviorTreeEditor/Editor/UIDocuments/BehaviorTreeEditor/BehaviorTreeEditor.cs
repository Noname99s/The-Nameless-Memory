using SOBehaviorTree;
using Unity.Plastic.Antlr3.Runtime.Tree;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeEditor : EditorWindow
{
    BehaviorTreeView treeView;
    BehaviorInspectorView inspectorView;
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("UIEditor/BehaviorTreeEditor")]
    public static void OpenBehaviorEditor()
    {
        BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviorTreeEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        SetTree(root);
        SetEditorView(root);
    }
    private void OnInspectorUpdate()
    {
        if (treeView == null) return;
        treeView.UpdateNodeState();
    }
    private void OnSelectionChange()
    {
        SOBehaviorTree.BehaviorTree tree = Selection.activeObject as SOBehaviorTree.BehaviorTree;

        tree = GetSelectedBehaviorTree(tree);

        PlayStateTree(tree);
#if UNITY_EDITOR
        EditStateTree(tree);
#endif
    }
    void SetTree(VisualElement root)
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Tools/BehaviorTreeEditor/Editor/UIDocuments/BehaviorTreeEditor/BehaviorTreeEditor.uxml");
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Tools/BehaviorTreeEditor/Editor/UIDocuments/BehaviorTreeEditor/BehaviorTreeEditor.uss");
        root.styleSheets.Add(styleSheet);
    }
    void SetEditorView(VisualElement root)
    {
        treeView = root.Q<BehaviorTreeView>();

        treeView.focusable = true;
        treeView.Focus();
        inspectorView = root.Q<BehaviorInspectorView>();

        treeView.OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }
    void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSelection(node);
    }

    
    BehaviorTree GetSelectedBehaviorTree(SOBehaviorTree.BehaviorTree tree)
    {
        if (tree) return tree;
        if (!Selection.activeGameObject) return tree;

        IFindTreeAble activeTree = Selection.activeGameObject.GetComponent<IFindTreeAble>();
        if (activeTree == null) return tree;
        return activeTree.GetBehaviorTree();
    }
    void PlayStateTree(BehaviorTree tree)
    {
        if (!Application.isPlaying) return;
        if (!tree) return;

        treeView.PlayModePopulateview(tree);
    }
    void EditStateTree(BehaviorTree tree)
    {
        if (Application.isPlaying) return;
        if (!tree || !AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())) return;

        treeView.EditorModePopulateview(tree);
    }
}
