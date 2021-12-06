using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public GameObject tool;

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            GameManager.Instance.currentTool = null;
            tool.transform.position = new Vector3(100, 100, 100);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            int layerMask = 1 << 30;

            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            Vector3 far = Camera.main.ScreenToWorldPoint(mousePosFar);
            Vector3 near = Camera.main.ScreenToWorldPoint(mousePosNear);

            RaycastHit hit;
            if (Physics.Raycast(near, far - near, out hit, Camera.main.farClipPlane, layerMask))
            {
                Vector3 collide = Vector3.Normalize(far - near) * hit.distance;
                Debug.DrawRay(near, collide, Color.green);

                tool.transform.position = hit.point;
            }
            else
            {
                Debug.DrawRay(near, far - near, Color.green);
            }
        }
    }
}
