using UnityEngine;
using System.Collections;

public class InteractionAudio : MonoBehaviour
{
    public AudioClip[] InteractionAudioClips;
    [Range(0, 1)] public float InteractionAudioVolume = 0.5f;
}
