using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

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
    [SerializeField] Material mat;
    [ColorUsageAttribute(true, false)]
    Color initialColor;
    public AudioSource exlposionSFX;
    void Start()
    {
        GameManager.Instance?.IncrementEnemy(this);
        mat = transform.GetChild(1).GetComponentInChildren<SkinnedMeshRenderer>().material;
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
        initialColor = (Vector4)transform.GetChild(1).GetComponentInChildren<SkinnedMeshRenderer>().material.GetColor("_EmissionColor");
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
        StopAllCoroutines();
        StartCoroutine(BackToRed(0.1f));
        mat.SetColor("_EmissionColor", new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f),2)) ;
        if (life <= 0)
        {
            //faudra mettre de la juicyness ici

            rd.enabled = false;
            GetComponent<Collider>().enabled = false;

            GameManager.Instance?.DecrementEnemy(this);

            if (GameFeelManager.instance.EnnemyExplosionGF)
            {
                exlposionSFX.pitch = UnityEngine.Random.Range(2.0f, 3.0f);
                exlposionSFX.Play();
                Explode.Play();
                Destroy(this.gameObject, 2);
            }
            else
            {
                Destroy(this.gameObject);
            }

            if (GameFeelManager.instance.PopScoreGF)
            {
                GameObject PopScore = Instantiate(PopScoreGO, transform.position, Quaternion.identity);
                PopScore.GetComponentInChildren<TextMeshPro>().text = ScoreValue.ToString();
                Destroy(PopScore, 1);
            }
            
            ScoreManager.instance.AddScore(ScoreValue);
        }
    }
    IEnumerator BackToRed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("CHAAAAAAANGE");
        mat.SetColor("_EmissionColor", new Color(initialColor.r,initialColor.g,initialColor.b,2));
    }
}
