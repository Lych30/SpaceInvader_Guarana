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

    [Header("Mesh Animation")]
    [SerializeField] private Animator anim;
    [SerializeField][Range(0f, 1f)] private float tiltAmount = 1f;

    [Header("Materials Control")]
    [SerializeField] private Material mat_Glow;
    [SerializeField] private Material mat_Holo;
    [SerializeField] [GradientUsage(true)] private Gradient hitBaseColorAnim;
    [SerializeField] [GradientUsage(true)] private Gradient hitHighlightColorAnim;
    [SerializeField] private float hitAnimDuration = 0.3f;
    [SerializeField] private int animationLoops = 3;

    [Header("Debug Options")]
    [SerializeField] private bool freezePosition = false;

    private MeshRenderer[] meshs;

    private void Start()
    {
#if !UNITY_EDITOR
        freezePosition = false;
#endif

        meshs = GetComponentsInChildren<MeshRenderer>();
        ChangeShipMaterial(mat_Glow);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (!freezePosition)
        {
            float positionX = transform.position.x + inputX * speed;
            positionX = Mathf.Clamp(positionX, -clampBorder, clampBorder);
            transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        }

        float orient = anim.GetFloat("Orientation");
        float distance = Mathf.Abs(inputX - orient);
        if (distance >= 0.01f)
        {
            inputX = Mathf.Lerp(orient, inputX, distance >= .4f ? .25f : .5f);
            anim.SetFloat("Orientation", inputX * tiltAmount);
        }
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.H))
        {
            StopCoroutine(HitEffect());
            StartCoroutine(HitEffect());
        }
    }

    public void Hit()
    {
        life--;

        if (life <= 0)
        {
            GameManager.Instance?.Defeat();
            this.enabled = false;
            //Destroy(gameObject);
        }

        StopCoroutine(HitEffect());
        StartCoroutine(HitEffect());
    }

    private void ChangeShipMaterial(Material mat)
    {
        for (int i = 0; i < meshs.Length; i++)
        {
            if (meshs[i].tag != "ScreenFirstPerson")
                meshs[i].material = mat;
        }
    }

    private void SetHologramColor(Color baseColor, Color highlight)
    {
        for (int i = 0; i < meshs.Length; i++)
        {
            var mesh = meshs[i];
            mesh.material.SetColor("Base_Color", baseColor);
            mesh.material.SetColor("Highlight_Color", highlight);
        }
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

        for (int i = 0; i < animationLoops; i++)
        {
            for (float t = 0; t <= hitAnimDuration; t += Time.deltaTime)
            {
                float v = t / hitAnimDuration;
                SetHologramColor(hitBaseColorAnim.Evaluate(v), hitHighlightColorAnim.Evaluate(v));
                yield return new WaitForEndOfFrame();
            }
        }

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
