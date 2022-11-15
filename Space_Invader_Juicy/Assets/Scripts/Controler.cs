using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Controler : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        float positionX = transform.position.x + (Input.GetAxis("Horizontal") * speed);
        positionX = Mathf.Clamp(positionX,-10,10);
        transform.position = new Vector3(positionX, 0,0);
    }
   
}
