using System.Collections.Generic;
using UnityEngine;

public class SoundInfo
{
    public AudioClip sound;
    public EMixerGroup mixerGroup;
}
public class SoundController : MonoBehaviour
{
    List<SoundInfo> soundList;
    float spatialBlend;
    float maxDistance;
    float minDistance;

    public void PlaySound(int soundType)
    {
        Managers.Sound.PlayEffect(soundList[soundType].sound, soundList[soundType].mixerGroup, transform, spatialBlend, minDistance, maxDistance);
    }
}
