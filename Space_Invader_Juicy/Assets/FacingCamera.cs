using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Camera.main.transform,Vector3.up);
    }

    
}
