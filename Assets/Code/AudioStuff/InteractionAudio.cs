using UnityEngine;
using System.Collections;

public class InteractionAudio : MonoBehaviour
{
    public AudioClip[] InteractionAudioClips;
    [Range(0, 1)] public float InteractionAudioVolume = 0.5f;
    public IEnumerator CrashSoundTimerCorutine()
    {
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(InteractionAudioClips[12], transform.TransformPoint(this.transform.position), InteractionAudioVolume);
    }
}
