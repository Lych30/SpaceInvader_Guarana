using System;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float healthPoints = 20f;

    private float life;
    public ParticleSystem Explode;
    [SerializeField] Mesh mesh;
    Renderer rd;
    void Start()
    {
        GameManager.Instance?.InvaderCountIncrement.Invoke();

        try
        {
            rd = gameObject.GetComponent<MeshRenderer>();
            mesh = GetComponent<MeshFilter>().mesh;
        }
        catch
        {
            var skinMR = GetComponentInChildren<SkinnedMeshRenderer>();
            rd = skinMR;
            mesh = skinMR.sharedMesh;
        }

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

            rd.enabled = false;
            GetComponent<Collider>().enabled = false;

            GameManager.Instance?.EnemyKilled.Invoke();

            Explode.Play();
            Destroy(this.gameObject, 2);
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<Rigidbody>().AddForce(Force);
        }
    }
}
