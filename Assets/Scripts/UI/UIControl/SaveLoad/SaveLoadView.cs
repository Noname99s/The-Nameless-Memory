using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SaveLoadView : MonoBehaviour , ISaveLoadView
{
    [SerializeField] Button[] memoryStack = new Button[3];
    [SerializeField] TextMeshProUGUI[] dataText = new TextMeshProUGUI[3];
    void Start()
    {
    }
    void OnDestroy()
    {
    }
}