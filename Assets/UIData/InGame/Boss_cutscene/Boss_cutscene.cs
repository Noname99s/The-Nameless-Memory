using DG.Tweening;
using UnityEngine;

public class Boss_cutscene : MonoBehaviour
{
    [SerializeField] DOTweenAnimation sequence;
    public void OnCutSceneEnd()
    {
        Debug.Log("Cutscene ended");
        sequence.DORestart();
    }
}
