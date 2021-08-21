using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lmao : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = Time.deltaTime * 100f;
        gameObject.transform.Rotate(0, 0, z);
    }
}
