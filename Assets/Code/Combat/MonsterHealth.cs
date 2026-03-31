using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public float health = 15;
    public float maxHealth = 15;
    public float cooldown = 8f;

    // damage = 5 -> 
    // hp 15 - 5: damaged sfx, hp 10 - 5: damaged sfx
    // kun hp = 0: damaged sfx ja setActive(false)
    public bool hitOnce = false;
    public bool hitTwice = false;
    
    public AudioClip[] MonsterAudioClips;
    [Range(0, 1)] public float MonsterAudioVolume = 0.5f;

    private void Update()
    {
        if(health == (maxHealth / 3) * 2 )
        {
            if(hitOnce == false) 
            {
                OnHit();
                hitOnce = true;
            }
        }
        
        
        if(health == maxHealth / 3)
        {
            if(hitTwice == false) 
            {
                OnHit();
                hitTwice = true;
            }
        }

        if(health <= 0)
        {
            if (MonsterAudioClips.Length > 0) OnHit();

            gameObject.SetActive(false);
            Invoke("Activate", cooldown);
        }
        
            
    }
    private void OnHit()
    {
        if (MonsterAudioClips.Length > 0)
            {
                var index = Random.Range(0, MonsterAudioClips.Length);
                AudioSource.PlayClipAtPoint(MonsterAudioClips[index], transform.TransformPoint(this.transform.position), MonsterAudioVolume);
            }
    }

    void Activate()
    {
        gameObject.SetActive(true);
        health = maxHealth;
        hitOnce = false;
        hitTwice = false;
    }


    
}
