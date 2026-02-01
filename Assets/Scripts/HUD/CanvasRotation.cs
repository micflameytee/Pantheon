using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotation : MonoBehaviour
{
    // This is very evil but I would love to get this healthbar feature shipped in alpha
    
    private Vector3 requiredLocalPos;
    private Quaternion requiredLocalRot;

    void Awake()
    {
        //requiredLocalPos = transform.parent.position - new Vector3(0, 1, 0);
        requiredLocalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged)
        {
            //Debug.Log(transform.localPosition);
            //transform.localPosition = requiredLocalPos;
            transform.rotation = requiredLocalRot;
            transform.hasChanged = false;
        }
    }
}
