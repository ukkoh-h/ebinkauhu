using UnityEngine;

public class Swinger : MonoBehaviour
{
    //public AnimationCurve openSpeedCurve = new(new Keyframe[] { new(0, 1, 0, 0), new(0.8f, 1, 0, 0), new(1, 0, 0, 0) }); //Contols the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)
    //public AnimationCurve openSpeedCurve = new(new Keyframe[] { new(0, 0, 1, 0), new(0, 0, 1, 0.8f), new(0, 0, 0, 1) }); //Contols the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)
    public float openSpeedMultiplier = 2.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = 120.0f; //Global door open speed that will multiply the openSpeedCurve

    bool swinging = false;
    float defaultRotationAngle;
    float currentRotationAngle;
    Vector3 swingerScale;
    float swingTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swingerScale = transform.localScale;
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
         if (swingTime < 1)
        {
            swingTime += Time.deltaTime * openSpeedMultiplier /* * openSpeedCurve.Evaluate(openTime)*/;
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (swinging ? doorOpenAngle : 0)*-1, swingTime), transform.localEulerAngles.z);
        
        if (swinging && swingTime > 0.9)
        {
            swinging = false;
            transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            currentRotationAngle = transform.localEulerAngles.y;
            swingTime = 0;
        }
    }
    public void Swing()
    {
        if (!swinging)
        {
            swinging = true;
            transform.localScale = new Vector3(1f,1f,1f);
            currentRotationAngle = transform.localEulerAngles.y;
            swingTime = 0;
        }
    }
}
