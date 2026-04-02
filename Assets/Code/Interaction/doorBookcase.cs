using UnityEngine;

public class doorBookcase : MonoBehaviour
{
    public float openSpeedMultiplier = 1.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = 90.0f; //Global door open speed that will multiply the openSpeedCurve

    bool open = false;
    //bool enter = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;
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
    }
    public void Open1()
    {
        if (!open)
        {
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }
    }
}
