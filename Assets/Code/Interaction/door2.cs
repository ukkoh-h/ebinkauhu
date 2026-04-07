using System.Collections;
using UnityEngine;

public class door2 : MonoBehaviour
{
    [SerializeField] GameObject particle1;
    [SerializeField] GameObject particle2;
    //public AnimationCurve openSpeedCurve = new(new Keyframe[] { new(0, 1, 0, 0), new(0.8f, 1, 0, 0), new(1, 0, 0, 0) }); //Contols the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)
    //public AnimationCurve openSpeedCurve = new(new Keyframe[] { new(0, 0, 1, 0), new(0, 0, 1, 0.8f), new(0, 0, 0, 1) }); //Contols the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)
    public float openSpeedMultiplier = 2.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = 90.0f; //Global door open speed that will multiply the openSpeedCurve

    bool open = false;
    bool direction = false;
    //float timedOpen = 0;
    //bool timedOpen = false;
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
        if (direction)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0) * -1, openTime), transform.localEulerAngles.z);
        } else
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);
        }
        if (open == true)
        {
            StartCoroutine(DoorTimerCorutine());
        }
    }
    private IEnumerator DoorTimerCorutine()
    {
        yield return new WaitForSeconds(3f);
        open = false;
        direction = false;
        currentRotationAngle = transform.localEulerAngles.y;
        openTime = 0;
        if (particle1!=null)particle1.SetActive(true);
        if (particle2!=null)particle2.SetActive(true);
        //timedOpen = true;
    }
    public void Open1()
    {
        if (!open)
        {
            open = !open;
            direction = false;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
            if (particle1!=null)particle1.SetActive(false);
            if (particle2!=null)particle2.SetActive(false);
        }
    }
    public void Open2()
    {
        if (!open)
        {
            open = !open;
            direction = true;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
            if (particle1!=null)particle1.SetActive(false);
            if (particle2!=null)particle2.SetActive(false);
        }
    }
}
