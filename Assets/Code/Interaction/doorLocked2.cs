using UnityEngine;

public class doorLocked2 : MonoBehaviour
{
public float openSpeedMultiplier = 2.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = 90.0f; //Global door open speed that will multiply the openSpeedCurve

    bool open = false;
    bool direction = false;
    bool _isLocked = true;
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
        if (direction)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, (defaultRotationAngle + (open ? doorOpenAngle : 0)) * -1, openTime), transform.localEulerAngles.z);
        } else
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);
        }
        if (open == true)
        {
            timeOpen ++;
            if (timeOpen > 320)
            {
                open = !open;
                currentRotationAngle = transform.localEulerAngles.y;
                openTime = 0;
                timeOpen = 0;
            }
        }
    }
    public void Open1()
    {
        if (!_isLocked) {
        if (!open)
            {
                open = !open;
                direction = false;
                currentRotationAngle = transform.localEulerAngles.y;
                openTime = 0;
            }
        }
    }
    public void Open2()
    {
        if (!_isLocked) {
        if (!open)
            {
                open = !open;
                direction = true;
                currentRotationAngle = transform.localEulerAngles.y;
                openTime = 0;
            }
        }
    }
        public void ChangeLocked()
    {
        _isLocked = !_isLocked;
    }
}
