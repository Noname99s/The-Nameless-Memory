using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] string _scenarioType = "Text_Prologue";
    [SerializeField] int _dialogIndex = 0;
    string text1 = string.Empty;
    public event System.Action<string> OnDialogUpdate;
    void Start()
    {
        text1 = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[_dialogIndex]._Text;
        OnDialogUpdate += Managers.UI.GetPopupUI<InGameComposition>().OnStaticDialogUpdateConnection;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnDialogUpdate?.Invoke(text1);
        }
    }
}
