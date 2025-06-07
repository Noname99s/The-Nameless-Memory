using UnityEngine;

namespace Orpul1.UI
{
    public enum EUiSound
    {
        ButtonClick,
        Cancel,
        Confirm,
        Error,
        TitleTowerMove,
        Notify,
        Popup,
        TabChange,
        Gamestart,
        END,
    }
    public enum ESystemSound
    {
        LoadingSceneEnter = EUiSound.END,
        MemoryPieceGain,
        NewSkill,
        StageClear,
        StageFail,
        RemindLoading,
    }
}
