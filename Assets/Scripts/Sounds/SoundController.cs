using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] List<AudioClip> sounds;
    [SerializeField] EMixerGroup mixerGroup;
    [SerializeField] float spatialBlend;
    [SerializeField] float maxDistance;
    [SerializeField] float minDistance;

    public void PlaySound(int soundType)
    {
        Managers.Sound.PlayEffect(sounds[soundType], mixerGroup, transform, spatialBlend, minDistance, maxDistance);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, minDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
