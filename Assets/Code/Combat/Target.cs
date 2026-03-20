using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;
    public AudioClip[] WeaponAudioClips;
    [Range(0, 1)] public float WeaponAudioVolume = 0.5f;

    private void Update()
    {
        if(health <= 0)
        {
            AudioSource.PlayClipAtPoint(WeaponAudioClips[0], transform.TransformPoint(this.transform.position), WeaponAudioVolume);
            Destroy(gameObject);
        }
        
            
    }
}
