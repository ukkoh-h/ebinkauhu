using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 5;
    public float maxHealth = 5;
    public float cooldown = 8f;
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
            Invoke("Activate", cooldown);
        }
        
            
    }
    void Activate()
        {
            gameObject.SetActive(true);
            health = maxHealth;
        }


    
}
