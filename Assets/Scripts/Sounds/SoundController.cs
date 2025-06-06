using System.Collections.Generic;
using UnityEngine;

public class SoundInfo
{
    public AudioClip sound;
    public EMixerGroup mixerGroup;
}
public class SoundController : MonoBehaviour
{
    [SerializeField] List<SoundInfo> soundList;
    [SerializeField] float spatialBlend;
    [SerializeField] float maxDistance;
    [SerializeField] float minDistance;

    public void PlaySound(int soundType)
    {
        Managers.Sound.PlayEffect(soundList[soundType].sound, soundList[soundType].mixerGroup, transform, spatialBlend, minDistance, maxDistance);
    }
}
