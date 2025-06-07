using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData/SoundData", menuName = "SoundData/SoundData")]
public class SoundData : ScriptableObject
{
    public List<AudioClip> audioList = new List<AudioClip>();
}
