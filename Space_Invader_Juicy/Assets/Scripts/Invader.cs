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

    [Space, Space, Header("Visual settings")]
    [SerializeField] Mesh mesh;
    public AudioSource explosionSFX;
    public Gradient lifeColorGradient;
    public Gradient positionAlphaGradient;
    public bool useMeshRenderer;

    [Space]
    public ParticleSystem[] Explode;
    [SerializeField] Renderer[] rd;
    [SerializeField] List<Material> mat;
    [ColorUsageAttribute(true, false), SerializeField] List<Color> initialColor;

    [Space, Header("Other settings")]
    [SerializeField] GameObject projectilePrefab;
    public GameObject PopScoreGO;

    void Start()
    {
        GameManager.Instance?.IncrementEnemy(this);

        explosionSFX = GetComponent<AudioSource>();
        
        if (!useMeshRenderer)
        {
            rd = GetComponentsInChildren<SkinnedMeshRenderer>();
            mesh = rd[0].GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }
        else
        {
            rd = GetComponentsInChildren<MeshRenderer>();
            mesh = rd[0].GetComponent<MeshFilter>().mesh;
        }
        

        /*rd = GetComponentsInChildren<Renderer>();

        if (rd.GetType() == typeof(MeshRenderer))
            mesh = rd[0].GetComponent<MeshFilter>().mesh;
        else if (rd.GetType() == typeof(SkinnedMeshRenderer))
            mesh = rd[0].GetComponent<SkinnedMeshRenderer>().sharedMesh;*/

        foreach (var item in rd)
        {
            mat.Add(item.material);
            initialColor.Add((Vector4)item.material.GetColor("_EmissionColor"));
        }

        foreach (var item in Explode)
        {
            var shape = item.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = mesh;
        }

        //initialColor = (Vector4)transform.GetChild(1).GetComponentInChildren<SkinnedMeshRenderer>().material.GetColor("_EmissionColor");
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

        foreach (var item in mat)
        {
            item.SetColor("_EmissionColor", new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 2));
        }

        //mat.SetColor("_EmissionColor", new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f),2)) ;
        
        if (life <= 0)
        {
            //faudra mettre de la juicyness ici

            foreach (var item in rd)
            {
                item.enabled = false;
            }

            //rd.enabled = false;
            GetComponent<Collider>().enabled = false;

            GameManager.Instance?.DecrementEnemy(this);

            if (GameFeelManager.instance.EnnemyExplosionGF)
            {
                explosionSFX.pitch = UnityEngine.Random.Range(2.0f, 3.0f);
                explosionSFX.Play();

                foreach (var item in Explode)
                {
                    item.Play();
                }

                //Explode.Play();
                Destroy(this.gameObject, 2);
            }
            else
            {
                Destroy(this.gameObject);
            }

            if (GameFeelManager.instance.PopScoreGF)
            {
                GameObject PopScore = Instantiate(PopScoreGO, transform.position, Quaternion.identity);
                PopScore.transform.GetChild(0).GetComponent<TextMeshPro>().text = ScoreValue.ToString();
                PopScore.transform.GetChild(1).GetComponent<TextMeshPro>().text = ScoreValue.ToString();
                Destroy(PopScore, 1);
            }
            
            ScoreManager.instance.AddScore(ScoreValue);
        }
    }

    IEnumerator BackToRed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("CHAAAAAAANGE");

        for (int i = 0; i < mat.Count; i++)
        {
            //mat[i].SetColor("_EmissionColor", new Color(initialColor[i].r, initialColor[i].g, initialColor[i].b, 2));
            mat[i].SetColor("_EmissionColor", lifeColorGradient.Evaluate(1 - life/healthPoints));
        }

        //mat.SetColor("_EmissionColor", new Color(initialColor.r,initialColor.g,initialColor.b,2));
    }
}
