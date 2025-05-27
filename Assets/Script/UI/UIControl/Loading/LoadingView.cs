using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : UIPopup
{
    [SerializeField] Text loadingText;
    BG_DB_LoadingText loadingTextData;

    private void Start()
    {
        loadingTextData = BG_DB_LoadingText.GetEntity(Random.Range(0, 4));

        if (loadingText != null)
        {
            loadingText.DOText(loadingTextData._detail + "\n" + loadingTextData._name, 5f, true)
                .SetEase(Ease.Linear)
                .SetLoops(1, LoopType.Restart);
        }
    }
}
