//using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class lockedInteractable : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] lockedStatus lockStat;
    [SerializeField] BookLockedStatus bookLockStat;
    [SerializeField] GameObject monster;
    [SerializeField] GameObject keyItem;
    public roomTrigger roomTrigger;
    public InteractablePickup pickup;
    bool interactable = true;
    bool inactiveState = false;
    public bool finalScene;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
        void Update()
    {
        if (interactable || !lockStat.unlocked || !bookLockStat.unlocked)
        {
            inactiveState = false;
        } else
        {
            inactiveState = true;
        }
    }
    public void Interact()
    {
        if (interactable)
        {
            bookLockStat?.ChangeLocked();
            lockStat?.ChangeLocked();
            pickup?.Pickup();
            if (monster != null) monster.SetActive(true);
            if (keyItem != null) keyItem.SetActive(false);
            if (finalScene) roomTrigger.finalScene = true;
            interactable = false;
            gameObject.SetActive(false);
        }
    }
        public void LoadData(GameData data)
    {
        data.itemActiveState.TryGetValue(id, out inactiveState);
        if (inactiveState)
        {
            gameObject.SetActive(false);
            if (keyItem != null) keyItem.SetActive(false);
        }
    }
        public void SaveData(ref GameData data)
    {
        if (data.itemActiveState.ContainsKey(id))
        {
            data.itemActiveState.Remove(id);
        }
        data.itemActiveState.Add(id, inactiveState);
    }
}
