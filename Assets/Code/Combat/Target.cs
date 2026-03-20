using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;
    //public float respawnTime;
    public AudioClip[] WeaponAudioClips;
    [Range(0, 1)] public float WeaponAudioVolume = 0.5f;

    private void Update()
    {
        if(health <= 0)
        {
            //Destroy(gameObject);
            AudioSource.PlayClipAtPoint(WeaponAudioClips[0], transform.TransformPoint(this.transform.position), WeaponAudioVolume);
            gameObject.SetActive(false);
            Invoke("Activate", 8f);
        }
        
            
    }
    void Activate()
        {
            gameObject.SetActive(true);
            health = 5;
        }


    
}
