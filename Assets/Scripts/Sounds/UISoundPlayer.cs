using System.Collections;
using System.Collections.Generic;
using Orpul1.UI;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    
    [SerializeField] List<AudioClip> uiAudioClips = new List<AudioClip>();
    
    public void PlayUISound(int type)
    {
        Managers.Sound.PlayEffect(uiAudioClips[type], EMixerGroup.UISound);
    }
}
