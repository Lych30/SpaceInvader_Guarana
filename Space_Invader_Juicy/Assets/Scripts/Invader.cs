using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float healthPoints = 20f;

    private float life;
    public ParticleSystem Explode;
    [SerializeField] Mesh mesh;
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        var shape = Explode.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        shape.mesh = mesh;
        
        life = healthPoints;
    }

    void Update()
    {

    }

    public void TakeDmg(float dmg)//, Vector3 Force)
    {
        life -= dmg;
        if (life <= 0)
        {
            //faudra mettre de la juicyness ici
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Explode.Play();
            Destroy(this.gameObject, 2);
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<Rigidbody>().AddForce(Force);
        }
    }

   
}
