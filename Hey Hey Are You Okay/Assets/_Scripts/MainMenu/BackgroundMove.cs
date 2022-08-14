using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMove : MonoBehaviour
{
    RectTransform rect;
    float x;
    [SerializeField] float xTeleport;
    public float speed = 50.0f;
    [SerializeField] float xThreshold = -765f;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        x = rect.anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        x -= Time.deltaTime * speed;
        rect.anchoredPosition = new Vector2(x, 0);

        if(x <= xThreshold)
        {
            x = xTeleport;
        }
    }
}
