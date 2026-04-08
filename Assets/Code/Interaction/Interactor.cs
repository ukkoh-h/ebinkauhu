using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    InputAction interact;

    private PlayerInput _playerInput;

    [SerializeField] Animator vampyr;
    [SerializeField] Animator noteAnimator;
    public TextMeshProUGUI text_interact;
    [SerializeField] GameObject saveMenu;
    [SerializeField] GameObject note;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        interact = _playerInput.actions.FindAction("Interact");
        text_interact.text = $"";
        note.SetActive(false);
        saveMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.WasPerformedThisFrame())
        {
            Ray r = new(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.CompareTag("SaveObject"))
                {
                    vampyr.Play("Interact", 0, 0.25f);
                    _playerInput.enabled = false;
                    Invoke("Activate_Menu", 0.7f);
                }
                else if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    if (hitInfo.collider.gameObject.CompareTag("Note"))
                    {
                        vampyr.Play("Interact", 0, 0.25f);
                        noteAnimator.Play("Note_anim", 0, 0.25f);
                        _playerInput.enabled = false;
                        Invoke("Activate_Note", 0.2f);
                        interactObj.Interact();
                    }
                    else
                    {
                        interactObj.Interact();
                        vampyr.Play("Interact", 0, 0.25f);
                        _playerInput.enabled = false;
                        Invoke("Activate", 0.7f);
                    }  
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(text_interact.text != $"Press E to interact")
        {
            if(other.TryGetComponent(out IInteractable interactObj))
            {
                text_interact.text = $"Press E to interact";
            }
            else if(other.gameObject.CompareTag("SaveObject"))
            {
                text_interact.text = $"Press E to interact";
            }
            else if(other.gameObject.CompareTag("Note"))
            {
                text_interact.text = $"Press E to interact";
            }
        }
  
    }
    private void OnTriggerExit(Collider other)
    {
        if(text_interact.text == $"Press E to interact")
        {
            if(other.TryGetComponent(out IInteractable interactObj))
            {
                text_interact.text = $"";
            }
            else if(other.gameObject.CompareTag("SaveObject"))
            {
                text_interact.text = $"";
            }
            else if(other.gameObject.CompareTag("Note"))
            {
                text_interact.text = $"";
            }
        }
    }
    void Activate()
    {
        _playerInput.enabled = true;
        note.SetActive(false);
    }
    void Activate_Menu()
    {
        _playerInput.enabled = true;
        saveMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = true;
        
    }
    void Activate_Note()
    {
        note.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void Deactivate_Note()
    {
        noteAnimator.Play("Note_anim_close", 0, 0.25f);
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("Activate", 0.2f);
    }
}