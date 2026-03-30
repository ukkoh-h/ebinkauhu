using UnityEngine;

public class Interactee : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("OH NO, I GOT INTERACTED!");
    }
}
