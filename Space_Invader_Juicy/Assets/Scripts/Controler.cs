using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Controler : MonoBehaviour
{
    public float speed;
    public float life = 10;
    public float clampBorder = 10f;

    [SerializeField] private Animator anim;

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
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ennemy"))
        {
            GameManager.Instance.Defeat();
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3( Mathf.Max(pos.x - 2, pos.x - clampBorder) , pos.y, pos.z), Vector3.left * clampBorder);
        Gizmos.DrawRay(new Vector3( Mathf.Min(pos.x + 2, pos.x + clampBorder) , pos.y, pos.z), Vector3.right * clampBorder);
    }
}
