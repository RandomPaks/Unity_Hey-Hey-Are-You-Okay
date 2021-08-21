using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance { get; private set; }

    public TapProperty tapProperty;
    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float gestureTime = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    Touch trackedFinger1;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            trackedFinger1 = Input.GetTouch(0);

            if (trackedFinger1.phase == TouchPhase.Began)
            {
                startPoint = trackedFinger1.position;
                gestureTime = 0;
            }

            if (trackedFinger1.phase == TouchPhase.Ended)
            {
                endPoint = trackedFinger1.position;

                if (gestureTime <= tapProperty.tapTime && Vector2.Distance(startPoint, endPoint) < (Screen.dpi * tapProperty.tapMaxDistance))
                {
                    Debug.Log("tap");
                }
            }
            else
            {
                gestureTime += Time.deltaTime;
            }
        }
    }
}
