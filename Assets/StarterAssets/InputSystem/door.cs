using UnityEngine;

public class door : MonoBehaviour, IInteractable
{
    //public AnimationCurve openSpeedCurve = new(new Keyframe[] { new(0, 1, 0, 0), new(0.8f, 1, 0, 0), new(1, 0, 0, 0) }); //Contols the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)
    //public AnimationCurve openSpeedCurve = new(new Keyframe[] { new(0, 0, 1, 0), new(0, 0, 1, 0.8f), new(0, 0, 0, 1) }); //Contols the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)
    public float openSpeedMultiplier = 2.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = 90.0f; //Global door open speed that will multiply the openSpeedCurve

    bool open = false;
    //bool enter = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;
    float timeOpen = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
         if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeedMultiplier /* * openSpeedCurve.Evaluate(openTime)*/;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);
        if (open == true)
        {
            timeOpen ++;
        }
        if (timeOpen > 160)
        {
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
            timeOpen = 0;
        }
    }
    public void Interact()
    {
        if (open == false)
        {
            Debug.Log("OH NO, I GOT INTERACTED!");
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }
    }
}
