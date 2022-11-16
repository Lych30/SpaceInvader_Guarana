using System;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float healthPoints = 20f;
    public int ScoreValue;
    private float life;
    public ParticleSystem Explode;
    [SerializeField] Mesh mesh;
    Renderer rd;
    [SerializeField] GameObject projectilePrefab;

    void Start()
    {
        GameManager.Instance?.InvaderCountIncrement.Invoke();
        GameManager.Instance?.IncrementEnemy(this);

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

    public void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position, transform.rotation);
        //bullet.GetComponent<EnemyBullet>();
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
            GameManager.Instance?.DecrementEnemy(this);

            Explode.Play();
            Destroy(this.gameObject, 2);
            ScoreManager.instance.AddScore(ScoreValue);
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<Rigidbody>().AddForce(Force);
        }
    }
}
