using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Controler : MonoBehaviour
{
    public float speed;
    public float life = 10;

    [SerializeField] private float clampBorder = 10f;

    [SerializeField] private Animator anim;
    [SerializeField] private Material mat_Glow;
    [SerializeField] private Material mat_Holo;

    private MeshRenderer[] meshs;

    private void Start()
    {
        meshs = GetComponentsInChildren<MeshRenderer>();
        ChangeShipMaterial(mat_Glow);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");

        float positionX = transform.position.x + inputX * speed;
        positionX = Mathf.Clamp(positionX, -clampBorder, clampBorder);
        transform.position = new Vector3(positionX, transform.position.y ,transform.position.z);

        anim.SetFloat("Orientation", inputX);
    }

    public void Hit()
    {
        life--;

        if (life <= 0)
        {
            GameManager.Instance?.Defeat();
            Destroy(gameObject);
        }

        StopCoroutine(HitEffect());
        StartCoroutine(HitEffect());
    }

    private void ChangeShipMaterial(Material mat)
    {
        for (int i = 0; i < meshs.Length; i++)
            meshs[i].material = mat;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ennemy"))
        {
            GameManager.Instance.Defeat();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator HitEffect()
    {
        ChangeShipMaterial(mat_Holo);
        yield return new WaitForSeconds(0.8f);
        ChangeShipMaterial(mat_Glow);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3( Mathf.Max(pos.x - 2, pos.x - clampBorder) , pos.y, pos.z), Vector3.left * clampBorder);
        Gizmos.DrawRay(new Vector3( Mathf.Min(pos.x + 2, pos.x + clampBorder) , pos.y, pos.z), Vector3.right * clampBorder);
    }
}
