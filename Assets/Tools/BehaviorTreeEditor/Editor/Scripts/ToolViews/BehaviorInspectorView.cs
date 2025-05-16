using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class BehaviorInspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BehaviorInspectorView, VisualElement.UxmlTraits> { }

    Editor editor;
    public BehaviorInspectorView()
    {

    }

    internal void UpdateSelection(NodeView nodeView)
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor); 
        editor = Editor.CreateEditor(nodeView.node);
        IMGUIContainer container = new IMGUIContainer(editor.OnInspectorGUI);
        Add(container);
    }
}
