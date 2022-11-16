using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public GameObject PopScoreGO;

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
            if (GameFeelManager.instance.EnnemyExplosionGF)
            {
                Explode.Play();
                Destroy(this.gameObject, 2);
            }
            else
            {
                Destroy(this.gameObject);
            }

            ScoreManager.instance.AddScore(ScoreValue);
            if (GameFeelManager.instance.PopScoreGF)
            {
                GameObject PopScore = Instantiate(PopScoreGO, transform.position, Quaternion.identity);
                PopScore.GetComponentInChildren<TextMeshPro>().text = ScoreValue.ToString();
                Destroy(PopScore, 1);
            }
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<Rigidbody>().AddForce(Force);
        }
    }
}
