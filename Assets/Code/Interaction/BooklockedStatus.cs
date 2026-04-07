using UnityEngine;

public class BookLockedStatus : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] doorBookcase _door1;
    [SerializeField] HUDManager _hud;
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
                _door1.Open1();
                opened = true;
            } else
            {
                _hud.interactText = whileUnlocked;
                _hud.UpdateInteractText();
            }
        } else
        {
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
