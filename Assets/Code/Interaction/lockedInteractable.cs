//using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class lockedInteractable : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] lockedStatus lockStat;
    [SerializeField] BookLockedStatus bookLockStat;
    [SerializeField] GameObject monster;
    public roomTrigger roomTrigger;
    bool interactable = true;
    bool activeState = true;
    public bool finalScene;
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
        void Update()
    {
        if (interactable || lockStat.locked || bookLockStat.locked)
        {
            activeState = true;
        } else
        {
            activeState = false;
        }
    }
    public void Interact()
    {
        if (interactable)
        {
            bookLockStat?.ChangeLocked();
            lockStat?.ChangeLocked();
            if (monster != null) monster.SetActive(true);
            if (finalScene) roomTrigger.finalScene = true;
            interactable = false;
            gameObject.SetActive(false);
        }
    }
        public void LoadData(GameData data)
    {
        data.itemActiveState.TryGetValue(id, out activeState);
        if (!activeState)
        {
            gameObject.SetActive(false);
        }
    }
        public void SaveData(ref GameData data)
    {
        if (data.itemActiveState.ContainsKey(id))
        {
            data.itemActiveState.Remove(id);
        }
        data.itemActiveState.Add(id, activeState);
    }
}
