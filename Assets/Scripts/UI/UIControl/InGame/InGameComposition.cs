using UnityEngine;

public class InGameComposition : MonoBehaviour
{
    [SerializeField] InGameView inGameView;
    InGamePresenter inGamePresenter;
    void Awake()
    {
        inGamePresenter = new InGamePresenter(inGameView);
    }
    void OnDestroy()
    {
        inGamePresenter.Dispose();
    }

}
