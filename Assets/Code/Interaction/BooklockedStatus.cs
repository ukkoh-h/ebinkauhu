using UnityEngine;

public class BookLockedStatus : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] doorBookcase _door1;
    [SerializeField] HUDManager _hud;
    public InteractionAudio interactionAudio;
    public bool unlocked = false;
    public bool opened = false;
    public string whileLocked;
    public string whileUnlocked;
    public string whenOpening;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public void LockedStatus1()
    {
        if (unlocked)
        {
            if (!opened)
            {
                _hud.interactText = whenOpening;
                _hud.UpdateInteractText();
                AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[5], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
                _door1.Open1();
                opened = true;
            } else
            {
                _hud.interactText = whileUnlocked;
                _hud.UpdateInteractText();
            }
        } else
        {
            AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[0], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume);
            _hud.interactText = whileLocked;
            _hud.UpdateInteractText();
        }
    }    
        public void ChangeLocked()
    {
        unlocked = !unlocked;
    }
    public void LoadData(GameData data)
    {
        data.lockedState.TryGetValue(id, out unlocked);
        if (unlocked)
        {
            unlocked = true;
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
