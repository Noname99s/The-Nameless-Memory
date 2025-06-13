using System.Collections.Generic;
using UnityEngine;
using Orpul1.Sound;


public class UISoundPlayer : MonoBehaviour
{
    public void PlayUISound(int type)
    {
        Managers.Sound.PlaySound2D(ESoundCategory.UI, type, EMixerGroup.UISound);
    }
}
