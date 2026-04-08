using NUnit.Framework;
using UnityEngine;

public class lockedStatus : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] door2 _door1;
    [SerializeField] door2 _door2;
    [SerializeField] lockedInteractable _lockedInt;
    [SerializeField] HUDManager _hud;
    public monster monster;
    public InteractionAudio interactionAudio;
    public lever lever;
    public bool locked1 = false;
    public bool locked2 = false;
    public bool locked3 = false;
    public bool lockedForPlayer = false;
    public bool isLever = false;
    public bool isPaper;
    public bool isDoor;
    public bool isClock;
    public bool isDisck;
    public bool isBookcaseDoor;
    public bool isChemistry;
    public bool isButton;
    bool firstTime = false;
    public bool unlocked = false;
    public string whileLocked;
    public string whileUnlocked;
    public string whenOpening;
    public string leverLocked;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    void Update()
    {
        if (locked1 || locked2 || locked3)
        {
            unlocked = false;
        } else
        {
            unlocked = true;
        }
    }

    public void LockedStatus1()
    {
        if (!isLever)
        {
            if (!locked1 && !locked2 && !locked3 && !firstTime)
            {
                _hud.interactText = whileUnlocked;
                _hud.UpdateInteractText();
                _door1?.Open1();
                _door2?.Open2();
                _lockedInt?.Interact();
                firstTime = true;
                if (isDoor) AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[3], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
                if (isClock)
                {
                    AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[4], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
                    monster.MakeNoise();
                }
                if (isPaper) AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[10], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
                if (isDisck) AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[7], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
                if (isBookcaseDoor) AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[5], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
                if (isChemistry) AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[8], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
                if (isButton) AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[9], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);

            }
            else if (!locked1 && !locked2 && !locked3 && firstTime)
            {
                _hud.interactText = whenOpening;
                _hud.UpdateInteractText();
                _door1?.Open1();
                _door2?.Open2();
                _lockedInt?.Interact();
            }
            else
            {
                _hud.interactText = whileLocked;
                _hud.UpdateInteractText();
                if (isDoor) AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[0], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
                //Debug.Log("IT'S LOCKED!");
            }
        } 
        else if (isLever && !locked1)
        {
            AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[6], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
            lever.MonsterSmash();
        }
        else if (isLever && locked1)
        {
            _hud.interactText = leverLocked;
            _hud.UpdateInteractText();
        }
    }    
    public void LockedStatus2()
    {
        if (!locked1 && !locked2 && !locked3 && !firstTime)
        {
            _hud.interactText = whileUnlocked;
            _hud.UpdateInteractText();
            _door1?.Open2();
            _door2?.Open1();
        } else if (!locked1 && !locked2 && !locked3 && firstTime)
        {
            _hud.interactText = whenOpening;
            _hud.UpdateInteractText();
            _door1?.Open1();
            _door2?.Open2();
            firstTime = false;
        } 
        else
        {
            _hud.interactText = whileLocked;
            _hud.UpdateInteractText();
        }
    }
    public void LockedStatus3()
    {
            _door1?.Open1();
            _door2?.Open2();
    }
    public void LockedStatus4()
    {
            _door1?.Open2();
            _door2?.Open1();
    }
    public void ChangeLocked()
    {
        if (locked1)
        {
            locked1 = false;
        } else if (locked2)
        {
            locked2 = false;
        } else if (locked3)
        {
            locked3 = false;
            //Debug.Log("IT'S UNLOCKED!");
        }
    }
    public void MonsterChangeLocked()
    {
            locked1 = !locked1;
    }
    public void LoadData(GameData data)
    {
        data.lockedState.TryGetValue(id, out unlocked);
        if (unlocked)
        {
            locked1 = false;
            locked2 = false;
            locked3 = false;
        }
    }
    public void SaveData(ref GameData data)
    {
        if (data.lockedState.ContainsKey(id))
        {
            data.lockedState.Remove(id);
        }
        data.lockedState.Add(id, unlocked);
    }
}
