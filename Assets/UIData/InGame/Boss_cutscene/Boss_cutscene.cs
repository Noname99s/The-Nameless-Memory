using DG.Tweening;
using UnityEngine;

public class Boss_cutscene : MonoBehaviour
{
    [SerializeField] DOTweenAnimation sequence;
    public void BackToMain()
    {
        Managers.Game.ChangeState(GameState.MainMenu);
    }
    public void OnCutSceneEnd()
    {
        Debug.Log("Cutscene ended");
        sequence.DORestart();
    }
}
